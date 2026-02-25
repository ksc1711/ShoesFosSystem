namespace ShoesFosSystem.Forms;

partial class ReportForm
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
        lblStart = new Label();
        dtpStart = new DateTimePicker();
        lblEnd = new Label();
        dtpEnd = new DateTimePicker();
        btnSearch = new Button();
        lblPeriodSales = new Label();
        panelDaily = new Panel();
        lblDailyTitle = new Label();
        dgvDaily = new DataGridView();
        colDate = new DataGridViewTextBoxColumn();
        colSales = new DataGridViewTextBoxColumn();
        panelByType = new Panel();
        lblByTypeTitle = new Label();
        dgvByType = new DataGridView();
        colTypeName = new DataGridViewTextBoxColumn();
        colTypeSales = new DataGridViewTextBoxColumn();
        btnExportCsv = new Button();
        btnClose = new Button();
        panelTop.SuspendLayout();
        panelDaily.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvDaily).BeginInit();
        panelByType.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvByType).BeginInit();
        SuspendLayout();
        //
        // panelTop
        //
        panelTop.Controls.Add(lblStart);
        panelTop.Controls.Add(dtpStart);
        panelTop.Controls.Add(lblEnd);
        panelTop.Controls.Add(dtpEnd);
        panelTop.Controls.Add(btnSearch);
        panelTop.Controls.Add(lblPeriodSales);
        panelTop.Dock = DockStyle.Top;
        panelTop.Padding = new Padding(16, 16, 16, 8);
        panelTop.Size = new Size(684, 70);
        panelTop.TabIndex = 0;
        //
        // lblStart
        //
        lblStart.AutoSize = true;
        lblStart.Font = new Font("맑은 고딕", 13F);
        lblStart.Location = new Point(16, 20);
        lblStart.Name = "lblStart";
        lblStart.Size = new Size(69, 25);
        lblStart.TabIndex = 0;
        lblStart.Text = "시작일";
        //
        // dtpStart
        //
        dtpStart.Font = new Font("맑은 고딕", 12F);
        dtpStart.Format = DateTimePickerFormat.Short;
        dtpStart.Location = new Point(90, 16);
        dtpStart.Name = "dtpStart";
        dtpStart.Size = new Size(130, 29);
        dtpStart.TabIndex = 1;
        //
        // lblEnd
        //
        lblEnd.AutoSize = true;
        lblEnd.Font = new Font("맑은 고딕", 13F);
        lblEnd.Location = new Point(240, 20);
        lblEnd.Name = "lblEnd";
        lblEnd.Size = new Size(69, 25);
        lblEnd.TabIndex = 2;
        lblEnd.Text = "종료일";
        //
        // dtpEnd
        //
        dtpEnd.Font = new Font("맑은 고딕", 12F);
        dtpEnd.Format = DateTimePickerFormat.Short;
        dtpEnd.Location = new Point(314, 16);
        dtpEnd.Name = "dtpEnd";
        dtpEnd.Size = new Size(130, 29);
        dtpEnd.TabIndex = 3;
        //
        // btnSearch
        //
        btnSearch.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
        btnSearch.Location = new Point(456, 12);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(100, 40);
        btnSearch.TabIndex = 4;
        btnSearch.Text = "조회";
        btnSearch.UseVisualStyleBackColor = true;
        btnSearch.Click += btnSearch_Click;
        //
        // lblPeriodSales
        //
        lblPeriodSales.AutoSize = true;
        lblPeriodSales.Font = new Font("맑은 고딕", 14F, FontStyle.Bold);
        lblPeriodSales.ForeColor = Color.FromArgb(0, 100, 0);
        lblPeriodSales.Location = new Point(16, 48);
        lblPeriodSales.Name = "lblPeriodSales";
        lblPeriodSales.Size = new Size(180, 25);
        lblPeriodSales.TabIndex = 5;
        lblPeriodSales.Text = "기간 총매출(취소제외): 0원";
        //
        // panelDaily
        //
        panelDaily.Controls.Add(lblDailyTitle);
        panelDaily.Controls.Add(dgvDaily);
        panelDaily.Dock = DockStyle.Top;
        panelDaily.Padding = new Padding(16, 8, 16, 8);
        panelDaily.Size = new Size(684, 220);
        panelDaily.TabIndex = 1;
        //
        // lblDailyTitle
        //
        lblDailyTitle.AutoSize = true;
        lblDailyTitle.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
        lblDailyTitle.Location = new Point(16, 8);
        lblDailyTitle.Name = "lblDailyTitle";
        lblDailyTitle.Size = new Size(121, 25);
        lblDailyTitle.TabIndex = 0;
        lblDailyTitle.Text = "일자별 매출";
        //
        // dgvDaily
        //
        dgvDaily.AllowUserToAddRows = false;
        dgvDaily.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvDaily.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvDaily.Columns.AddRange(new DataGridViewColumn[] { colDate, colSales });
        dgvDaily.Dock = DockStyle.Bottom;
        dgvDaily.Location = new Point(16, 40);
        dgvDaily.Name = "dgvDaily";
        dgvDaily.ReadOnly = true;
        dgvDaily.RowHeadersVisible = false;
        dgvDaily.RowTemplate.Height = 36;
        dgvDaily.Size = new Size(652, 172);
        dgvDaily.TabIndex = 1;
        //
        // colDate
        //
        colDate.HeaderText = "일자";
        colDate.Name = "colDate";
        colDate.ReadOnly = true;
        colDate.Width = 120;
        //
        // colSales
        //
        colSales.HeaderText = "매출(원)";
        colSales.Name = "colSales";
        colSales.ReadOnly = true;
        //
        // panelByType
        //
        panelByType.Controls.Add(lblByTypeTitle);
        panelByType.Controls.Add(dgvByType);
        panelByType.Dock = DockStyle.Top;
        panelByType.Padding = new Padding(16, 8, 16, 8);
        panelByType.Size = new Size(684, 200);
        panelByType.TabIndex = 2;
        //
        // lblByTypeTitle
        //
        lblByTypeTitle.AutoSize = true;
        lblByTypeTitle.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
        lblByTypeTitle.Location = new Point(16, 8);
        lblByTypeTitle.Name = "lblByTypeTitle";
        lblByTypeTitle.Size = new Size(164, 25);
        lblByTypeTitle.TabIndex = 0;
        lblByTypeTitle.Text = "신발종류별 매출";
        //
        // dgvByType
        //
        dgvByType.AllowUserToAddRows = false;
        dgvByType.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvByType.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvByType.Columns.AddRange(new DataGridViewColumn[] { colTypeName, colTypeSales });
        dgvByType.Location = new Point(16, 40);
        dgvByType.Name = "dgvByType";
        dgvByType.ReadOnly = true;
        dgvByType.RowHeadersVisible = false;
        dgvByType.RowTemplate.Height = 36;
        dgvByType.Size = new Size(652, 152);
        dgvByType.TabIndex = 1;
        //
        // colTypeName
        //
        colTypeName.HeaderText = "신발 종류";
        colTypeName.Name = "colTypeName";
        colTypeName.ReadOnly = true;
        //
        // colTypeSales
        //
        colTypeSales.HeaderText = "매출(원)";
        colTypeSales.Name = "colTypeSales";
        colTypeSales.ReadOnly = true;
        //
        // btnExportCsv
        //
        btnExportCsv.Font = new Font("맑은 고딕", 14F, FontStyle.Bold);
        btnExportCsv.Location = new Point(16, 430);
        btnExportCsv.Name = "btnExportCsv";
        btnExportCsv.Size = new Size(180, 50);
        btnExportCsv.TabIndex = 3;
        btnExportCsv.Text = "CSV 내보내기";
        btnExportCsv.UseVisualStyleBackColor = true;
        btnExportCsv.Click += btnExportCsv_Click;
        //
        // btnClose
        //
        btnClose.Font = new Font("맑은 고딕", 14F, FontStyle.Bold);
        btnClose.Location = new Point(520, 430);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(120, 50);
        btnClose.TabIndex = 4;
        btnClose.Text = "닫기";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += (s, e) => Close();
        //
        // ReportForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(684, 500);
        Controls.Add(btnClose);
        Controls.Add(btnExportCsv);
        Controls.Add(panelByType);
        Controls.Add(panelDaily);
        Controls.Add(panelTop);
        Font = new Font("맑은 고딕", 12F);
        MinimumSize = new Size(600, 480);
        StartPosition = FormStartPosition.CenterParent;
        Text = "매출 / 리포트";
        panelTop.ResumeLayout(false);
        panelTop.PerformLayout();
        panelDaily.ResumeLayout(false);
        panelDaily.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvDaily).EndInit();
        panelByType.ResumeLayout(false);
        panelByType.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvByType).EndInit();
        ResumeLayout(false);
    }

    private Panel panelTop;
    private Label lblStart;
    private DateTimePicker dtpStart;
    private Label lblEnd;
    private DateTimePicker dtpEnd;
    private Button btnSearch;
    private Label lblPeriodSales;
    private Panel panelDaily;
    private Label lblDailyTitle;
    private DataGridView dgvDaily;
    private DataGridViewTextBoxColumn colDate;
    private DataGridViewTextBoxColumn colSales;
    private Panel panelByType;
    private Label lblByTypeTitle;
    private DataGridView dgvByType;
    private DataGridViewTextBoxColumn colTypeName;
    private DataGridViewTextBoxColumn colTypeSales;
    private Button btnExportCsv;
    private Button btnClose;
}
