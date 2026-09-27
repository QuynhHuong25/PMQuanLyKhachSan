namespace PMQuanLyKhachSan
{
    partial class frmQLDichVu
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQLDichVu));
            label7 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnThem = new Button();
            btnXoa = new Button();
            btnSua = new Button();
            btnLamMoi = new Button();
            dgvDichVu = new DataGridView();
            txtMaDV = new TextBox();
            txtTenDV = new TextBox();
            txtGiaDV = new TextBox();
            label19 = new Label();
            pictureBox11 = new PictureBox();
            label4 = new Label();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dgvDichVu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.FlatStyle = FlatStyle.Flat;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.FromArgb(246, 224, 175);
            label7.ImageAlign = ContentAlignment.MiddleLeft;
            label7.Location = new Point(128, 256);
            label7.Name = "label7";
            label7.Size = new Size(117, 28);
            label7.TabIndex = 38;
            label7.Text = "Mã dịch vụ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.FlatStyle = FlatStyle.Flat;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(246, 224, 175);
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(128, 338);
            label2.Name = "label2";
            label2.Size = new Size(120, 28);
            label2.TabIndex = 39;
            label2.Text = "Tên dịch vụ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.FlatStyle = FlatStyle.Flat;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.FromArgb(246, 224, 175);
            label3.ImageAlign = ContentAlignment.MiddleLeft;
            label3.Location = new Point(128, 420);
            label3.Name = "label3";
            label3.Size = new Size(118, 28);
            label3.TabIndex = 40;
            label3.Text = "Giá dịch vụ";
            // 
            // btnThem
            // 
            btnThem.BackColor = Color.Gold;
            btnThem.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btnThem.Location = new Point(1247, 350);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(150, 40);
            btnThem.TabIndex = 46;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = false;
            btnThem.Click += btnThem_Click;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = Color.Gold;
            btnXoa.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btnXoa.Location = new Point(1247, 291);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(150, 40);
            btnXoa.TabIndex = 45;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = false;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnSua
            // 
            btnSua.BackColor = Color.Gold;
            btnSua.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btnSua.Location = new Point(1247, 230);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(150, 40);
            btnSua.TabIndex = 44;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = false;
            btnSua.Click += btnSua_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = Color.Gold;
            btnLamMoi.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btnLamMoi.Location = new Point(1247, 410);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(150, 40);
            btnLamMoi.TabIndex = 62;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // dgvDichVu
            // 
            dgvDichVu.BackgroundColor = Color.FromArgb(1, 17, 32);
            dgvDichVu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDichVu.Location = new Point(40, 599);
            dgvDichVu.Name = "dgvDichVu";
            dgvDichVu.RowHeadersWidth = 51;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDichVu.RowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvDichVu.RowTemplate.Height = 29;
            dgvDichVu.Size = new Size(1458, 446);
            dgvDichVu.TabIndex = 63;
            dgvDichVu.CellClick += dgvDichVu_CellClick;
            // 
            // txtMaDV
            // 
            txtMaDV.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txtMaDV.Location = new Point(316, 260);
            txtMaDV.Name = "txtMaDV";
            txtMaDV.Size = new Size(523, 31);
            txtMaDV.TabIndex = 64;
            // 
            // txtTenDV
            // 
            txtTenDV.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txtTenDV.Location = new Point(316, 345);
            txtTenDV.Name = "txtTenDV";
            txtTenDV.Size = new Size(523, 31);
            txtTenDV.TabIndex = 65;
            // 
            // txtGiaDV
            // 
            txtGiaDV.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txtGiaDV.Location = new Point(316, 427);
            txtGiaDV.Name = "txtGiaDV";
            txtGiaDV.Size = new Size(523, 31);
            txtGiaDV.TabIndex = 66;
            txtGiaDV.TextChanged += txtGiaDV_TextChanged;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.BackColor = Color.FromArgb(2, 18, 34);
            label19.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label19.ForeColor = Color.FromArgb(245, 210, 105);
            label19.Location = new Point(128, 188);
            label19.Name = "label19";
            label19.Size = new Size(240, 31);
            label19.TabIndex = 86;
            label19.Text = "THÔNG TIN DỊCH VỤ";
            // 
            // pictureBox11
            // 
            pictureBox11.BackColor = Color.FromArgb(2, 18, 34);
            pictureBox11.Image = (Image)resources.GetObject("pictureBox11.Image");
            pictureBox11.Location = new Point(93, 187);
            pictureBox11.Name = "pictureBox11";
            pictureBox11.Size = new Size(32, 32);
            pictureBox11.TabIndex = 85;
            pictureBox11.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Times New Roman", 40.2F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.FromArgb(245, 210, 105);
            label4.Location = new Point(452, 54);
            label4.Name = "label4";
            label4.Size = new Size(641, 76);
            label4.TabIndex = 88;
            label4.Text = "QUẢN LÝ DỊCH VỤ";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1285, 25);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(141, 114);
            pictureBox1.TabIndex = 89;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(2, 18, 34);
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(245, 210, 105);
            label1.Location = new Point(93, 557);
            label1.Name = "label1";
            label1.Size = new Size(385, 28);
            label1.TabIndex = 91;
            label1.Text = "DANH SÁCH DỊCH VỤ CỦA KHÁCH SẠN";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(2, 18, 34);
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(58, 556);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(32, 32);
            pictureBox2.TabIndex = 90;
            pictureBox2.TabStop = false;
            // 
            // frmQLDichVu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1537, 1118);
            Controls.Add(label1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(label4);
            Controls.Add(label19);
            Controls.Add(pictureBox11);
            Controls.Add(txtGiaDV);
            Controls.Add(txtTenDV);
            Controls.Add(txtMaDV);
            Controls.Add(dgvDichVu);
            Controls.Add(btnLamMoi);
            Controls.Add(btnThem);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label7);
            Name = "frmQLDichVu";
            Text = "frmQLDichVu";
            Load += frmQLDichVu_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDichVu).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label7;
        private Label label2;
        private Label label3;
        private Button btnThem;
        private Button btnXoa;
        private Button btnSua;
        private Button btnLamMoi;
        private DataGridView dgvDichVu;
        private TextBox txtMaDV;
        private TextBox txtTenDV;
        private TextBox txtGiaDV;
        private Label label19;
        private PictureBox pictureBox11;
        private Label label4;
        private PictureBox pictureBox1;
        private Label label1;
        private PictureBox pictureBox2;
    }
}