using ShoesFosSystem.Data;
using System.Globalization;
using System.Text;

namespace ShoesFosSystem.Forms;

/// <summary>
/// 매출/리포트 - 기간별 일매출, 신발종류별 매출, CSV 내보내기
/// </summary>
public partial class ReportForm : Form
{
    private string _startDate = "";
    private string _endDate = "";
    private int _periodTotal;

    public ReportForm()
    {
        InitializeComponent();
        var today = DateTime.Today;
        dtpStart.Value = today.AddMonths(-1);
        dtpEnd.Value = today;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        RunSearch();
    }

    private void btnSearch_Click(object? sender, EventArgs e)
    {
        RunSearch();
    }

    private void RunSearch()
    {
        _startDate = dtpStart.Value.ToString("yyyy-MM-dd");
        _endDate = dtpEnd.Value.ToString("yyyy-MM-dd");
        if (_startDate.CompareTo(_endDate) > 0)
        {
            MessageBox.Show("시작일이 종료일보다 늦을 수 없습니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var daily = OrderRepository.GetDailySalesInRange(_startDate, _endDate);
        _periodTotal = daily.Sum(x => x.sales);
        lblPeriodSales.Text = $"기간 총매출(취소제외): {_periodTotal:N0}원";

        dgvDaily.Rows.Clear();
        foreach (var (bizDate, sales) in daily)
            dgvDaily.Rows.Add(bizDate, sales);
        if (dgvDaily.Columns["colSales"].Index >= 0)
            dgvDaily.Columns["colSales"].DefaultCellStyle.Format = "N0";

        var byType = OrderRepository.GetSalesByShoeType(_startDate, _endDate);
        dgvByType.Rows.Clear();
        foreach (var (name, sales) in byType)
            dgvByType.Rows.Add(name, sales);
        if (dgvByType.Columns["colTypeSales"].Index >= 0)
            dgvByType.Columns["colTypeSales"].DefaultCellStyle.Format = "N0";
    }

    private void btnExportCsv_Click(object? sender, EventArgs e)
    {
        _startDate = dtpStart.Value.ToString("yyyy-MM-dd");
        _endDate = dtpEnd.Value.ToString("yyyy-MM-dd");
        var dir = AppDomain.CurrentDomain.BaseDirectory;
        var exportDir = Path.Combine(dir, "export");
        Directory.CreateDirectory(exportDir);
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmm");
        var ordersPath = Path.Combine(exportDir, $"orders_{timestamp}.csv");
        var itemsPath = Path.Combine(exportDir, $"order_items_{timestamp}.csv");

        try
        {
            var orders = OrderRepository.GetOrdersInRange(_startDate, _endDate, includeCanceled: true);
            var orderIds = orders.Select(o => o.Id).ToList();
            var items = OrderRepository.GetOrderItemsForOrderIds(orderIds);

            WriteOrdersCsv(ordersPath, orders);
            WriteOrderItemsCsv(itemsPath, items);

            MessageBox.Show($"내보내기 완료:\n{ordersPath}\n{itemsPath}", "CSV 내보내기", MessageBoxButtons.OK, MessageBoxIcon.Information);
            System.Diagnostics.Process.Start("explorer.exe", exportDir);
        }
        catch (Exception ex)
        {
            MessageBox.Show("CSV 내보내기 중 오류: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// orders CSV 작성 (기간 내 주문)
    /// </summary>
    private static void WriteOrdersCsv(string path, List<Models.Order> orders)
    {
        var sb = new StringBuilder();
        sb.AppendLine("id,biz_date,daily_seq,customer_name,customer_phone,status,memo,total_amount,created_at,updated_at");
        foreach (var o in orders)
        {
            sb.AppendLine(string.Join(",",
                o.Id,
                CsvEscape(o.BizDate),
                o.DailySeq,
                CsvEscape(o.CustomerName),
                CsvEscape(o.CustomerPhone),
                CsvEscape(o.Status),
                CsvEscape(o.Memo),
                o.TotalAmount,
                CsvEscape(o.CreatedAt),
                CsvEscape(o.UpdatedAt)));
        }
        File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
    }

    /// <summary>
    /// order_items CSV 작성
    /// </summary>
    private static void WriteOrderItemsCsv(string path, List<Models.OrderItem> items)
    {
        var sb = new StringBuilder();
        sb.AppendLine("id,order_id,shoe_type_id,shoe_type_name,qty,unit_price,extra_price,memo,line_total");
        foreach (var it in items)
        {
            sb.AppendLine(string.Join(",",
                it.Id,
                it.OrderId,
                it.ShoeTypeId,
                CsvEscape(it.ShoeTypeName),
                it.Qty,
                it.UnitPrice,
                it.ExtraPrice,
                CsvEscape(it.Memo),
                it.LineTotal));
        }
        File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
    }

    private static string CsvEscape(string? s)
    {
        if (s == null) return "";
        if (s.Contains(',') || s.Contains('"') || s.Contains('\n'))
            return "\"" + s.Replace("\"", "\"\"") + "\"";
        return s;
    }
}
