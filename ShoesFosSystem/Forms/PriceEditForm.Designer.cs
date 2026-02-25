namespace ShoesFosSystem.Forms;

partial class PriceEditForm
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
        labelName = new Label();
        txtName = new TextBox();
        labelPrice = new Label();
        numPrice = new NumericUpDown();
        chkActive = new CheckBox();
        btnOk = new Button();
        btnCancel = new Button();
        ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
        SuspendLayout();
        //
        // labelName
        //
        labelName.AutoSize = true;
        labelName.Font = new Font("맑은 고딕", 14F);
        labelName.Location = new Point(24, 24);
        labelName.Name = "labelName";
        labelName.Size = new Size(107, 25);
        labelName.TabIndex = 0;
        labelName.Text = "신발 종류명";
        //
        // txtName
        //
        txtName.Font = new Font("맑은 고딕", 14F);
        txtName.Location = new Point(24, 54);
        txtName.Name = "txtName";
        txtName.Size = new Size(320, 32);
        txtName.TabIndex = 1;
        //
        // labelPrice
        //
        labelPrice.AutoSize = true;
        labelPrice.Font = new Font("맑은 고딕", 14F);
        labelPrice.Location = new Point(24, 100);
        labelPrice.Name = "labelPrice";
        labelPrice.Size = new Size(121, 25);
        labelPrice.TabIndex = 2;
        labelPrice.Text = "기본가격(원)";
        //
        // numPrice
        //
        numPrice.Font = new Font("맑은 고딕", 14F);
        numPrice.Location = new Point(24, 130);
        numPrice.Maximum = 1000000;
        numPrice.Minimum = 0;
        numPrice.Name = "numPrice";
        numPrice.Size = new Size(160, 32);
        numPrice.TabIndex = 3;
        numPrice.Value = 10000;
        //
        // chkActive
        //
        chkActive.AutoSize = true;
        chkActive.Checked = true;
        chkActive.CheckState = CheckState.Checked;
        chkActive.Font = new Font("맑은 고딕", 14F);
        chkActive.Location = new Point(24, 180);
        chkActive.Name = "chkActive";
        chkActive.Size = new Size(118, 29);
        chkActive.TabIndex = 4;
        chkActive.Text = "판매 중";
        chkActive.UseVisualStyleBackColor = true;
        //
        // btnOk
        //
        btnOk.Font = new Font("맑은 고딕", 14F, FontStyle.Bold);
        btnOk.Location = new Point(24, 230);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(120, 50);
        btnOk.TabIndex = 5;
        btnOk.Text = "확인";
        btnOk.UseVisualStyleBackColor = true;
        btnOk.Click += btnOk_Click;
        //
        // btnCancel
        //
        btnCancel.Font = new Font("맑은 고딕", 14F, FontStyle.Bold);
        btnCancel.Location = new Point(160, 230);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(120, 50);
        btnCancel.TabIndex = 6;
        btnCancel.Text = "취소";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
        //
        // PriceEditForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(384, 301);
        Controls.Add(btnCancel);
        Controls.Add(btnOk);
        Controls.Add(chkActive);
        Controls.Add(numPrice);
        Controls.Add(labelPrice);
        Controls.Add(txtName);
        Controls.Add(labelName);
        Font = new Font("맑은 고딕", 12F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "가격표 추가/수정";
        ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label labelName;
    private TextBox txtName;
    private Label labelPrice;
    private NumericUpDown numPrice;
    private CheckBox chkActive;
    private Button btnOk;
    private Button btnCancel;
}
