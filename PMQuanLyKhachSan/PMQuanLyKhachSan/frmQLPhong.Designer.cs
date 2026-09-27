namespace PMQuanLyKhachSan
{
    partial class frmQLPhong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQLPhong));
            label2 = new Label();
            txtMaPhong = new TextBox();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            txtTenPhong = new TextBox();
            txtGiaPhong = new TextBox();
            cboLoaiPhong = new ComboBox();
            label7 = new Label();
            cboTimLoaiPhong = new ComboBox();
            label8 = new Label();
            dtpTuNgay = new DateTimePicker();
            dtpDenNgay = new DateTimePicker();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            dgvPhong = new DataGridView();
            txtMoTa = new TextBox();
            txtTrangThai = new TextBox();
            btThem = new Button();
            btnSua = new Button();
            btnLamMoi = new Button();
            btnTraCuu = new Button();
            label12 = new Label();
            label19 = new Label();
            pictureBox11 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dgvPhong).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(246, 224, 175);
            label2.Location = new Point(80, 195);
            label2.Name = "label2";
            label2.Size = new Size(99, 25);
            label2.TabIndex = 30;
            label2.Text = "Mã phòng";
            // 
            // txtMaPhong
            // 
            txtMaPhong.Location = new Point(214, 196);
            txtMaPhong.Name = "txtMaPhong";
            txtMaPhong.Size = new Size(344, 27);
            txtMaPhong.TabIndex = 29;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(246, 224, 175);
            label1.Location = new Point(80, 455);
            label1.Name = "label1";
            label1.Size = new Size(99, 25);
            label1.TabIndex = 31;
            label1.Text = "Trạng thái";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.FromArgb(246, 224, 175);
            label3.Location = new Point(80, 247);
            label3.Name = "label3";
            label3.Size = new Size(102, 25);
            label3.TabIndex = 32;
            label3.Text = "Tên phòng";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.FromArgb(246, 224, 175);
            label4.Location = new Point(80, 403);
            label4.Name = "label4";
            label4.Size = new Size(62, 25);
            label4.TabIndex = 33;
            label4.Text = "Mô tả";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.FromArgb(246, 224, 175);
            label5.Location = new Point(80, 299);
            label5.Name = "label5";
            label5.Size = new Size(107, 25);
            label5.TabIndex = 34;
            label5.Text = "Loại phòng";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.FromArgb(246, 224, 175);
            label6.Location = new Point(80, 351);
            label6.Name = "label6";
            label6.Size = new Size(100, 25);
            label6.TabIndex = 35;
            label6.Text = "Giá phòng";
            // 
            // txtTenPhong
            // 
            txtTenPhong.Location = new Point(214, 248);
            txtTenPhong.Name = "txtTenPhong";
            txtTenPhong.Size = new Size(344, 27);
            txtTenPhong.TabIndex = 36;
            // 
            // txtGiaPhong
            // 
            txtGiaPhong.Location = new Point(214, 349);
            txtGiaPhong.Name = "txtGiaPhong";
            txtGiaPhong.Size = new Size(344, 27);
            txtGiaPhong.TabIndex = 37;
            // 
            // cboLoaiPhong
            // 
            cboLoaiPhong.FormattingEnabled = true;
            cboLoaiPhong.Location = new Point(214, 296);
            cboLoaiPhong.Name = "cboLoaiPhong";
            cboLoaiPhong.Size = new Size(344, 28);
            cboLoaiPhong.TabIndex = 38;
            cboLoaiPhong.SelectedIndexChanged += cboLoaiPhong_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.FromArgb(246, 224, 175);
            label7.Location = new Point(963, 247);
            label7.Name = "label7";
            label7.Size = new Size(107, 25);
            label7.TabIndex = 41;
            label7.Text = "Loại Phòng";
            // 
            // cboTimLoaiPhong
            // 
            cboTimLoaiPhong.FormattingEnabled = true;
            cboTimLoaiPhong.Location = new Point(1084, 244);
            cboTimLoaiPhong.Name = "cboTimLoaiPhong";
            cboTimLoaiPhong.Size = new Size(225, 28);
            cboTimLoaiPhong.TabIndex = 42;
            cboTimLoaiPhong.SelectedIndexChanged += cboTimLoaiPhong_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label8.ForeColor = Color.FromArgb(246, 224, 175);
            label8.Location = new Point(968, 344);
            label8.Name = "label8";
            label8.Size = new Size(82, 25);
            label8.TabIndex = 44;
            label8.Text = "Từ ngày";
            // 
            // dtpTuNgay
            // 
            dtpTuNgay.Location = new Point(1108, 345);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(306, 27);
            dtpTuNgay.TabIndex = 45;
            // 
            // dtpDenNgay
            // 
            dtpDenNgay.Location = new Point(1108, 398);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(306, 27);
            dtpDenNgay.TabIndex = 47;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label9.ForeColor = Color.FromArgb(246, 224, 175);
            label9.Location = new Point(968, 397);
            label9.Name = "label9";
            label9.Size = new Size(93, 25);
            label9.TabIndex = 46;
            label9.Text = "Đến ngày";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label10.ForeColor = Color.Gold;
            label10.Location = new Point(963, 198);
            label10.Name = "label10";
            label10.Size = new Size(268, 25);
            label10.TabIndex = 48;
            label10.Text = "TÌM KIẾM THEO LOẠI PHÒNG";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.Transparent;
            label11.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label11.ForeColor = Color.Gold;
            label11.Location = new Point(963, 295);
            label11.Name = "label11";
            label11.Size = new Size(280, 25);
            label11.TabIndex = 49;
            label11.Text = "TRA CỨU TÌNH TRẠNG PHÒNG";
            // 
            // dgvPhong
            // 
            dgvPhong.BackgroundColor = Color.FromArgb(1, 17, 32);
            dgvPhong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPhong.Location = new Point(50, 603);
            dgvPhong.Name = "dgvPhong";
            dgvPhong.RowHeadersWidth = 51;
            dgvPhong.RowTemplate.Height = 29;
            dgvPhong.Size = new Size(1455, 475);
            dgvPhong.TabIndex = 55;
            dgvPhong.CellClick += dgvPhong_CellClick;
            dgvPhong.CellContentClick += dgvPhong_CellContentClick;
            // 
            // txtMoTa
            // 
            txtMoTa.Location = new Point(214, 404);
            txtMoTa.Name = "txtMoTa";
            txtMoTa.Size = new Size(344, 27);
            txtMoTa.TabIndex = 56;
            // 
            // txtTrangThai
            // 
            txtTrangThai.Location = new Point(214, 458);
            txtTrangThai.Name = "txtTrangThai";
            txtTrangThai.Size = new Size(344, 27);
            txtTrangThai.TabIndex = 57;
            // 
            // btThem
            // 
            btThem.BackColor = Color.Gold;
            btThem.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btThem.Location = new Point(670, 238);
            btThem.Name = "btThem";
            btThem.Size = new Size(150, 40);
            btThem.TabIndex = 58;
            btThem.Text = "Thêm";
            btThem.UseVisualStyleBackColor = false;
            btThem.Click += btThem_Click_1;
            // 
            // btnSua
            // 
            btnSua.BackColor = Color.Gold;
            btnSua.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btnSua.Location = new Point(670, 311);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(150, 40);
            btnSua.TabIndex = 59;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = false;
            btnSua.Click += btnSua_Click_1;
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = Color.Gold;
            btnLamMoi.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btnLamMoi.Location = new Point(670, 388);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(150, 40);
            btnLamMoi.TabIndex = 61;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            btnLamMoi.Click += btnLamMoi_Click_1;
            // 
            // btnTraCuu
            // 
            btnTraCuu.BackColor = Color.Gold;
            btnTraCuu.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btnTraCuu.Location = new Point(1265, 447);
            btnTraCuu.Name = "btnTraCuu";
            btnTraCuu.Size = new Size(150, 40);
            btnTraCuu.TabIndex = 62;
            btnTraCuu.Text = "Tra cứu";
            btnTraCuu.UseVisualStyleBackColor = false;
            btnTraCuu.Click += btnTraCuu_Click_1;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.Transparent;
            label12.Font = new Font("Times New Roman", 40.2F, FontStyle.Bold, GraphicsUnit.Point);
            label12.ForeColor = Color.FromArgb(245, 210, 105);
            label12.Location = new Point(472, 54);
            label12.Name = "label12";
            label12.Size = new Size(600, 76);
            label12.TabIndex = 63;
            label12.Text = "QUẢN LÝ PHÒNG";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.BackColor = Color.FromArgb(2, 18, 34);
            label19.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label19.ForeColor = Color.FromArgb(245, 210, 105);
            label19.Location = new Point(113, 557);
            label19.Name = "label19";
            label19.Size = new Size(455, 28);
            label19.TabIndex = 86;
            label19.Text = "DANH SÁCH PHÒNG HIỆN CÓ TẠI KHÁCH SẠN";
            // 
            // pictureBox11
            // 
            pictureBox11.BackColor = Color.FromArgb(2, 18, 34);
            pictureBox11.Image = (Image)resources.GetObject("pictureBox11.Image");
            pictureBox11.Location = new Point(78, 556);
            pictureBox11.Name = "pictureBox11";
            pictureBox11.Size = new Size(32, 32);
            pictureBox11.TabIndex = 85;
            pictureBox11.TabStop = false;
            // 
            // frmQLPhong
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1551, 1123);
            Controls.Add(label19);
            Controls.Add(pictureBox11);
            Controls.Add(label12);
            Controls.Add(btnTraCuu);
            Controls.Add(btnLamMoi);
            Controls.Add(btnSua);
            Controls.Add(btThem);
            Controls.Add(txtTrangThai);
            Controls.Add(txtMoTa);
            Controls.Add(dgvPhong);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(dtpDenNgay);
            Controls.Add(label9);
            Controls.Add(dtpTuNgay);
            Controls.Add(label8);
            Controls.Add(cboTimLoaiPhong);
            Controls.Add(label7);
            Controls.Add(cboLoaiPhong);
            Controls.Add(txtGiaPhong);
            Controls.Add(txtTenPhong);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(txtMaPhong);
            Name = "frmQLPhong";
            Text = "QLPhong";
            Load += frmQLPhong_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPhong).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private TextBox txtMaPhong;
        private Label label1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtTenPhong;
        private TextBox txtGiaPhong;
        private ComboBox cboLoaiPhong;
        private Label label7;
        private ComboBox cboTimLoaiPhong;
        private Label label8;
        private DateTimePicker dtpTuNgay;
        private DateTimePicker dtpDenNgay;
        private Label label9;
        private Label label10;
        private Label label11;
        private DataGridView dgvPhong;
        private TextBox txtMoTa;
        private TextBox txtTrangThai;
        private Button btThem;
        private Button btnSua;
        private Button btnLamMoi;
        private Button btnTraCuu;
        private Label label12;
        private Label label19;
        private PictureBox pictureBox11;
    }
}