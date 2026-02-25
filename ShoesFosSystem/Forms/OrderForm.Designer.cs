namespace ShoesFosSystem.Forms;

partial class OrderForm
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
        panelHead = new Panel();
        lblSeq = new Label();
        lblBizDate = new Label();
        txtCustomerName = new TextBox();
        lblCustomerName = new Label();
        txtCustomerPhone = new TextBox();
        lblCustomerPhone = new Label();
        panelItems = new Panel();
        dgvItems = new DataGridView();
        btnAddItem = new Button();
        btnDeleteItem = new Button();
        lblExtraPreset = new Label();
        btnExtra3000 = new Button();
        btnExtra5000 = new Button();
        btnExtra10000 = new Button();
        panelTotal = new Panel();
        lblTotalLabel = new Label();
        lblTotalAmount = new Label();
        txtOrderMemo = new TextBox();
        lblOrderMemo = new Label();
        panelButtons = new Panel();
        btnSave = new Button();
        btnSaveComplete = new Button();
        btnCancelOrder = new Button();
        btnClose = new Button();
        panelHead.SuspendLayout();
        panelItems.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
        panelTotal.SuspendLayout();
        panelButtons.SuspendLayout();
        SuspendLayout();
        //
        // panelHead
        //
        panelHead.Controls.Add(lblSeq);
        panelHead.Controls.Add(lblBizDate);
        panelHead.Controls.Add(txtCustomerName);
        panelHead.Controls.Add(lblCustomerName);
        panelHead.Controls.Add(txtCustomerPhone);
        panelHead.Controls.Add(lblCustomerPhone);
        panelHead.Dock = DockStyle.Top;
        panelHead.Padding = new Padding(16, 12, 16, 8);
        panelHead.Size = new Size(784, 120);
        panelHead.TabIndex = 0;
        //
        // lblSeq
        //
        lblSeq.AutoSize = true;
        lblSeq.Font = new Font("맑은 고딕", 14F, FontStyle.Bold);
        lblSeq.Location = new Point(16, 12);
        lblSeq.Name = "lblSeq";
        lblSeq.Size = new Size(100, 25);
        lblSeq.TabIndex = 0;
        lblSeq.Text = "오늘 1번";
        //
        // lblBizDate
        //
        lblBizDate.AutoSize = true;
        lblBizDate.Font = new Font("맑은 고딕", 12F);
        lblBizDate.ForeColor = Color.Gray;
        lblBizDate.Location = new Point(130, 16);
        lblBizDate.Name = "lblBizDate";
        lblBizDate.Size = new Size(80, 21);
        lblBizDate.TabIndex = 1;
        lblBizDate.Text = "2026-02-25";
        //
        // lblCustomerName
        //
        lblCustomerName.AutoSize = true;
        lblCustomerName.Font = new Font("맑은 고딕", 13F);
        lblCustomerName.Location = new Point(16, 50);
        lblCustomerName.Name = "lblCustomerName";
        lblCustomerName.Size = new Size(69, 25);
        lblCustomerName.TabIndex = 2;
        lblCustomerName.Text = "고객명";
        //
        // txtCustomerName
        //
        txtCustomerName.Font = new Font("맑은 고딕", 13F);
        txtCustomerName.Location = new Point(100, 47);
        txtCustomerName.Name = "txtCustomerName";
        txtCustomerName.Size = new Size(200, 31);
        txtCustomerName.TabIndex = 3;
        //
        // lblCustomerPhone
        //
        lblCustomerPhone.AutoSize = true;
        lblCustomerPhone.Font = new Font("맑은 고딕", 13F);
        lblCustomerPhone.Location = new Point(320, 50);
        lblCustomerPhone.Name = "lblCustomerPhone";
        lblCustomerPhone.Size = new Size(52, 25);
        lblCustomerPhone.TabIndex = 4;
        lblCustomerPhone.Text = "전화";
        //
        // txtCustomerPhone
        //
        txtCustomerPhone.Font = new Font("맑은 고딕", 13F);
        txtCustomerPhone.Location = new Point(380, 47);
        txtCustomerPhone.Name = "txtCustomerPhone";
        txtCustomerPhone.Size = new Size(180, 31);
        txtCustomerPhone.TabIndex = 5;
        //
        // panelItems
        //
        panelItems.Controls.Add(dgvItems);
        panelItems.Controls.Add(btnAddItem);
        panelItems.Controls.Add(btnDeleteItem);
        panelItems.Controls.Add(lblExtraPreset);
        panelItems.Controls.Add(btnExtra3000);
        panelItems.Controls.Add(btnExtra5000);
        panelItems.Controls.Add(btnExtra10000);
        panelItems.Dock = DockStyle.Fill;
        panelItems.Padding = new Padding(16, 8, 16, 8);
        panelItems.Size = new Size(784, 280);
        panelItems.TabIndex = 1;
        //
        // dgvItems
        //
        dgvItems.AllowUserToAddRows = false;
        dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvItems.Dock = DockStyle.Fill;
        dgvItems.Font = new Font("맑은 고딕", 12F);
        dgvItems.Location = new Point(16, 8);
        dgvItems.Name = "dgvItems";
        dgvItems.RowHeadersVisible = false;
        dgvItems.RowTemplate.Height = 38;
        dgvItems.Size = new Size(752, 180);
        dgvItems.TabIndex = 0;
        dgvItems.CellEndEdit += dgvItems_CellEndEdit;
        dgvItems.SelectionChanged += dgvItems_SelectionChanged;
        //
        // btnAddItem
        //
        btnAddItem.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
        btnAddItem.Location = new Point(16, 196);
        btnAddItem.Name = "btnAddItem";
        btnAddItem.Size = new Size(120, 44);
        btnAddItem.TabIndex = 1;
        btnAddItem.Text = "품목 추가";
        btnAddItem.UseVisualStyleBackColor = true;
        btnAddItem.Click += btnAddItem_Click;
        //
        // btnDeleteItem
        //
        btnDeleteItem.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
        btnDeleteItem.Location = new Point(146, 196);
        btnDeleteItem.Name = "btnDeleteItem";
        btnDeleteItem.Size = new Size(120, 44);
        btnDeleteItem.TabIndex = 2;
        btnDeleteItem.Text = "품목 삭제";
        btnDeleteItem.UseVisualStyleBackColor = true;
        btnDeleteItem.Click += btnDeleteItem_Click;
        //
        // lblExtraPreset
        //
        lblExtraPreset.AutoSize = true;
        lblExtraPreset.Font = new Font("맑은 고딕", 12F);
        lblExtraPreset.Location = new Point(290, 206);
        lblExtraPreset.Name = "lblExtraPreset";
        lblExtraPreset.Size = new Size(122, 21);
        lblExtraPreset.TabIndex = 3;
        lblExtraPreset.Text = "추가금 프리셋:";
        //
        // btnExtra3000
        //
        btnExtra3000.Font = new Font("맑은 고딕", 12F, FontStyle.Bold);
        btnExtra3000.Location = new Point(418, 196);
        btnExtra3000.Name = "btnExtra3000";
        btnExtra3000.Size = new Size(70, 44);
        btnExtra3000.TabIndex = 4;
        btnExtra3000.Text = "+3000";
        btnExtra3000.UseVisualStyleBackColor = true;
        btnExtra3000.Click += btnExtra3000_Click;
        //
        // btnExtra5000
        //
        btnExtra5000.Font = new Font("맑은 고딕", 12F, FontStyle.Bold);
        btnExtra5000.Location = new Point(494, 196);
        btnExtra5000.Name = "btnExtra5000";
        btnExtra5000.Size = new Size(70, 44);
        btnExtra5000.TabIndex = 5;
        btnExtra5000.Text = "+5000";
        btnExtra5000.UseVisualStyleBackColor = true;
        btnExtra5000.Click += btnExtra5000_Click;
        //
        // btnExtra10000
        //
        btnExtra10000.Font = new Font("맑은 고딕", 12F, FontStyle.Bold);
        btnExtra10000.Location = new Point(570, 196);
        btnExtra10000.Name = "btnExtra10000";
        btnExtra10000.Size = new Size(80, 44);
        btnExtra10000.TabIndex = 6;
        btnExtra10000.Text = "+10000";
        btnExtra10000.UseVisualStyleBackColor = true;
        btnExtra10000.Click += btnExtra10000_Click;
        //
        // panelTotal
        //
        panelTotal.BackColor = Color.FromArgb(245, 245, 245);
        panelTotal.Controls.Add(lblTotalLabel);
        panelTotal.Controls.Add(lblTotalAmount);
        panelTotal.Controls.Add(txtOrderMemo);
        panelTotal.Controls.Add(lblOrderMemo);
        panelTotal.Dock = DockStyle.Top;
        panelTotal.Padding = new Padding(16, 10, 16, 10);
        panelTotal.Size = new Size(784, 90);
        panelTotal.TabIndex = 2;
        //
        // lblTotalLabel
        //
        lblTotalLabel.AutoSize = true;
        lblTotalLabel.Font = new Font("맑은 고딕", 14F, FontStyle.Bold);
        lblTotalLabel.Location = new Point(16, 14);
        lblTotalLabel.Name = "lblTotalLabel";
        lblTotalLabel.Size = new Size(69, 25);
        lblTotalLabel.TabIndex = 0;
        lblTotalLabel.Text = "합계:";
        //
        // lblTotalAmount
        //
        lblTotalAmount.AutoSize = true;
        lblTotalAmount.Font = new Font("맑은 고딕", 18F, FontStyle.Bold);
        lblTotalAmount.ForeColor = Color.FromArgb(0, 80, 0);
        lblTotalAmount.Location = new Point(100, 10);
        lblTotalAmount.Name = "lblTotalAmount";
        lblTotalAmount.Size = new Size(100, 32);
        lblTotalAmount.TabIndex = 1;
        lblTotalAmount.Text = "0원";
        //
        // lblOrderMemo
        //
        lblOrderMemo.AutoSize = true;
        lblOrderMemo.Font = new Font("맑은 고딕", 12F);
        lblOrderMemo.Location = new Point(16, 52);
        lblOrderMemo.Name = "lblOrderMemo";
        lblOrderMemo.Size = new Size(82, 21);
        lblOrderMemo.TabIndex = 2;
        lblOrderMemo.Text = "주문 메모";
        //
        // txtOrderMemo
        //
        txtOrderMemo.Font = new Font("맑은 고딕", 12F);
        txtOrderMemo.Location = new Point(100, 49);
        txtOrderMemo.Name = "txtOrderMemo";
        txtOrderMemo.Size = new Size(668, 29);
        txtOrderMemo.TabIndex = 3;
        //
        // panelButtons
        //
        btnDeleteOrder = new Button();
        btnPrint = new Button();
        panelButtons.Controls.Add(btnSave);
        panelButtons.Controls.Add(btnSaveComplete);
        panelButtons.Controls.Add(btnCancelOrder);
        panelButtons.Controls.Add(btnDeleteOrder);
        panelButtons.Controls.Add(btnPrint);
        panelButtons.Controls.Add(btnClose);
        panelButtons.Dock = DockStyle.Bottom;
        panelButtons.Padding = new Padding(16, 12, 16, 16);
        panelButtons.Size = new Size(784, 80);
        panelButtons.TabIndex = 3;
        //
        // btnSave
        //
        btnSave.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
        btnSave.Location = new Point(16, 12);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(100, 52);
        btnSave.TabIndex = 0;
        btnSave.Text = "저장";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += btnSave_Click;
        //
        // btnSaveComplete
        //
        btnSaveComplete.BackColor = Color.FromArgb(70, 130, 180);
        btnSaveComplete.FlatStyle = FlatStyle.Flat;
        btnSaveComplete.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
        btnSaveComplete.ForeColor = Color.White;
        btnSaveComplete.Location = new Point(122, 12);
        btnSaveComplete.Name = "btnSaveComplete";
        btnSaveComplete.Size = new Size(120, 52);
        btnSaveComplete.TabIndex = 1;
        btnSaveComplete.Text = "저장 + 완료";
        btnSaveComplete.UseVisualStyleBackColor = false;
        btnSaveComplete.Click += btnSaveComplete_Click;
        //
        // btnCancelOrder
        //
        btnCancelOrder.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
        btnCancelOrder.ForeColor = Color.DarkRed;
        btnCancelOrder.Location = new Point(248, 12);
        btnCancelOrder.Name = "btnCancelOrder";
        btnCancelOrder.Size = new Size(100, 52);
        btnCancelOrder.TabIndex = 2;
        btnCancelOrder.Text = "취소처리";
        btnCancelOrder.UseVisualStyleBackColor = true;
        btnCancelOrder.Click += btnCancelOrder_Click;
        //
        // btnDeleteOrder
        //
        btnDeleteOrder.Font = new Font("맑은 고딕", 12F, FontStyle.Bold);
        btnDeleteOrder.ForeColor = Color.DarkRed;
        btnDeleteOrder.Location = new Point(354, 12);
        btnDeleteOrder.Name = "btnDeleteOrder";
        btnDeleteOrder.Size = new Size(90, 52);
        btnDeleteOrder.TabIndex = 3;
        btnDeleteOrder.Text = "주문 삭제";
        btnDeleteOrder.UseVisualStyleBackColor = true;
        btnDeleteOrder.Click += btnDeleteOrder_Click;
        //
        // btnPrint
        //
        btnPrint.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
        btnPrint.Location = new Point(450, 12);
        btnPrint.Name = "btnPrint";
        btnPrint.Size = new Size(90, 52);
        btnPrint.TabIndex = 4;
        btnPrint.Text = "출력";
        btnPrint.UseVisualStyleBackColor = true;
        btnPrint.Click += btnPrint_Click;
        //
        // btnClose
        //
        btnClose.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
        btnClose.Location = new Point(546, 12);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(80, 52);
        btnClose.TabIndex = 5;
        btnClose.Text = "닫기";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
        //
        // OrderForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(784, 570);
        Controls.Add(panelItems);
        Controls.Add(panelTotal);
        Controls.Add(panelHead);
        Controls.Add(panelButtons);
        Font = new Font("맑은 고딕", 12F);
        MinimumSize = new Size(750, 550);
        StartPosition = FormStartPosition.CenterParent;
        Text = "주문";
        panelHead.ResumeLayout(false);
        panelHead.PerformLayout();
        panelItems.ResumeLayout(false);
        panelItems.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
        panelTotal.ResumeLayout(false);
        panelTotal.PerformLayout();
        panelButtons.ResumeLayout(false);
        ResumeLayout(false);
    }

    private Panel panelHead;
    private Label lblSeq;
    private Label lblBizDate;
    private TextBox txtCustomerName;
    private Label lblCustomerName;
    private TextBox txtCustomerPhone;
    private Label lblCustomerPhone;
    private Panel panelItems;
    private DataGridView dgvItems;
    private Button btnAddItem;
    private Button btnDeleteItem;
    private Label lblExtraPreset;
    private Button btnExtra3000;
    private Button btnExtra5000;
    private Button btnExtra10000;
    private Panel panelTotal;
    private Label lblTotalLabel;
    private Label lblTotalAmount;
    private TextBox txtOrderMemo;
    private Label lblOrderMemo;
    private Panel panelButtons;
    private Button btnSave;
    private Button btnSaveComplete;
    private Button btnCancelOrder;
    private Button btnDeleteOrder;
    private Button btnPrint;
    private Button btnClose;
}
