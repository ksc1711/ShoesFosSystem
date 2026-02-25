using ShoesFosSystem.Data;
using ShoesFosSystem.Models;

namespace ShoesFosSystem.Forms;

/// <summary>
/// 가격표(신발 종류) 설정 폼 - 목록, 추가/수정/비활성
/// </summary>
public partial class PriceListForm : Form
{
    private List<ShoeType> _list = new();

    public PriceListForm()
    {
        InitializeComponent();
        dgv.DataBindingComplete += (s, e) => { dgv.ClearSelection(); };
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        LoadList();
    }

    /// <summary>
    /// 목록 새로고침
    /// </summary>
    private void LoadList()
    {
        try
        {
            _list = ShoeTypeRepository.GetAll(activeOnly: false);
            var display = _list.Select(x => new
            {
                x.Id,
                x.Name,
                x.BasePrice,
                IsActiveText = x.IsActive == 1 ? "판매중" : "중지"
            }).ToList();
            dgv.DataSource = null;
            dgv.DataSource = display;
            if (dgv.Columns.Count > 0)
            {
                if (dgv.Columns["colId"] != null) dgv.Columns["colId"].Width = 60;
                if (dgv.Columns["colBasePrice"] != null) dgv.Columns["colBasePrice"].DefaultCellStyle.Format = "N0";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("가격표 목록을 불러오는 중 오류가 발생했습니다.\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnAdd_Click(object? sender, EventArgs e)
    {
        using var dlg = new PriceEditForm(null);
        if (dlg.ShowDialog() != DialogResult.OK) return;
        LoadList();
    }

    private void btnEdit_Click(object? sender, EventArgs e)
    {
        if (dgv.CurrentRow?.DataBoundItem is not { } row) return;
        var id = (int)row.GetType().GetProperty("Id")!.GetValue(row)!;
        var entity = _list.FirstOrDefault(x => x.Id == id);
        if (entity == null) return;
        using var dlg = new PriceEditForm(entity);
        if (dlg.ShowDialog() != DialogResult.OK) return;
        LoadList();
    }

    private void btnInactive_Click(object? sender, EventArgs e)
    {
        if (dgv.CurrentRow?.DataBoundItem is not { } row) return;
        var id = (int)row.GetType().GetProperty("Id")!.GetValue(row)!;
        if (MessageBox.Show("이 신발 종류를 판매 중지하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;
        ShoeTypeRepository.SetInactive(id);
        LoadList();
    }
}
