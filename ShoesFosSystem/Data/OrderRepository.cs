using Microsoft.Data.Sqlite;
using ShoesFosSystem.Models;

namespace ShoesFosSystem.Data;

/// <summary>
/// 주문/품목 CRUD 및 매출 쿼리
/// </summary>
public static class OrderRepository
{
    /// <summary>
    /// 해당 날짜 다음 daily_seq 발급 (트랜잭션 내에서 호출)
    /// </summary>
    public static int IssueNextDailySeq(SqliteConnection conn, SqliteTransaction? trans, string bizDate)
    {
        using (var cmd = new SqliteCommand("SELECT last_seq FROM daily_counters WHERE biz_date = @bizDate", conn, trans))
        {
            cmd.Parameters.AddWithValue("@bizDate", bizDate);
            var o = cmd.ExecuteScalar();
            if (o == null || o == DBNull.Value)
            {
                using var ins = new SqliteCommand("INSERT INTO daily_counters (biz_date, last_seq) VALUES (@bizDate, 0)", conn, trans);
                ins.Parameters.AddWithValue("@bizDate", bizDate);
                ins.ExecuteNonQuery();
            }
        }

        using (var cmd = new SqliteCommand("UPDATE daily_counters SET last_seq = last_seq + 1 WHERE biz_date = @bizDate", conn, trans))
        {
            cmd.Parameters.AddWithValue("@bizDate", bizDate);
            cmd.ExecuteNonQuery();
        }

        using var sel = new SqliteCommand("SELECT last_seq FROM daily_counters WHERE biz_date = @bizDate", conn, trans);
        sel.Parameters.AddWithValue("@bizDate", bizDate);
        return Convert.ToInt32(sel.ExecuteScalar());
    }

    /// <summary>
    /// 주문 헤더만 INSERT (total_amount=0, status=RECEIVED)
    /// </summary>
    public static int InsertOrder(SqliteConnection conn, SqliteTransaction? trans, string bizDate, int dailySeq)
    {
        var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        using var cmd = new SqliteCommand(
            @"INSERT INTO orders (biz_date, daily_seq, customer_name, customer_phone, status, memo, total_amount, created_at, updated_at)
              VALUES (@bizDate, @dailySeq, NULL, NULL, 'RECEIVED', NULL, 0, @now, @now);
              SELECT last_insert_rowid();", conn, trans);
        cmd.Parameters.AddWithValue("@bizDate", bizDate);
        cmd.Parameters.AddWithValue("@dailySeq", dailySeq);
        cmd.Parameters.AddWithValue("@now", now);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    /// <summary>
    /// 특정 날짜 주문 목록 (daily_seq 순)
    /// </summary>
    public static List<Order> GetOrdersByBizDate(string bizDate)
    {
        var list = new List<Order>();
        using var conn = DatabaseHelper.CreateConnection();
        using var cmd = new SqliteCommand(
            "SELECT id, biz_date, daily_seq, customer_name, customer_phone, status, memo, total_amount, created_at, updated_at FROM orders WHERE biz_date = @bizDate ORDER BY daily_seq ASC", conn);
        cmd.Parameters.AddWithValue("@bizDate", bizDate);
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            list.Add(ReadOrder(r));
        }
        return list;
    }

    /// <summary>
    /// ID로 주문 + 품목 조회
    /// </summary>
    public static (Order? order, List<OrderItem> items) GetOrderWithItems(int orderId)
    {
        using var conn = DatabaseHelper.CreateConnection();
        Order? order = null;
        using (var cmd = new SqliteCommand(
            "SELECT id, biz_date, daily_seq, customer_name, customer_phone, status, memo, total_amount, created_at, updated_at FROM orders WHERE id = @id", conn))
        {
            cmd.Parameters.AddWithValue("@id", orderId);
            using var r = cmd.ExecuteReader();
            if (r.Read()) order = ReadOrder(r);
        }
        if (order == null) return (null, new List<OrderItem>());

        var items = new List<OrderItem>();
        using (var cmd = new SqliteCommand(
            "SELECT oi.id, oi.order_id, oi.shoe_type_id, oi.qty, oi.unit_price, oi.extra_price, oi.memo, oi.line_total, st.name FROM order_items oi JOIN shoe_types st ON st.id = oi.shoe_type_id WHERE oi.order_id = @orderId", conn))
        {
            cmd.Parameters.AddWithValue("@orderId", orderId);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                items.Add(new OrderItem
                {
                    Id = r.GetInt32(0),
                    OrderId = r.GetInt32(1),
                    ShoeTypeId = r.GetInt32(2),
                    Qty = r.GetInt32(3),
                    UnitPrice = r.GetInt32(4),
                    ExtraPrice = r.GetInt32(5),
                    Memo = r.IsDBNull(6) ? null : r.GetString(6),
                    LineTotal = r.GetInt32(7),
                    ShoeTypeName = r.GetString(8)
                });
            }
        }
        return (order, items);
    }

    /// <summary>
    /// 주문 업데이트(헤더) + 품목 삭제 후 재삽입, total_amount 서버 재계산
    /// </summary>
    public static void UpdateOrderWithItems(Order order, List<OrderItem> items)
    {
        var totalAmount = items.Sum(x => x.LineTotal);
        order.TotalAmount = totalAmount;
        var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        order.UpdatedAt = now;

        using var conn = DatabaseHelper.CreateConnection();
        using var trans = conn.BeginTransaction();
        try
        {
            using (var cmd = new SqliteCommand(
                "UPDATE orders SET customer_name=@cn, customer_phone=@cp, status=@status, memo=@memo, total_amount=@total, updated_at=@updated WHERE id=@id", conn, trans))
            {
                cmd.Parameters.AddWithValue("@id", order.Id);
                cmd.Parameters.AddWithValue("@cn", (object?)order.CustomerName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@cp", (object?)order.CustomerPhone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@status", order.Status);
                cmd.Parameters.AddWithValue("@memo", (object?)order.Memo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@total", order.TotalAmount);
                cmd.Parameters.AddWithValue("@updated", order.UpdatedAt);
                cmd.ExecuteNonQuery();
            }

            using (var del = new SqliteCommand("DELETE FROM order_items WHERE order_id = @id", conn, trans))
            {
                del.Parameters.AddWithValue("@id", order.Id);
                del.ExecuteNonQuery();
            }

            foreach (var it in items)
            {
                it.LineTotal = it.Qty * (it.UnitPrice + it.ExtraPrice);
                using var ins = new SqliteCommand(
                    "INSERT INTO order_items (order_id, shoe_type_id, qty, unit_price, extra_price, memo, line_total) VALUES (@oid, @stid, @qty, @up, @ex, @memo, @lt)", conn, trans);
                ins.Parameters.AddWithValue("@oid", order.Id);
                ins.Parameters.AddWithValue("@stid", it.ShoeTypeId);
                ins.Parameters.AddWithValue("@qty", it.Qty);
                ins.Parameters.AddWithValue("@up", it.UnitPrice);
                ins.Parameters.AddWithValue("@ex", it.ExtraPrice);
                ins.Parameters.AddWithValue("@memo", (object?)it.Memo ?? DBNull.Value);
                ins.Parameters.AddWithValue("@lt", it.LineTotal);
                ins.ExecuteNonQuery();
            }
            trans.Commit();
        }
        catch
        {
            trans.Rollback();
            throw;
        }
    }

    /// <summary>
    /// 주문 상태만 변경 (취소 등)
    /// </summary>
    public static void UpdateOrderStatus(int orderId, string status)
    {
        var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        using var conn = DatabaseHelper.CreateConnection();
        using var cmd = new SqliteCommand("UPDATE orders SET status=@status, updated_at=@updated WHERE id=@id", conn);
        cmd.Parameters.AddWithValue("@id", orderId);
        cmd.Parameters.AddWithValue("@status", status);
        cmd.Parameters.AddWithValue("@updated", now);
        cmd.ExecuteNonQuery();
    }

    /// <summary>
    /// 주문 완전 삭제 (order_items는 CASCADE로 함께 삭제)
    /// </summary>
    public static void DeleteOrder(int orderId)
    {
        using var conn = DatabaseHelper.CreateConnection();
        using var cmd = new SqliteCommand("DELETE FROM orders WHERE id = @id", conn);
        cmd.Parameters.AddWithValue("@id", orderId);
        cmd.ExecuteNonQuery();
    }

    /// <summary>
    /// 일매출(취소 제외)
    /// </summary>
    public static int GetDailySales(string bizDate)
    {
        using var conn = DatabaseHelper.CreateConnection();
        using var cmd = new SqliteCommand(
            "SELECT COALESCE(SUM(total_amount),0) FROM orders WHERE biz_date=@bizDate AND status != 'CANCELED'", conn);
        cmd.Parameters.AddWithValue("@bizDate", bizDate);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    /// <summary>
    /// 일자별 매출 (기간, 취소 제외)
    /// </summary>
    public static List<(string bizDate, int sales)> GetDailySalesInRange(string startDate, string endDate)
    {
        var list = new List<(string, int)>();
        using var conn = DatabaseHelper.CreateConnection();
        using var cmd = new SqliteCommand(
            "SELECT biz_date, COALESCE(SUM(total_amount),0) FROM orders WHERE biz_date BETWEEN @s AND @e AND status != 'CANCELED' GROUP BY biz_date ORDER BY biz_date", conn);
        cmd.Parameters.AddWithValue("@s", startDate);
        cmd.Parameters.AddWithValue("@e", endDate);
        using var r = cmd.ExecuteReader();
        while (r.Read())
            list.Add((r.GetString(0), r.GetInt32(1)));
        return list;
    }

    /// <summary>
    /// 기간 신발종류별 매출 (취소 제외)
    /// </summary>
    public static List<(string name, int sales)> GetSalesByShoeType(string startDate, string endDate)
    {
        var list = new List<(string, int)>();
        using var conn = DatabaseHelper.CreateConnection();
        using var cmd = new SqliteCommand(
            @"SELECT st.name, COALESCE(SUM(oi.line_total),0) FROM orders o
              JOIN order_items oi ON oi.order_id = o.id
              JOIN shoe_types st ON st.id = oi.shoe_type_id
              WHERE o.biz_date BETWEEN @s AND @e AND o.status != 'CANCELED'
              GROUP BY st.id ORDER BY 2 DESC", conn);
        cmd.Parameters.AddWithValue("@s", startDate);
        cmd.Parameters.AddWithValue("@e", endDate);
        using var r = cmd.ExecuteReader();
        while (r.Read())
            list.Add((r.GetString(0), r.GetInt32(1)));
        return list;
    }

    /// <summary>
    /// 기간 내 주문 목록 (리포트/CSV용, 취소 포함 여부 옵션)
    /// </summary>
    public static List<Order> GetOrdersInRange(string startDate, string endDate, bool includeCanceled = true)
    {
        var list = new List<Order>();
        using var conn = DatabaseHelper.CreateConnection();
        var sql = includeCanceled
            ? "SELECT id, biz_date, daily_seq, customer_name, customer_phone, status, memo, total_amount, created_at, updated_at FROM orders WHERE biz_date BETWEEN @s AND @e ORDER BY biz_date, daily_seq"
            : "SELECT id, biz_date, daily_seq, customer_name, customer_phone, status, memo, total_amount, created_at, updated_at FROM orders WHERE biz_date BETWEEN @s AND @e AND status != 'CANCELED' ORDER BY biz_date, daily_seq";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@s", startDate);
        cmd.Parameters.AddWithValue("@e", endDate);
        using var r = cmd.ExecuteReader();
        while (r.Read())
            list.Add(ReadOrder(r));
        return list;
    }

    /// <summary>
    /// 기간 내 order_id 목록에 해당하는 order_items 전부 (이름 포함)
    /// </summary>
    public static List<OrderItem> GetOrderItemsForOrderIds(IEnumerable<int> orderIds)
    {
        var ids = string.Join(",", orderIds);
        if (string.IsNullOrEmpty(ids)) return new List<OrderItem>();
        var list = new List<OrderItem>();
        using var conn = DatabaseHelper.CreateConnection();
        using var cmd = new SqliteCommand(
            $"SELECT oi.id, oi.order_id, oi.shoe_type_id, oi.qty, oi.unit_price, oi.extra_price, oi.memo, oi.line_total, st.name FROM order_items oi JOIN shoe_types st ON st.id = oi.shoe_type_id WHERE oi.order_id IN ({ids})", conn);
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            list.Add(new OrderItem
            {
                Id = r.GetInt32(0),
                OrderId = r.GetInt32(1),
                ShoeTypeId = r.GetInt32(2),
                Qty = r.GetInt32(3),
                UnitPrice = r.GetInt32(4),
                ExtraPrice = r.GetInt32(5),
                Memo = r.IsDBNull(6) ? null : r.GetString(6),
                LineTotal = r.GetInt32(7),
                ShoeTypeName = r.GetString(8)
            });
        }
        return list;
    }

    private static Order ReadOrder(SqliteDataReader r)
    {
        return new Order
        {
            Id = r.GetInt32(0),
            BizDate = r.GetString(1),
            DailySeq = r.GetInt32(2),
            CustomerName = r.IsDBNull(3) ? null : r.GetString(3),
            CustomerPhone = r.IsDBNull(4) ? null : r.GetString(4),
            Status = r.GetString(5),
            Memo = r.IsDBNull(6) ? null : r.GetString(6),
            TotalAmount = r.GetInt32(7),
            CreatedAt = r.GetString(8),
            UpdatedAt = r.GetString(9)
        };
    }
}
