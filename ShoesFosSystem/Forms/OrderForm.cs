using ShoesFosSystem.Data;
using ShoesFosSystem.Models;
using ShoesFosSystem.Services;

namespace ShoesFosSystem.Forms;

/// <summary>
/// 새 주문 / 주문 수정 폼 - 품목 여러 줄, 합계, 저장/완료/취소
/// </summary>
public partial class OrderForm : Form
{
    private readonly Order _order;
    private List<OrderItem> _items = new();
    private List<ShoeType> _shoeTypes = new();
    private DataGridViewComboBoxColumn? _colShoeType;
    private List<string> _comboDisplayStrings = new();
    private bool _loading;

    public OrderForm(Order order, List<OrderItem> items)
    {
        _order = order;
        _items = items?.Select(x => new OrderItem
        {
            ShoeTypeId = x.ShoeTypeId,
            ShoeTypeName = x.ShoeTypeName,
            Qty = x.Qty,
            UnitPrice = x.UnitPrice,
            ExtraPrice = x.ExtraPrice,
            Memo = x.Memo,
            LineTotal = x.LineTotal
        }).ToList() ?? new List<OrderItem>();
        InitializeComponent();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        lblSeq.Text = $"오늘 {_order.DailySeq}번";
        lblBizDate.Text = _order.BizDate;
        txtCustomerName.Text = _order.CustomerName ?? "";
        txtCustomerPhone.Text = _order.CustomerPhone ?? "";
        txtOrderMemo.Text = _order.Memo ?? "";
        _shoeTypes = ShoeTypeRepository.GetAll(activeOnly: true);
        BuildGrid();
        LoadItemsIntoGrid();
        // 새 주문(품목 0개)이면 품목 한 줄 자동 추가 → 바로 '신발 종류' 선택 가능
        if (dgvItems.Rows.Count == 0 && _shoeTypes.Count > 0)
        {
            var first = _shoeTypes[0];
            dgvItems.Rows.Add(_comboDisplayStrings[0], 1, first.BasePrice, 0, "", first.BasePrice, first.Id);
        }
        UpdateTotalLabel();
        // 셀 클릭 시 바로 콤보 편집 가능하도록
        dgvItems.EditMode = DataGridViewEditMode.EditOnEnter;
    }

    /// <summary>
    /// 그리드 컬럼 구성: 신발종류(콤보), 수량, 단가, 추가금, 메모, 라인합계
    /// </summary>
    private void BuildGrid()
    {
        dgvItems.Columns.Clear();
        _colShoeType = new DataGridViewComboBoxColumn
        {
            Name = "colShoeType",
            HeaderText = "신발 종류",
            Width = 140,
            DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
        };
        _comboDisplayStrings.Clear();
        foreach (var st in _shoeTypes)
        {
            var display = $"{st.Name} ({st.BasePrice:N0}원)";
            _comboDisplayStrings.Add(display);
            _colShoeType.Items.Add(display);
        }
        dgvItems.Columns.Add(_colShoeType);

        dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colQty", HeaderText = "수량", Width = 60 });
        dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colUnitPrice", HeaderText = "단가", Width = 90 });
        dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colExtraPrice", HeaderText = "추가금", Width = 80 });
        dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMemo", HeaderText = "메모", Width = 120 });
        dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLineTotal", HeaderText = "라인합계", Width = 100, ReadOnly = true });
        dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colShoeTypeId", HeaderText = "", Width = 0, Visible = false });
    }

    private void LoadItemsIntoGrid()
    {
        _loading = true;
        dgvItems.Rows.Clear();
        foreach (var it in _items)
        {
            var idx = _shoeTypes.FindIndex(x => x.Id == it.ShoeTypeId);
            var comboText = idx >= 0 ? _comboDisplayStrings[idx] : "";
            var lineTotal = it.Qty * (it.UnitPrice + it.ExtraPrice);
            dgvItems.Rows.Add(comboText, it.Qty, it.UnitPrice, it.ExtraPrice, it.Memo ?? "", lineTotal, it.ShoeTypeId);
        }
        _loading = false;
    }

    private void dgvItems_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
    {
        if (_loading) return;
        RecalcRow(e.RowIndex);
        UpdateTotalLabel();
    }

    private void dgvItems_SelectionChanged(object? sender, EventArgs e)
    {
        // 선택 변경 시 추가금 프리셋 대상 갱신 등
    }

    /// <summary>
    /// 해당 행 라인합계 재계산 및 단가 자동 채우기(콤보 선택 시)
    /// </summary>
    private void RecalcRow(int rowIndex)
    {
        if (rowIndex < 0 || rowIndex >= dgvItems.Rows.Count) return;
        var row = dgvItems.Rows[rowIndex];
        if (row.IsNewRow) return;

        var comboVal = row.Cells["colShoeType"]?.Value?.ToString();
        if (!string.IsNullOrEmpty(comboVal))
        {
            var idx = _comboDisplayStrings.IndexOf(comboVal);
            if (idx >= 0 && idx < _shoeTypes.Count)
            {
                var st = _shoeTypes[idx];
                row.Cells["colUnitPrice"].Value = st.BasePrice;
                row.Cells["colShoeTypeId"].Value = st.Id;
            }
        }

        var qty = GetCellInt(row, "colQty", 1);
        var unitPrice = GetCellInt(row, "colUnitPrice", 0);
        var extraPrice = GetCellInt(row, "colExtraPrice", 0);
        if (qty < 1) row.Cells["colQty"].Value = 1;
        var lineTotal = (qty < 1 ? 1 : qty) * (unitPrice + extraPrice);
        row.Cells["colLineTotal"].Value = lineTotal;
    }

    private static int GetCellInt(DataGridViewRow row, string colName, int defaultValue)
    {
        var v = row.Cells[colName]?.Value;
        if (v == null) return defaultValue;
        if (v is int i) return i;
        return int.TryParse(v.ToString(), out var n) ? n : defaultValue;
    }

    private void UpdateTotalLabel()
    {
        var total = 0;
        for (var i = 0; i < dgvItems.Rows.Count; i++)
        {
            if (dgvItems.Rows[i].IsNewRow) continue;
            var v = dgvItems.Rows[i].Cells["colLineTotal"]?.Value;
            if (v != null && int.TryParse(v.ToString(), out var n)) total += n;
        }
        lblTotalAmount.Text = $"{total:N0}원";
    }

    /// <summary>
    /// 그리드에서 OrderItem 리스트로 수집
    /// </summary>
    private List<OrderItem> CollectItemsFromGrid()
    {
        var list = new List<OrderItem>();
        for (var i = 0; i < dgvItems.Rows.Count; i++)
        {
            var row = dgvItems.Rows[i];
            if (row.IsNewRow) continue;
            var comboVal = row.Cells["colShoeType"]?.Value?.ToString();
            if (string.IsNullOrEmpty(comboVal)) continue;
            var idx = _comboDisplayStrings.IndexOf(comboVal);
            var shoeTypeId = idx >= 0 && idx < _shoeTypes.Count ? _shoeTypes[idx].Id : GetCellInt(row, "colShoeTypeId", 0);
            if (shoeTypeId == 0) continue;
            var qty = GetCellInt(row, "colQty", 1);
            var unitPrice = GetCellInt(row, "colUnitPrice", 0);
            var extraPrice = GetCellInt(row, "colExtraPrice", 0);
            var memo = row.Cells["colMemo"]?.Value?.ToString();
            list.Add(new OrderItem
            {
                ShoeTypeId = shoeTypeId,
                Qty = qty,
                UnitPrice = unitPrice,
                ExtraPrice = extraPrice,
                Memo = string.IsNullOrWhiteSpace(memo) ? null : memo.Trim(),
                LineTotal = qty * (unitPrice + extraPrice)
            });
        }
        return list;
    }

    private void btnAddItem_Click(object? sender, EventArgs e)
    {
        if (_shoeTypes.Count == 0)
        {
            MessageBox.Show("먼저 가격표에서 신발 종류를 추가해 주세요.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        var first = _shoeTypes[0];
        dgvItems.Rows.Add(_comboDisplayStrings[0], 1, first.BasePrice, 0, "", first.BasePrice, first.Id);
        UpdateTotalLabel();
    }

    private void btnDeleteItem_Click(object? sender, EventArgs e)
    {
        if (dgvItems.CurrentRow?.Index >= 0 && !dgvItems.CurrentRow.IsNewRow)
        {
            dgvItems.Rows.RemoveAt(dgvItems.CurrentRow.Index);
            UpdateTotalLabel();
        }
    }

    private void btnExtra3000_Click(object? sender, EventArgs e) => AddExtraToCurrentRow(3000);
    private void btnExtra5000_Click(object? sender, EventArgs e) => AddExtraToCurrentRow(5000);
    private void btnExtra10000_Click(object? sender, EventArgs e) => AddExtraToCurrentRow(10000);

    private void AddExtraToCurrentRow(int add)
    {
        var row = dgvItems.CurrentRow;
        if (row == null || row.Index < 0 || row.IsNewRow) return;
        var cur = GetCellInt(row, "colExtraPrice", 0);
        row.Cells["colExtraPrice"].Value = cur + add;
        RecalcRow(row.Index);
        UpdateTotalLabel();
    }

    /// <summary>
    /// 주문완료: 저장 후 상태를 완료로 설정하고 창 닫기
    /// </summary>
    private void btnCompleteOrder_Click(object? sender, EventArgs e)
    {
        var items = CollectItemsFromGrid();
        if (items.Count == 0)
        {
            MessageBox.Show("품목을 1개 이상 입력해 주세요.", "주문완료 불가", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        foreach (var it in items)
        {
            if (!OrderService.ValidateItem(it, out var err))
            {
                MessageBox.Show(err, "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        _order.CustomerName = string.IsNullOrWhiteSpace(txtCustomerName.Text) ? null : txtCustomerName.Text.Trim();
        _order.CustomerPhone = string.IsNullOrWhiteSpace(txtCustomerPhone.Text) ? null : txtCustomerPhone.Text.Trim();
        _order.Memo = string.IsNullOrWhiteSpace(txtOrderMemo.Text) ? null : txtOrderMemo.Text.Trim();
        _order.Status = "COMPLETED";

        try
        {
            OrderRepository.UpdateOrderWithItems(_order, items);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("저장 중 오류: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// 주문 삭제 (완전 삭제)
    /// </summary>
    private void btnDeleteOrder_Click(object? sender, EventArgs e)
    {
        if (MessageBox.Show("이 주문을 완전히 삭제하시겠습니까? 복구할 수 없습니다.", "주문 삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            return;
        try
        {
            OrderRepository.DeleteOrder(_order.Id);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("삭제 중 오류: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// POS 영수증 출력 (추후 기기 연동 시 구현)
    /// </summary>
    private void btnPrint_Click(object? sender, EventArgs e)
    {
        MessageBox.Show("POS 출력 기능은 기기 모델 선정 후 연동 예정입니다.", "출력", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
