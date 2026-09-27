namespace PMQuanLyKhachSan
{
    partial class frmXemLichSu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmXemLichSu));
            label7 = new Label();
            dtpThoiGian = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            btnTim = new Button();
            dgvLichSu = new DataGridView();
            cbTrangThai = new ComboBox();
            txtMaDP = new TextBox();
            btnLamMoi = new Button();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvLichSu).BeginInit();
            SuspendLayout();
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.FromArgb(4, 17, 33);
            label7.FlatStyle = FlatStyle.Flat;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.FromArgb(246, 224, 175);
            label7.Image = (Image)resources.GetObject("label7.Image");
            label7.ImageAlign = ContentAlignment.MiddleLeft;
            label7.Location = new Point(76, 255);
            label7.Name = "label7";
            label7.RightToLeft = RightToLeft.No;
            label7.Size = new Size(202, 33);
            label7.TabIndex = 27;
            label7.Text = "       THỜI GIAN ĐẶT";
            label7.TextAlign = ContentAlignment.MiddleRight;
            label7.UseCompatibleTextRendering = true;
            // 
            // dtpThoiGian
            // 
            dtpThoiGian.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            dtpThoiGian.Location = new Point(50, 305);
            dtpThoiGian.Name = "dtpThoiGian";
            dtpThoiGian.Size = new Size(290, 31);
            dtpThoiGian.TabIndex = 26;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(4, 17, 33);
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(246, 224, 175);
            label1.Image = (Image)resources.GetObject("label1.Image");
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(411, 255);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.No;
            label1.Size = new Size(172, 33);
            label1.TabIndex = 28;
            label1.Text = "       TRẠNG THÁI";
            label1.TextAlign = ContentAlignment.MiddleRight;
            label1.UseCompatibleTextRendering = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(4, 17, 33);
            label2.FlatStyle = FlatStyle.Flat;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(246, 224, 175);
            label2.Image = (Image)resources.GetObject("label2.Image");
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(734, 255);
            label2.Name = "label2";
            label2.RightToLeft = RightToLeft.No;
            label2.Size = new Size(209, 33);
            label2.TabIndex = 29;
            label2.Text = "       MÃ ĐẶT PHÒNG";
            label2.TextAlign = ContentAlignment.MiddleRight;
            label2.UseCompatibleTextRendering = true;
            // 
            // btnTim
            // 
            btnTim.BackColor = Color.FromArgb(4, 17, 33);
            btnTim.FlatAppearance.BorderSize = 0;
            btnTim.FlatAppearance.MouseDownBackColor = Color.FromArgb(2, 12, 24);
            btnTim.FlatAppearance.MouseOverBackColor = Color.FromArgb(100, 77, 38);
            btnTim.FlatStyle = FlatStyle.Flat;
            btnTim.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnTim.ForeColor = Color.FromArgb(246, 224, 175);
            btnTim.Image = (Image)resources.GetObject("btnTim.Image");
            btnTim.ImageAlign = ContentAlignment.MiddleRight;
            btnTim.Location = new Point(1063, 265);
            btnTim.Name = "btnTim";
            btnTim.Size = new Size(194, 73);
            btnTim.TabIndex = 30;
            btnTim.Text = "TÌM KIẾM";
            btnTim.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnTim.UseVisualStyleBackColor = false;
            btnTim.Click += btnTim_Click_1;
            // 
            // dgvLichSu
            // 
            dgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLichSu.BackgroundColor = Color.FromArgb(0, 14, 25);
            dgvLichSu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLichSu.Location = new Point(41, 404);
            dgvLichSu.Name = "dgvLichSu";
            dgvLichSu.RowHeadersWidth = 51;
            dgvLichSu.RowTemplate.Height = 29;
            dgvLichSu.Size = new Size(1449, 497);
            dgvLichSu.TabIndex = 52;
            dgvLichSu.CellContentClick += dgvLichSu_CellContentClick;
            // 
            // cbTrangThai
            // 
            cbTrangThai.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            cbTrangThai.FormattingEnabled = true;
            cbTrangThai.Location = new Point(396, 305);
            cbTrangThai.Name = "cbTrangThai";
            cbTrangThai.Size = new Size(251, 33);
            cbTrangThai.TabIndex = 53;
            // 
            // txtMaDP
            // 
            txtMaDP.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txtMaDP.Location = new Point(724, 305);
            txtMaDP.Name = "txtMaDP";
            txtMaDP.Size = new Size(280, 31);
            txtMaDP.TabIndex = 54;
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = Color.FromArgb(4, 17, 33);
            btnLamMoi.FlatAppearance.BorderSize = 0;
            btnLamMoi.FlatAppearance.MouseDownBackColor = Color.FromArgb(2, 12, 24);
            btnLamMoi.FlatAppearance.MouseOverBackColor = Color.FromArgb(100, 77, 38);
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnLamMoi.ForeColor = Color.FromArgb(246, 224, 175);
            btnLamMoi.Image = (Image)resources.GetObject("btnLamMoi.Image");
            btnLamMoi.ImageAlign = ContentAlignment.MiddleRight;
            btnLamMoi.Location = new Point(1295, 265);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(166, 71);
            btnLamMoi.TabIndex = 55;
            btnLamMoi.Text = "LÀM MỚI";
            btnLamMoi.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLamMoi.UseVisualStyleBackColor = false;
            btnLamMoi.Click += btnLamMoi_Click_1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Book Antiqua", 40.2F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.FromArgb(245, 210, 105);
            label3.Location = new Point(384, 66);
            label3.Name = "label3";
            label3.Size = new Size(791, 80);
            label3.TabIndex = 56;
            label3.Text = "TRA CỨU ĐẶT PHÒNG";
            // 
            // frmXemLichSu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1549, 1135);
            Controls.Add(label3);
            Controls.Add(btnLamMoi);
            Controls.Add(txtMaDP);
            Controls.Add(cbTrangThai);
            Controls.Add(dgvLichSu);
            Controls.Add(btnTim);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label7);
            Controls.Add(dtpThoiGian);
            Name = "frmXemLichSu";
            Text = "frmXemLichSu";
            Load += frmXemLichSu_Load_1;
            ((System.ComponentModel.ISupportInitialize)dgvLichSu).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label7;
        private DateTimePicker dtpThoiGian;
        private Label label1;
        private Label label2;
        private Button btnTim;
        private DataGridView dgvLichSu;
        private ComboBox cbTrangThai;
        private TextBox txtMaDP;
        private Button btnLamMoi;
        private Label label3;
    }
}