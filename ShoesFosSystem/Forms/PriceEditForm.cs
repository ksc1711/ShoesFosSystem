using ShoesFosSystem.Data;
using ShoesFosSystem.Models;

namespace ShoesFosSystem.Forms;

/// <summary>
/// 가격표 1건 추가/수정 다이얼로그
/// </summary>
public partial class PriceEditForm : Form
{
    private readonly ShoeType? _entity;

    public PriceEditForm(ShoeType? entity)
    {
        _entity = entity;
        InitializeComponent();
        if (entity != null)
        {
            Text = "가격표 수정";
            txtName.Text = entity.Name;
            numPrice.Value = entity.BasePrice;
            chkActive.Checked = entity.IsActive == 1;
        }
    }

    private void btnOk_Click(object? sender, EventArgs e)
    {
        var name = txtName.Text.Trim();
        if (string.IsNullOrEmpty(name))
        {
            MessageBox.Show("신발 종류명을 입력해 주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtName.Focus();
            return;
        }
        var price = (int)numPrice.Value;
        if (price < 0)
        {
            MessageBox.Show("가격은 0 이상이어야 합니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            if (_entity == null)
            {
                var newEntity = new ShoeType { Name = name, BasePrice = price, IsActive = chkActive.Checked ? 1 : 0, SortOrder = 0 };
                ShoeTypeRepository.Insert(newEntity);
            }
            else
            {
                _entity.Name = name;
                _entity.BasePrice = price;
                _entity.IsActive = chkActive.Checked ? 1 : 0;
                ShoeTypeRepository.Update(_entity);
            }
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("저장 중 오류가 발생했습니다: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
