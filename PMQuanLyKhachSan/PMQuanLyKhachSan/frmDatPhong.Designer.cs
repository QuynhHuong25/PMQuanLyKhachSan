namespace PMQuanLyKhachSan
{
    partial class frmDatPhong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDatPhong));
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            txtTenKH = new TextBox();
            txtCCCD = new TextBox();
            txtSDT = new TextBox();
            dtpNgayTra = new DateTimePicker();
            dtpNgayNhan = new DateTimePicker();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            cbLoai = new ComboBox();
            btnDatPhong = new Button();
            dgvPhong = new DataGridView();
            btnTraCuu = new Button();
            btnCheck = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPhong).BeginInit();
            SuspendLayout();
            // 
            // txtTenKH
            // 
            txtTenKH.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtTenKH.Location = new Point(262, 216);
            txtTenKH.Name = "txtTenKH";
            txtTenKH.Size = new Size(315, 34);
            txtTenKH.TabIndex = 3;
            // 
            // txtCCCD
            // 
            txtCCCD.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtCCCD.Location = new Point(261, 276);
            txtCCCD.Name = "txtCCCD";
            txtCCCD.Size = new Size(316, 34);
            txtCCCD.TabIndex = 4;
            // 
            // txtSDT
            // 
            txtSDT.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtSDT.Location = new Point(261, 343);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(316, 34);
            txtSDT.TabIndex = 5;
            // 
            // dtpNgayTra
            // 
            dtpNgayTra.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            dtpNgayTra.Location = new Point(261, 482);
            dtpNgayTra.Name = "dtpNgayTra";
            dtpNgayTra.Size = new Size(316, 31);
            dtpNgayTra.TabIndex = 6;
            // 
            // dtpNgayNhan
            // 
            dtpNgayNhan.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            dtpNgayNhan.Location = new Point(262, 420);
            dtpNgayNhan.Name = "dtpNgayNhan";
            dtpNgayNhan.Size = new Size(315, 31);
            dtpNgayNhan.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(4, 17, 33);
            label2.FlatStyle = FlatStyle.Flat;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(246, 224, 175);
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(65, 343);
            label2.Name = "label2";
            label2.Size = new Size(50, 28);
            label2.TabIndex = 20;
            label2.Text = "SĐT";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(4, 17, 33);
            label3.FlatStyle = FlatStyle.Flat;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.FromArgb(246, 224, 175);
            label3.ImageAlign = ContentAlignment.MiddleLeft;
            label3.Location = new Point(61, 278);
            label3.Name = "label3";
            label3.Size = new Size(61, 28);
            label3.TabIndex = 21;
            label3.Text = "CCCD";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(4, 17, 33);
            label4.FlatStyle = FlatStyle.Flat;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.FromArgb(246, 224, 175);
            label4.ImageAlign = ContentAlignment.MiddleLeft;
            label4.Location = new Point(61, 216);
            label4.Name = "label4";
            label4.Size = new Size(160, 28);
            label4.TabIndex = 22;
            label4.Text = "Tên khách hàng";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(4, 17, 33);
            label5.FlatStyle = FlatStyle.Flat;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.FromArgb(246, 224, 175);
            label5.ImageAlign = ContentAlignment.MiddleLeft;
            label5.Location = new Point(57, 539);
            label5.Name = "label5";
            label5.Size = new Size(117, 28);
            label5.TabIndex = 23;
            label5.Text = "Loại phòng";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(4, 17, 33);
            label6.FlatStyle = FlatStyle.Flat;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.FromArgb(246, 224, 175);
            label6.ImageAlign = ContentAlignment.MiddleLeft;
            label6.Location = new Point(57, 481);
            label6.Name = "label6";
            label6.Size = new Size(161, 28);
            label6.TabIndex = 24;
            label6.Text = "Ngày trả phòng";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.FromArgb(4, 17, 33);
            label7.FlatStyle = FlatStyle.Flat;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.FromArgb(246, 224, 175);
            label7.ImageAlign = ContentAlignment.MiddleLeft;
            label7.Location = new Point(57, 420);
            label7.Name = "label7";
            label7.RightToLeft = RightToLeft.No;
            label7.Size = new Size(181, 33);
            label7.TabIndex = 25;
            label7.Text = "Ngày nhận phòng";
            label7.TextAlign = ContentAlignment.MiddleRight;
            label7.UseCompatibleTextRendering = true;
            // 
            // cbLoai
            // 
            cbLoai.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cbLoai.FormattingEnabled = true;
            cbLoai.Location = new Point(261, 542);
            cbLoai.Name = "cbLoai";
            cbLoai.Size = new Size(316, 36);
            cbLoai.TabIndex = 26;
            // 
            // btnDatPhong
            // 
            btnDatPhong.BackColor = Color.Gold;
            btnDatPhong.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnDatPhong.ForeColor = SystemColors.ActiveCaptionText;
            btnDatPhong.Image = (Image)resources.GetObject("btnDatPhong.Image");
            btnDatPhong.ImageAlign = ContentAlignment.MiddleRight;
            btnDatPhong.Location = new Point(109, 625);
            btnDatPhong.Name = "btnDatPhong";
            btnDatPhong.Size = new Size(414, 60);
            btnDatPhong.TabIndex = 27;
            btnDatPhong.Text = "  Đặt phòng";
            btnDatPhong.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDatPhong.UseVisualStyleBackColor = false;
            btnDatPhong.Click += btnDatPhong_Click;
            // 
            // dgvPhong
            // 
            dgvPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhong.BackgroundColor = Color.FromArgb(5, 21, 36);
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvPhong.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvPhong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvPhong.DefaultCellStyle = dataGridViewCellStyle6;
            dgvPhong.EnableHeadersVisualStyles = false;
            dgvPhong.GridColor = Color.LightSlateGray;
            dgvPhong.Location = new Point(680, 140);
            dgvPhong.Name = "dgvPhong";
            dgvPhong.RowHeadersWidth = 51;
            dgvPhong.RowTemplate.Height = 29;
            dgvPhong.Size = new Size(831, 913);
            dgvPhong.TabIndex = 28;
            dgvPhong.CellContentClick += dgvPhong_CellContentClick;
            // 
            // btnTraCuu
            // 
            btnTraCuu.BackColor = Color.Gold;
            btnTraCuu.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnTraCuu.ForeColor = SystemColors.ActiveCaptionText;
            btnTraCuu.Image = (Image)resources.GetObject("btnTraCuu.Image");
            btnTraCuu.ImageAlign = ContentAlignment.MiddleRight;
            btnTraCuu.Location = new Point(63, 741);
            btnTraCuu.Name = "btnTraCuu";
            btnTraCuu.Size = new Size(254, 56);
            btnTraCuu.TabIndex = 29;
            btnTraCuu.Text = " Tra cứu";
            btnTraCuu.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnTraCuu.UseVisualStyleBackColor = false;
            btnTraCuu.Click += btnTraCuu_Click;
            // 
            // btnCheck
            // 
            btnCheck.BackColor = Color.Gold;
            btnCheck.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnCheck.ForeColor = SystemColors.ActiveCaptionText;
            btnCheck.Image = (Image)resources.GetObject("btnCheck.Image");
            btnCheck.ImageAlign = ContentAlignment.MiddleRight;
            btnCheck.Location = new Point(346, 741);
            btnCheck.Name = "btnCheck";
            btnCheck.Size = new Size(263, 56);
            btnCheck.TabIndex = 30;
            btnCheck.Text = " Check-in/out";
            btnCheck.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCheck.UseVisualStyleBackColor = false;
            btnCheck.Click += btnCheck_Click;
            // 
            // frmDatPhong
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(1, 12, 25);
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1540, 1098);
            Controls.Add(btnCheck);
            Controls.Add(btnTraCuu);
            Controls.Add(dgvPhong);
            Controls.Add(btnDatPhong);
            Controls.Add(cbLoai);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(dtpNgayNhan);
            Controls.Add(dtpNgayTra);
            Controls.Add(txtSDT);
            Controls.Add(txtCCCD);
            Controls.Add(txtTenKH);
            Name = "frmDatPhong";
            Text = "frmDatPhong";
            Load += frmDatPhong_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPhong).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtTenKH;
        private TextBox txtCCCD;
        private TextBox txtSDT;
        private DateTimePicker dtpNgayTra;
        private DateTimePicker dtpNgayNhan;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private ComboBox cbLoai;
        private Button btnDatPhong;
        private DataGridView dgvPhong;
        private Button btnTraCuu;
        private Button btnCheck;
    }
}