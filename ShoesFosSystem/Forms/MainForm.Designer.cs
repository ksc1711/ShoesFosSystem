namespace ShoesFosSystem.Forms;

partial class MainForm
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
        panelTop = new TableLayoutPanel();
        lblDate = new Label();
        dtpFilter = new DateTimePicker();
        btnNewOrder = new Button();
        btnPriceList = new Button();
        btnReport = new Button();
        btnBackup = new Button();
        panelSummary = new Panel();
        lblOrderCount = new Label();
        lblTotalSales = new Label();
        lblCancelCount = new Label();
        dgvOrders = new DataGridView();
        colDailySeq = new DataGridViewTextBoxColumn();
        colCustomer = new DataGridViewTextBoxColumn();
        colTotal = new DataGridViewTextBoxColumn();
        colStatus = new DataGridViewTextBoxColumn();
        colEdit = new DataGridViewButtonColumn();
        panelTop.SuspendLayout();
        panelSummary.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
        SuspendLayout();
        //
        // panelTop (TableLayoutPanel: 2행 - 1행: 날짜+버튼, 2행: 백업)
        //
        panelTop.ColumnCount = 6;
        panelTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
        panelTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
        panelTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145F));
        panelTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145F));
        panelTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145F));
        panelTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        panelTop.RowCount = 2;
        panelTop.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        panelTop.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        panelTop.Controls.Add(lblDate, 0, 0);
        panelTop.Controls.Add(dtpFilter, 1, 0);
        panelTop.Controls.Add(btnNewOrder, 2, 0);
        panelTop.Controls.Add(btnPriceList, 3, 0);
        panelTop.Controls.Add(btnReport, 4, 0);
        panelTop.Controls.Add(btnBackup, 0, 1);
        panelTop.Dock = DockStyle.Top;
        panelTop.Padding = new Padding(12, 12, 12, 8);
        panelTop.Size = new Size(784, 108);
        panelTop.TabIndex = 0;
        //
        // lblDate
        //
        lblDate.Anchor = AnchorStyles.Left;
        lblDate.AutoSize = true;
        lblDate.Font = new Font("맑은 고딕", 16F, FontStyle.Bold);
        lblDate.Location = new Point(15, 13);
        lblDate.Name = "lblDate";
        lblDate.Size = new Size(120, 30);
        lblDate.TabIndex = 0;
        lblDate.Text = "오늘 2026-02-25";
        //
        // dtpFilter
        //
        dtpFilter.Anchor = AnchorStyles.Left;
        dtpFilter.Font = new Font("맑은 고딕", 12F);
        dtpFilter.Format = DateTimePickerFormat.Short;
        dtpFilter.Location = new Point(155, 14);
        dtpFilter.Name = "dtpFilter";
        dtpFilter.Size = new Size(140, 29);
        dtpFilter.TabIndex = 1;
        dtpFilter.ValueChanged += dtpFilter_ValueChanged;
        //
        // btnNewOrder
        //
        btnNewOrder.BackColor = Color.FromArgb(70, 130, 180);
        btnNewOrder.Dock = DockStyle.Fill;
        btnNewOrder.FlatStyle = FlatStyle.Flat;
        btnNewOrder.Font = new Font("맑은 고딕", 14F, FontStyle.Bold);
        btnNewOrder.ForeColor = Color.White;
        btnNewOrder.Location = new Point(308, 15);
        btnNewOrder.Name = "btnNewOrder";
        btnNewOrder.Size = new Size(139, 50);
        btnNewOrder.TabIndex = 2;
        btnNewOrder.Text = "새 주문";
        btnNewOrder.UseVisualStyleBackColor = false;
        btnNewOrder.Click += btnNewOrder_Click;
        //
        // btnPriceList
        //
        btnPriceList.Dock = DockStyle.Fill;
        btnPriceList.Font = new Font("맑은 고딕", 14F, FontStyle.Bold);
        btnPriceList.Location = new Point(453, 15);
        btnPriceList.Name = "btnPriceList";
        btnPriceList.Size = new Size(139, 50);
        btnPriceList.TabIndex = 3;
        btnPriceList.Text = "가격표 설정";
        btnPriceList.UseVisualStyleBackColor = true;
        btnPriceList.Click += btnPriceList_Click;
        //
        // btnReport
        //
        btnReport.Dock = DockStyle.Fill;
        btnReport.Font = new Font("맑은 고딕", 14F, FontStyle.Bold);
        btnReport.Location = new Point(598, 15);
        btnReport.Name = "btnReport";
        btnReport.Size = new Size(139, 50);
        btnReport.TabIndex = 4;
        btnReport.Text = "매출/리포트";
        btnReport.UseVisualStyleBackColor = true;
        btnReport.Click += btnReport_Click;
        //
        // btnBackup
        //
        btnBackup.Anchor = AnchorStyles.Left;
        btnBackup.Font = new Font("맑은 고딕", 12F, FontStyle.Bold);
        btnBackup.Location = new Point(15, 64);
        btnBackup.Name = "btnBackup";
        btnBackup.Size = new Size(160, 36);
        btnBackup.TabIndex = 5;
        btnBackup.Text = "백업 폴더 열기";
        btnBackup.UseVisualStyleBackColor = true;
        btnBackup.Click += btnBackup_Click;
        //
        // panelSummary
        //
        panelSummary.BackColor = Color.FromArgb(245, 245, 245);
        panelSummary.Controls.Add(lblOrderCount);
        panelSummary.Controls.Add(lblTotalSales);
        panelSummary.Controls.Add(lblCancelCount);
        panelSummary.Dock = DockStyle.Top;
        panelSummary.Padding = new Padding(16, 12, 16, 12);
        panelSummary.Size = new Size(784, 56);
        panelSummary.TabIndex = 1;
        //
        // lblOrderCount
        //
        lblOrderCount.AutoSize = true;
        lblOrderCount.Font = new Font("맑은 고딕", 14F, FontStyle.Bold);
        lblOrderCount.Location = new Point(16, 14);
        lblOrderCount.Name = "lblOrderCount";
        lblOrderCount.Size = new Size(180, 25);
        lblOrderCount.TabIndex = 0;
        lblOrderCount.Text = "주문 수(취소 제외): 0건";
        //
        // lblTotalSales
        //
        lblTotalSales.AutoSize = true;
        lblTotalSales.Font = new Font("맑은 고딕", 14F, FontStyle.Bold);
        lblTotalSales.ForeColor = Color.FromArgb(0, 100, 0);
        lblTotalSales.Location = new Point(220, 14);
        lblTotalSales.Name = "lblTotalSales";
        lblTotalSales.Size = new Size(140, 25);
        lblTotalSales.TabIndex = 1;
        lblTotalSales.Text = "총 매출: 0원";
        //
        // lblCancelCount
        //
        lblCancelCount.AutoSize = true;
        lblCancelCount.Font = new Font("맑은 고딕", 12F);
        lblCancelCount.ForeColor = Color.Gray;
        lblCancelCount.Location = new Point(440, 16);
        lblCancelCount.Name = "lblCancelCount";
        lblCancelCount.Size = new Size(100, 21);
        lblCancelCount.TabIndex = 2;
        lblCancelCount.Text = "취소: 0건(참고)";
        //
        // dgvOrders
        //
        dgvOrders.AllowUserToAddRows = false;
        dgvOrders.AllowUserToDeleteRows = false;
        dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        colOrderId = new DataGridViewTextBoxColumn();
        colOrderId.Name = "colOrderId";
        colOrderId.Visible = false;
        dgvOrders.Columns.AddRange(new DataGridViewColumn[] { colDailySeq, colCustomer, colTotal, colStatus, colEdit, colOrderId });
        dgvOrders.Dock = DockStyle.Fill;
        dgvOrders.Font = new Font("맑은 고딕", 13F);
        dgvOrders.ReadOnly = false;
        dgvOrders.RowHeadersVisible = false;
        dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvOrders.MultiSelect = false;
        dgvOrders.Location = new Point(0, 156);
        dgvOrders.Name = "dgvOrders";
        dgvOrders.RowTemplate.Height = 44;
        dgvOrders.Size = new Size(784, 394);
        dgvOrders.TabIndex = 2;
        dgvOrders.CellClick += dgvOrders_CellClick;
        //
        // colDailySeq
        //
        colDailySeq.HeaderText = "접수번호";
        colDailySeq.Name = "colDailySeq";
        colDailySeq.ReadOnly = true;
        colDailySeq.Width = 90;
        //
        // colCustomer
        //
        colCustomer.HeaderText = "고객명";
        colCustomer.Name = "colCustomer";
        colCustomer.ReadOnly = true;
        //
        // colTotal
        //
        colTotal.HeaderText = "합계(원)";
        colTotal.Name = "colTotal";
        colTotal.ReadOnly = true;
        colTotal.Width = 120;
        //
        // colStatus
        //
        colStatus.HeaderText = "상태";
        colStatus.Name = "colStatus";
        colStatus.ReadOnly = true;
        colStatus.Width = 90;
        //
        // colEdit
        //
        colEdit.HeaderText = "";
        colEdit.Name = "colEdit";
        colEdit.ReadOnly = true;
        colEdit.Text = "수정";
        colEdit.UseColumnTextForButtonValue = true;
        colEdit.Width = 70;
        //
        // MainForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(784, 550);
        Controls.Add(dgvOrders);
        Controls.Add(panelSummary);
        Controls.Add(panelTop);
        Font = new Font("맑은 고딕", 12F);
        MinimumSize = new Size(700, 500);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "신발빨래방 POS";
        panelTop.ResumeLayout(false);
        panelSummary.ResumeLayout(false);
        panelSummary.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
        ResumeLayout(false);
    }

    private TableLayoutPanel panelTop;
    private Label lblDate;
    private DateTimePicker dtpFilter;
    private Button btnNewOrder;
    private Button btnPriceList;
    private Button btnReport;
    private Button btnBackup;
    private Panel panelSummary;
    private Label lblOrderCount;
    private Label lblTotalSales;
    private Label lblCancelCount;
    private DataGridView dgvOrders;
    private DataGridViewTextBoxColumn colDailySeq;
    private DataGridViewTextBoxColumn colCustomer;
    private DataGridViewTextBoxColumn colTotal;
    private DataGridViewTextBoxColumn colStatus;
    private DataGridViewButtonColumn colEdit;
    private DataGridViewTextBoxColumn colOrderId;
}
