namespace PMQuanLyKhachSan
{
    partial class frmQLNhanVien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQLNhanVien));
            txtMaNV = new TextBox();
            txtHoTen = new TextBox();
            txtEmail = new TextBox();
            txtSDT = new TextBox();
            btTim = new Button();
            btLamMoi = new Button();
            btSua = new Button();
            btXoa = new Button();
            dgvNhanVien = new DataGridView();
            cboChucVu = new ComboBox();
            dtpNgaySinh = new DateTimePicker();
            btThem = new Button();
            btBack = new Button();
            rdNam = new RadioButton();
            rdNu = new RadioButton();
            label7 = new Label();
            label1 = new Label();
            label2 = new Label();
            label8 = new Label();
            label3 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label19 = new Label();
            pictureBox11 = new PictureBox();
            btnLichPhanCong = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvNhanVien).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).BeginInit();
            SuspendLayout();
            // 
            // txtMaNV
            // 
            txtMaNV.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txtMaNV.Location = new Point(199, 224);
            txtMaNV.Name = "txtMaNV";
            txtMaNV.ReadOnly = true;
            txtMaNV.Size = new Size(315, 31);
            txtMaNV.TabIndex = 4;
            // 
            // txtHoTen
            // 
            txtHoTen.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txtHoTen.Location = new Point(199, 285);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(315, 31);
            txtHoTen.TabIndex = 5;
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txtEmail.Location = new Point(716, 289);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(309, 31);
            txtEmail.TabIndex = 11;
            txtEmail.TextChanged += txtEmail_TextChanged;
            // 
            // txtSDT
            // 
            txtSDT.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txtSDT.Location = new Point(716, 224);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(309, 31);
            txtSDT.TabIndex = 13;
            // 
            // btTim
            // 
            btTim.BackColor = Color.Gold;
            btTim.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btTim.Location = new Point(716, 415);
            btTim.Name = "btTim";
            btTim.Size = new Size(120, 35);
            btTim.TabIndex = 14;
            btTim.Text = "Tìm kiếm";
            btTim.UseVisualStyleBackColor = false;
            btTim.Click += btTim_Click;
            // 
            // btLamMoi
            // 
            btLamMoi.BackColor = Color.Gold;
            btLamMoi.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btLamMoi.Location = new Point(862, 415);
            btLamMoi.Name = "btLamMoi";
            btLamMoi.Size = new Size(120, 35);
            btLamMoi.TabIndex = 15;
            btLamMoi.Text = "Làm mới";
            btLamMoi.UseVisualStyleBackColor = false;
            btLamMoi.Click += btLamMoi_Click;
            // 
            // btSua
            // 
            btSua.BackColor = Color.Gold;
            btSua.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btSua.Location = new Point(1215, 206);
            btSua.Name = "btSua";
            btSua.Size = new Size(150, 40);
            btSua.TabIndex = 17;
            btSua.Text = "Sửa";
            btSua.UseVisualStyleBackColor = false;
            btSua.Click += btSua_Click;
            // 
            // btXoa
            // 
            btXoa.BackColor = Color.Gold;
            btXoa.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btXoa.Location = new Point(1215, 267);
            btXoa.Name = "btXoa";
            btXoa.Size = new Size(150, 40);
            btXoa.TabIndex = 18;
            btXoa.Text = "Xóa";
            btXoa.UseVisualStyleBackColor = false;
            btXoa.Click += btXoa_Click;
            // 
            // dgvNhanVien
            // 
            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhanVien.BackgroundColor = Color.FromArgb(1, 17, 32);
            dgvNhanVien.BorderStyle = BorderStyle.None;
            dgvNhanVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNhanVien.Location = new Point(48, 597);
            dgvNhanVien.Name = "dgvNhanVien";
            dgvNhanVien.RowHeadersWidth = 51;
            dgvNhanVien.RowTemplate.Height = 29;
            dgvNhanVien.Size = new Size(1435, 464);
            dgvNhanVien.TabIndex = 22;
            dgvNhanVien.CellClick += dgvNhanVien_CellClick;
            // 
            // cboChucVu
            // 
            cboChucVu.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            cboChucVu.FormattingEnabled = true;
            cboChucVu.Location = new Point(199, 353);
            cboChucVu.Name = "cboChucVu";
            cboChucVu.Size = new Size(315, 33);
            cboChucVu.TabIndex = 23;
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            dtpNgaySinh.Location = new Point(716, 350);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(309, 31);
            dtpNgaySinh.TabIndex = 31;
            // 
            // btThem
            // 
            btThem.BackColor = Color.Gold;
            btThem.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btThem.Location = new Point(1215, 326);
            btThem.Name = "btThem";
            btThem.Size = new Size(150, 40);
            btThem.TabIndex = 32;
            btThem.Text = "Thêm";
            btThem.UseVisualStyleBackColor = false;
            btThem.Click += btThem_Click;
            // 
            // btBack
            // 
            btBack.BackColor = Color.Gold;
            btBack.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btBack.Location = new Point(1215, 445);
            btBack.Name = "btBack";
            btBack.Size = new Size(150, 40);
            btBack.TabIndex = 33;
            btBack.Text = "Quay lại";
            btBack.UseVisualStyleBackColor = false;
            btBack.Click += btBack_Click;
            // 
            // rdNam
            // 
            rdNam.AutoSize = true;
            rdNam.BackColor = Color.Transparent;
            rdNam.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            rdNam.ForeColor = Color.FromArgb(246, 224, 175);
            rdNam.Location = new Point(224, 417);
            rdNam.Name = "rdNam";
            rdNam.Size = new Size(75, 32);
            rdNam.TabIndex = 35;
            rdNam.TabStop = true;
            rdNam.Text = "Nam";
            rdNam.UseVisualStyleBackColor = false;
            // 
            // rdNu
            // 
            rdNu.AutoSize = true;
            rdNu.BackColor = Color.Transparent;
            rdNu.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            rdNu.ForeColor = Color.FromArgb(246, 224, 175);
            rdNu.Location = new Point(378, 415);
            rdNu.Name = "rdNu";
            rdNu.Size = new Size(60, 32);
            rdNu.TabIndex = 36;
            rdNu.TabStop = true;
            rdNu.Text = "Nữ";
            rdNu.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.FlatStyle = FlatStyle.Flat;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.FromArgb(246, 224, 175);
            label7.ImageAlign = ContentAlignment.MiddleLeft;
            label7.Location = new Point(65, 225);
            label7.Name = "label7";
            label7.Size = new Size(77, 28);
            label7.TabIndex = 37;
            label7.Text = "Mã NV";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(246, 224, 175);
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(65, 285);
            label1.Name = "label1";
            label1.Size = new Size(104, 28);
            label1.TabIndex = 38;
            label1.Text = "Họ và tên";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.FlatStyle = FlatStyle.Flat;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(246, 224, 175);
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(65, 356);
            label2.Name = "label2";
            label2.Size = new Size(88, 28);
            label2.TabIndex = 39;
            label2.Text = "Chức vụ";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.FlatStyle = FlatStyle.Flat;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label8.ForeColor = Color.FromArgb(246, 224, 175);
            label8.ImageAlign = ContentAlignment.MiddleLeft;
            label8.Location = new Point(65, 419);
            label8.Name = "label8";
            label8.Size = new Size(95, 28);
            label8.TabIndex = 40;
            label8.Text = "Giới tính";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.FlatStyle = FlatStyle.Flat;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.FromArgb(246, 224, 175);
            label3.ImageAlign = ContentAlignment.MiddleLeft;
            label3.Location = new Point(569, 229);
            label3.Name = "label3";
            label3.Size = new Size(50, 28);
            label3.TabIndex = 41;
            label3.Text = "SĐT";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.FlatStyle = FlatStyle.Flat;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.FromArgb(246, 224, 175);
            label6.ImageAlign = ContentAlignment.MiddleLeft;
            label6.Location = new Point(569, 289);
            label6.Name = "label6";
            label6.Size = new Size(64, 28);
            label6.TabIndex = 42;
            label6.Text = "Email";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.FlatStyle = FlatStyle.Flat;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.FromArgb(246, 224, 175);
            label5.ImageAlign = ContentAlignment.MiddleLeft;
            label5.Location = new Point(569, 351);
            label5.Name = "label5";
            label5.Size = new Size(107, 28);
            label5.TabIndex = 43;
            label5.Text = "Ngày sinh";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Times New Roman", 40.2F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.FromArgb(245, 210, 105);
            label4.Location = new Point(426, 52);
            label4.Name = "label4";
            label4.Size = new Size(734, 76);
            label4.TabIndex = 44;
            label4.Text = "QUẢN LÝ NHÂN VIÊN";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.BackColor = Color.FromArgb(2, 18, 34);
            label19.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label19.ForeColor = Color.FromArgb(245, 210, 105);
            label19.Location = new Point(110, 556);
            label19.Name = "label19";
            label19.Size = new Size(249, 28);
            label19.TabIndex = 86;
            label19.Text = "DANH SÁCH NHÂN VIÊN";
            // 
            // pictureBox11
            // 
            pictureBox11.BackColor = Color.FromArgb(2, 18, 34);
            pictureBox11.Image = (Image)resources.GetObject("pictureBox11.Image");
            pictureBox11.Location = new Point(75, 555);
            pictureBox11.Name = "pictureBox11";
            pictureBox11.Size = new Size(32, 32);
            pictureBox11.TabIndex = 85;
            pictureBox11.TabStop = false;
            // 
            // btnLichPhanCong
            // 
            btnLichPhanCong.BackColor = Color.Gold;
            btnLichPhanCong.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btnLichPhanCong.Location = new Point(1215, 385);
            btnLichPhanCong.Name = "btnLichPhanCong";
            btnLichPhanCong.Size = new Size(150, 40);
            btnLichPhanCong.TabIndex = 87;
            btnLichPhanCong.Text = "Lịch phân công";
            btnLichPhanCong.UseVisualStyleBackColor = false;
            btnLichPhanCong.Click += btnLichPhanCong_Click;
            // 
            // frmQLNhanVien
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1520, 1093);
            Controls.Add(btnLichPhanCong);
            Controls.Add(label19);
            Controls.Add(pictureBox11);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(label3);
            Controls.Add(label8);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label7);
            Controls.Add(rdNu);
            Controls.Add(rdNam);
            Controls.Add(btBack);
            Controls.Add(btThem);
            Controls.Add(dtpNgaySinh);
            Controls.Add(cboChucVu);
            Controls.Add(dgvNhanVien);
            Controls.Add(btXoa);
            Controls.Add(btSua);
            Controls.Add(btLamMoi);
            Controls.Add(btTim);
            Controls.Add(txtSDT);
            Controls.Add(txtEmail);
            Controls.Add(txtHoTen);
            Controls.Add(txtMaNV);
            ForeColor = SystemColors.ControlText;
            Name = "frmQLNhanVien";
            Text = "frmQLNhanVien";
            Load += frmQLNhanVien_Load;
            ((System.ComponentModel.ISupportInitialize)dgvNhanVien).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtMaNV;
        private TextBox txtHoTen;
        private TextBox txtEmail;
        private TextBox txtSDT;
        private Button btTim;
        private Button btLamMoi;
        private Button btSua;
        private Button btXoa;
        private Button btTao;
        private DataGridView dataGridView1;
        private ComboBox cboChucVu;
        private DateTimePicker dtpNgaySinh;
        private Button btThem;
        private Button btBack;
        private DataGridView dgvNhanVien;
        private RadioButton rdNam;
        private RadioButton rdNu;
        private Label label7;
        private Label label1;
        private Label label2;
        private Label label8;
        private Label label3;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label19;
        private PictureBox pictureBox11;
        private Button btnLichPhanCong;
    }
}