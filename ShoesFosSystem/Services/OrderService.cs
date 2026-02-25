using ShoesFosSystem.Data;
using ShoesFosSystem.Models;

namespace ShoesFosSystem.Services;

/// <summary>
/// 주문 생성( daily_seq 발급 ) 및 저장 로직
/// </summary>
public static class OrderService
{
    /// <summary>
    /// 새 주문 생성: daily_seq 발급 후 헤더 INSERT, 새 Order.Id 반환
    /// </summary>
    public static int CreateNewOrder(string bizDate)
    {
        using var conn = DatabaseHelper.CreateConnection();
        using var trans = conn.BeginTransaction();
        var seq = OrderRepository.IssueNextDailySeq(conn, trans, bizDate);
        var orderId = OrderRepository.InsertOrder(conn, trans, bizDate, seq);
        trans.Commit();
        return orderId;
    }

    /// <summary>
    /// 라인 합계: qty * (unit_price + extra_price)
    /// </summary>
    public static int CalculateLineTotal(int qty, int unitPrice, int extraPrice)
    {
        return qty * (unitPrice + extraPrice);
    }

    /// <summary>
    /// 품목 리스트로 주문 합계 계산
    /// </summary>
    public static int CalculateOrderTotal(IEnumerable<OrderItem> items)
    {
        return items.Sum(x => x.Qty * (x.UnitPrice + x.ExtraPrice));
    }

    /// <summary>
    /// 유효성: qty>=1, unit_price>=0, extra_price>=0
    /// </summary>
    public static bool ValidateItem(OrderItem item, out string error)
    {
        if (item.Qty < 1) { error = "수량은 1 이상이어야 합니다."; return false; }
        if (item.UnitPrice < 0) { error = "단가는 0 이상이어야 합니다."; return false; }
        if (item.ExtraPrice < 0) { error = "추가금은 0 이상이어야 합니다."; return false; }
        error = string.Empty;
        return true;
    }
}
