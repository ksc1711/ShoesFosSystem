using ShoesFosSystem.Data;
using ShoesFosSystem.Models;
using ShoesFosSystem.Services;

namespace ShoesFosSystem.Forms;

/// <summary>
/// 메인 폼 - 오늘 날짜, 주문 리스트, 매출 요약, 버튼
/// </summary>
public partial class MainForm : Form
{
    private string _currentBizDate = "";

    public MainForm()
    {
        InitializeComponent();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        dtpFilter.Value = DateTime.Today;
        _currentBizDate = GetBizDateFromPicker();
        RefreshDateLabel();
        LoadOrders();
        RefreshSummary();
    }

    private string GetBizDateFromPicker()
    {
        return dtpFilter.Value.ToString("yyyy-MM-dd");
    }

    private void dtpFilter_ValueChanged(object? sender, EventArgs e)
    {
        _currentBizDate = GetBizDateFromPicker();
        RefreshDateLabel();
        LoadOrders();
        RefreshSummary();
    }

    private void RefreshDateLabel()
    {
        var isToday = dtpFilter.Value.Date == DateTime.Today;
        lblDate.Text = isToday ? $"오늘 {_currentBizDate}" : _currentBizDate;
    }

    /// <summary>
    /// 오늘(또는 선택일) 주문 리스트 로드
    /// </summary>
    private void LoadOrders()
    {
        var orders = OrderRepository.GetOrdersByBizDate(_currentBizDate);
        dgvOrders.Rows.Clear();
        foreach (var o in orders)
        {
            var statusText = o.Status switch
            {
                "RECEIVED" => "접수",
                "COMPLETED" => "완료",
                "CANCELED" => "취소",
                _ => o.Status
            };
            dgvOrders.Rows.Add(o.DailySeq, o.CustomerName ?? "-", o.TotalAmount, statusText, "수정", o.Id);
        }
        if (dgvOrders.Columns["colTotal"].Index >= 0)
            dgvOrders.Columns["colTotal"].DefaultCellStyle.Format = "N0";
    }

    /// <summary>
    /// 선택일 매출 요약 갱신
    /// </summary>
    private void RefreshSummary()
    {
        var orders = OrderRepository.GetOrdersByBizDate(_currentBizDate);
        var completed = orders.Where(x => x.Status != "CANCELED").ToList();
        var cancelCount = orders.Count(x => x.Status == "CANCELED");
        var totalSales = OrderRepository.GetDailySales(_currentBizDate);

        lblOrderCount.Text = $"주문 수(취소 제외): {completed.Count}건";
        lblTotalSales.Text = $"총 매출: {totalSales:N0}원";
        lblCancelCount.Text = $"취소: {cancelCount}건(참고)";
    }

    private void btnNewOrder_Click(object? sender, EventArgs e)
    {
        var bizDate = DateTime.Today.ToString("yyyy-MM-dd");
        try
        {
            var orderId = OrderService.CreateNewOrder(bizDate);
            var (order, items) = OrderRepository.GetOrderWithItems(orderId);
            if (order == null) return;
            using var f = new OrderForm(order, items);
            if (f.ShowDialog() == DialogResult.OK)
            {
                dtpFilter.Value = DateTime.Today;
                _currentBizDate = GetBizDateFromPicker();
                LoadOrders();
                RefreshSummary();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("주문 생성 중 오류: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void dgvOrders_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex != colEdit.Index) return;
        if (!TryGetOrderIdFromRow(e.RowIndex, out var orderId)) return;
        OpenOrderDetail(orderId);
    }

    /// <summary>
    /// 리스트 행 더블클릭 시 주문 상세(수정) 창 열기
    /// </summary>
    private void dgvOrders_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        if (!TryGetOrderIdFromRow(e.RowIndex, out var orderId)) return;
        OpenOrderDetail(orderId);
    }

    /// <summary>
    /// 해당 행에서 주문 ID 추출
    /// </summary>
    private bool TryGetOrderIdFromRow(int rowIndex, out int orderId)
    {
        orderId = 0;
        if (rowIndex < 0 || rowIndex >= dgvOrders.Rows.Count) return false;
        var row = dgvOrders.Rows[rowIndex];
        var orderIdCell = row.Cells["colOrderId"]?.Value;
        return orderIdCell != null && int.TryParse(orderIdCell.ToString(), out orderId);
    }

    /// <summary>
    /// 주문 상세(수정) 창 열기 후 리스트/요약 새로고침
    /// </summary>
    private void OpenOrderDetail(int orderId)
    {
        var (order, items) = OrderRepository.GetOrderWithItems(orderId);
        if (order == null) return;
        using var f = new OrderForm(order, items);
        if (f.ShowDialog() == DialogResult.OK)
        {
            LoadOrders();
            RefreshSummary();
        }
    }

    private void btnPriceList_Click(object? sender, EventArgs e)
    {
        using var f = new PriceListForm();
        f.ShowDialog();
    }

    private void btnReport_Click(object? sender, EventArgs e)
    {
        using var f = new ReportForm();
        f.ShowDialog();
    }

    /// <summary>
    /// 외부에서 리스트/요약 새로고침 호출용
    /// </summary>
    public void RefreshListAndSummary()
    {
        _currentBizDate = GetBizDateFromPicker();
        LoadOrders();
        RefreshSummary();
    }
}
