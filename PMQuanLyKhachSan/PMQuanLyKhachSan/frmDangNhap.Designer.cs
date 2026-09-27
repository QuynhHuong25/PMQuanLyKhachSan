namespace PMQuanLyKhachSan
{
    partial class frmDangNhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDangNhap));
            btnDoiMK = new Button();
            label2 = new Label();
            label1 = new Label();
            txtTenDangNhap = new TextBox();
            btnThoat = new Button();
            txtMatKhau = new TextBox();
            btnDangNhap = new Button();
            SuspendLayout();
            // 
            // btnDoiMK
            // 
            btnDoiMK.BackColor = Color.Transparent;
            btnDoiMK.FlatAppearance.BorderSize = 0;
            btnDoiMK.FlatStyle = FlatStyle.Flat;
            btnDoiMK.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnDoiMK.ForeColor = Color.FromArgb(185, 126, 50);
            btnDoiMK.Location = new Point(1074, 421);
            btnDoiMK.Name = "btnDoiMK";
            btnDoiMK.Size = new Size(138, 29);
            btnDoiMK.TabIndex = 15;
            btnDoiMK.Text = "Quên mật khẩu?";
            btnDoiMK.UseVisualStyleBackColor = false;
            btnDoiMK.Click += btnDoiMK_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(766, 379);
            label2.Name = "label2";
            label2.Size = new Size(93, 25);
            label2.TabIndex = 14;
            label2.Text = "Mật khẩu";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(766, 310);
            label1.Name = "label1";
            label1.Size = new Size(94, 25);
            label1.TabIndex = 13;
            label1.Text = "Tài khoản";
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txtTenDangNhap.Location = new Point(890, 308);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.Size = new Size(336, 31);
            txtTenDangNhap.TabIndex = 0;
            // 
            // btnThoat
            // 
            btnThoat.BackColor = Color.FromArgb(250, 240, 238);
            btnThoat.FlatAppearance.BorderSize = 0;
            btnThoat.FlatStyle = FlatStyle.Flat;
            btnThoat.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnThoat.ForeColor = Color.FromArgb(4, 17, 33);
            btnThoat.Image = (Image)resources.GetObject("btnThoat.Image");
            btnThoat.ImageAlign = ContentAlignment.MiddleRight;
            btnThoat.Location = new Point(812, 564);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(365, 61);
            btnThoat.TabIndex = 12;
            btnThoat.Text = "Thoát";
            btnThoat.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnThoat.UseVisualStyleBackColor = false;
            btnThoat.Click += btnThoat_Click;
            // 
            // txtMatKhau
            // 
            txtMatKhau.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txtMatKhau.Location = new Point(890, 375);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(336, 31);
            txtMatKhau.TabIndex = 1;
            txtMatKhau.UseSystemPasswordChar = true;
            // 
            // btnDangNhap
            // 
            btnDangNhap.BackColor = Color.FromArgb(4, 17, 33);
            btnDangNhap.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnDangNhap.ForeColor = Color.FromArgb(246, 224, 175);
            btnDangNhap.Image = (Image)resources.GetObject("btnDangNhap.Image");
            btnDangNhap.ImageAlign = ContentAlignment.MiddleRight;
            btnDangNhap.Location = new Point(812, 484);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(365, 61);
            btnDangNhap.TabIndex = 10;
            btnDangNhap.Text = "Đăng nhập";
            btnDangNhap.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDangNhap.UseVisualStyleBackColor = false;
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // frmDangNhap
            // 
            AcceptButton = btnDangNhap;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1329, 885);
            Controls.Add(btnDoiMK);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtTenDangNhap);
            Controls.Add(btnThoat);
            Controls.Add(txtMatKhau);
            Controls.Add(btnDangNhap);
            Name = "frmDangNhap";
            Text = "frmDangNhapNV";
            Load += frmDangNhapNV_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnDoiMK;
        private Label label2;
        private Label label1;
        private TextBox txtTenDangNhap;
        private Button btnThoat;
        private TextBox txtMatKhau;
        private Button btnDangNhap;
    }
}