namespace ShoesFosSystem.Forms;

partial class PriceListForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        panelTop = new Panel();
        btnAdd = new Button();
        btnEdit = new Button();
        btnInactive = new Button();
        btnClose = new Button();
        dgv = new DataGridView();
        colId = new DataGridViewTextBoxColumn();
        colName = new DataGridViewTextBoxColumn();
        colBasePrice = new DataGridViewTextBoxColumn();
        colIsActive = new DataGridViewTextBoxColumn();
        panelTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
        SuspendLayout();
        //
        // panelTop
        //
        panelTop.BackColor = Color.FromArgb(255, 255, 255);
        panelTop.Controls.Add(btnAdd);
        panelTop.Controls.Add(btnEdit);
        panelTop.Controls.Add(btnInactive);
        panelTop.Controls.Add(btnClose);
        panelTop.Dock = DockStyle.Top;
        panelTop.Padding = new Padding(14, 14, 14, 10);
        panelTop.Size = new Size(584, 72);
        panelTop.TabIndex = 0;
        //
        // btnAdd
        //
        btnAdd.Font = new Font("맑은 고딕", 14F, FontStyle.Bold);
        btnAdd.Location = new Point(12, 12);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(120, 46);
        btnAdd.TabIndex = 0;
        btnAdd.Text = "추가";
        btnAdd.UseVisualStyleBackColor = true;
        btnAdd.Click += btnAdd_Click;
        //
        // btnEdit
        //
        btnEdit.Font = new Font("맑은 고딕", 14F, FontStyle.Bold);
        btnEdit.Location = new Point(138, 12);
        btnEdit.Name = "btnEdit";
        btnEdit.Size = new Size(120, 46);
        btnEdit.TabIndex = 1;
        btnEdit.Text = "수정";
        btnEdit.UseVisualStyleBackColor = true;
        btnEdit.Click += btnEdit_Click;
        //
        // btnInactive
        //
        btnInactive.Font = new Font("맑은 고딕", 14F, FontStyle.Bold);
        btnInactive.Location = new Point(264, 12);
        btnInactive.Name = "btnInactive";
        btnInactive.Size = new Size(140, 46);
        btnInactive.TabIndex = 2;
        btnInactive.Text = "판매중지";
        btnInactive.UseVisualStyleBackColor = true;
        btnInactive.Click += btnInactive_Click;
        //
        // btnClose
        //
        btnClose.Font = new Font("맑은 고딕", 14F, FontStyle.Bold);
        btnClose.Location = new Point(410, 12);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(120, 46);
        btnClose.TabIndex = 3;
        btnClose.Text = "닫기";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += (s, e) => Close();
        //
        // dgv
        //
        dgv.AllowUserToAddRows = false;
        dgv.AllowUserToDeleteRows = false;
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgv.BackgroundColor = Color.White;
        dgv.BorderStyle = BorderStyle.None;
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgv.Columns.AddRange(new DataGridViewColumn[] { colId, colName, colBasePrice, colIsActive });
        dgv.Dock = DockStyle.Fill;
        dgv.EnableHeadersVisualStyles = false;
        dgv.Font = new Font("맑은 고딕", 13F);
        dgv.GridColor = Color.FromArgb(230, 230, 230);
        dgv.ReadOnly = true;
        dgv.RowHeadersVisible = false;
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgv.MultiSelect = false;
        dgv.Location = new Point(0, 72);
        dgv.Name = "dgv";
        dgv.RowTemplate.Height = 40;
        dgv.Size = new Size(584, 391);
        dgv.TabIndex = 1;
        //
        // colId
        //
        colId.DataPropertyName = "Id";
        colId.HeaderText = "번호";
        colId.Name = "colId";
        colId.ReadOnly = true;
        colId.Width = 60;
        //
        // colName
        //
        colName.DataPropertyName = "Name";
        colName.HeaderText = "신발 종류";
        colName.Name = "colName";
        colName.ReadOnly = true;
        //
        // colBasePrice
        //
        colBasePrice.DataPropertyName = "BasePrice";
        colBasePrice.HeaderText = "기본가격(원)";
        colBasePrice.Name = "colBasePrice";
        colBasePrice.ReadOnly = true;
        //
        // colIsActive
        //
        colIsActive.DataPropertyName = "IsActiveText";
        colIsActive.HeaderText = "상태";
        colIsActive.Name = "colIsActive";
        colIsActive.ReadOnly = true;
        colIsActive.Width = 80;
        //
        // PriceListForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(250, 250, 250);
        ClientSize = new Size(584, 461);
        Controls.Add(dgv);
        Controls.Add(panelTop);
        Font = new Font("맑은 고딕", 12F);
        MinimumSize = new Size(500, 400);
        StartPosition = FormStartPosition.CenterParent;
        Text = "가격표 설정";
        panelTop.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
        ResumeLayout(false);
    }

    private Panel panelTop;
    private Button btnAdd;
    private Button btnEdit;
    private Button btnInactive;
    private Button btnClose;
    private DataGridView dgv;
    private DataGridViewTextBoxColumn colId;
    private DataGridViewTextBoxColumn colName;
    private DataGridViewTextBoxColumn colBasePrice;
    private DataGridViewTextBoxColumn colIsActive;
}
