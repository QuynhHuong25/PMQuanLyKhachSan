namespace PMQuanLyKhachSan
{
    partial class frmDoiMK
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDoiMK));
            txtTenDangNhap = new TextBox();
            txtMKMoi = new TextBox();
            txtXacNhan = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnDoiMK = new Button();
            btnQuayLai = new Button();
            SuspendLayout();
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Location = new Point(272, 314);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.Size = new Size(283, 30);
            txtTenDangNhap.TabIndex = 0;
            // 
            // txtMKMoi
            // 
            txtMKMoi.Location = new Point(272, 398);
            txtMKMoi.Name = "txtMKMoi";
            txtMKMoi.Size = new Size(283, 30);
            txtMKMoi.TabIndex = 1;
            txtMKMoi.UseSystemPasswordChar = true;
            // 
            // txtXacNhan
            // 
            txtXacNhan.Location = new Point(272, 481);
            txtXacNhan.Name = "txtXacNhan";
            txtXacNhan.Size = new Size(283, 30);
            txtXacNhan.TabIndex = 2;
            txtXacNhan.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(110, 316);
            label1.Name = "label1";
            label1.Size = new Size(134, 25);
            label1.TabIndex = 3;
            label1.Text = "Tên đăng nhập";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(111, 398);
            label2.Name = "label2";
            label2.Size = new Size(126, 25);
            label2.TabIndex = 4;
            label2.Text = "Mật khẩu mới";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(59, 483);
            label3.Name = "label3";
            label3.Size = new Size(204, 25);
            label3.TabIndex = 5;
            label3.Text = "Xác nhận mật khẩu mới";
            // 
            // btnDoiMK
            // 
            btnDoiMK.BackColor = Color.FromArgb(4, 17, 33);
            btnDoiMK.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnDoiMK.ForeColor = Color.FromArgb(246, 224, 175);
            btnDoiMK.Image = (Image)resources.GetObject("btnDoiMK.Image");
            btnDoiMK.ImageAlign = ContentAlignment.MiddleRight;
            btnDoiMK.Location = new Point(97, 703);
            btnDoiMK.Name = "btnDoiMK";
            btnDoiMK.Size = new Size(458, 70);
            btnDoiMK.TabIndex = 11;
            btnDoiMK.Text = "Đổi mật khẩu";
            btnDoiMK.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDoiMK.UseVisualStyleBackColor = false;
            btnDoiMK.Click += btnDoiMK_Click;
            // 
            // btnQuayLai
            // 
            btnQuayLai.BackColor = Color.Transparent;
            btnQuayLai.FlatAppearance.BorderSize = 0;
            btnQuayLai.FlatStyle = FlatStyle.Flat;
            btnQuayLai.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnQuayLai.ForeColor = Color.FromArgb(145, 91, 17);
            btnQuayLai.Image = (Image)resources.GetObject("btnQuayLai.Image");
            btnQuayLai.ImageAlign = ContentAlignment.TopLeft;
            btnQuayLai.Location = new Point(75, 59);
            btnQuayLai.Name = "btnQuayLai";
            btnQuayLai.Size = new Size(224, 31);
            btnQuayLai.TabIndex = 12;
            btnQuayLai.Text = "Quay lại đăng nhập";
            btnQuayLai.TextAlign = ContentAlignment.BottomCenter;
            btnQuayLai.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnQuayLai.UseVisualStyleBackColor = false;
            btnQuayLai.Click += btnQuayLai_Click;
            // 
            // frmDoiMK
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1315, 876);
            Controls.Add(btnQuayLai);
            Controls.Add(btnDoiMK);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtXacNhan);
            Controls.Add(txtMKMoi);
            Controls.Add(txtTenDangNhap);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "frmDoiMK";
            Text = "frmDoiMK";
            Load += frmDoiMK_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTenDangNhap;
        private TextBox txtMKMoi;
        private TextBox txtXacNhan;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnDoiMK;
        private Button btnQuayLai;
    }
}