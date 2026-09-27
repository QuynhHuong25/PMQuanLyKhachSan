namespace PMQuanLyKhachSan
{
    partial class frmDatDV
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDatDV));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            lblSL = new Label();
            numSL = new NumericUpDown();
            btnDatDV = new Button();
            cbLoaiDV = new ComboBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            dtpNgaySD = new DateTimePicker();
            dtpGioSD = new DateTimePicker();
            txtMaDP = new TextBox();
            txtTenKH = new TextBox();
            dgvDSDV = new DataGridView();
            dgvPhongDaDat = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)numSL).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDSDV).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPhongDaDat).BeginInit();
            SuspendLayout();
            // 
            // lblSL
            // 
            lblSL.AutoSize = true;
            lblSL.BackColor = Color.FromArgb(4, 17, 33);
            lblSL.FlatStyle = FlatStyle.Flat;
            lblSL.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblSL.ForeColor = Color.FromArgb(246, 224, 175);
            lblSL.ImageAlign = ContentAlignment.MiddleLeft;
            lblSL.Location = new Point(50, 573);
            lblSL.Name = "lblSL";
            lblSL.Size = new Size(97, 28);
            lblSL.TabIndex = 53;
            lblSL.Text = "Số lượng";
            // 
            // numSL
            // 
            numSL.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            numSL.Location = new Point(228, 573);
            numSL.Name = "numSL";
            numSL.Size = new Size(171, 34);
            numSL.TabIndex = 51;
            // 
            // btnDatDV
            // 
            btnDatDV.BackColor = Color.Gold;
            btnDatDV.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnDatDV.ForeColor = SystemColors.ActiveCaptionText;
            btnDatDV.Image = (Image)resources.GetObject("btnDatDV.Image");
            btnDatDV.ImageAlign = ContentAlignment.MiddleRight;
            btnDatDV.Location = new Point(77, 662);
            btnDatDV.Name = "btnDatDV";
            btnDatDV.Size = new Size(434, 71);
            btnDatDV.TabIndex = 50;
            btnDatDV.Text = " Đặt dịch vụ";
            btnDatDV.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDatDV.UseVisualStyleBackColor = false;
            btnDatDV.Click += btnDatDV_Click;
            // 
            // cbLoaiDV
            // 
            cbLoaiDV.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cbLoaiDV.FormattingEnabled = true;
            cbLoaiDV.Location = new Point(228, 350);
            cbLoaiDV.Name = "cbLoaiDV";
            cbLoaiDV.Size = new Size(314, 36);
            cbLoaiDV.TabIndex = 49;
            cbLoaiDV.SelectedIndexChanged += cbLoaiDV_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.FromArgb(4, 17, 33);
            label7.FlatStyle = FlatStyle.Flat;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.FromArgb(246, 224, 175);
            label7.ImageAlign = ContentAlignment.MiddleLeft;
            label7.Location = new Point(50, 446);
            label7.Name = "label7";
            label7.RightToLeft = RightToLeft.No;
            label7.Size = new Size(143, 33);
            label7.TabIndex = 48;
            label7.Text = "Ngày sử dụng";
            label7.TextAlign = ContentAlignment.MiddleRight;
            label7.UseCompatibleTextRendering = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(4, 17, 33);
            label6.FlatStyle = FlatStyle.Flat;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.FromArgb(246, 224, 175);
            label6.ImageAlign = ContentAlignment.MiddleLeft;
            label6.Location = new Point(50, 511);
            label6.Name = "label6";
            label6.Size = new Size(127, 28);
            label6.TabIndex = 47;
            label6.Text = "Giờ sử dụng";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(4, 17, 33);
            label5.FlatStyle = FlatStyle.Flat;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.FromArgb(246, 224, 175);
            label5.ImageAlign = ContentAlignment.MiddleLeft;
            label5.Location = new Point(51, 350);
            label5.Name = "label5";
            label5.Size = new Size(126, 28);
            label5.TabIndex = 46;
            label5.Text = "Loại dịch vụ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(4, 17, 33);
            label4.FlatStyle = FlatStyle.Flat;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.FromArgb(246, 224, 175);
            label4.ImageAlign = ContentAlignment.MiddleLeft;
            label4.Location = new Point(54, 224);
            label4.Name = "label4";
            label4.Size = new Size(160, 28);
            label4.TabIndex = 45;
            label4.Text = "Tên khách hàng";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(4, 17, 33);
            label3.FlatStyle = FlatStyle.Flat;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.FromArgb(246, 224, 175);
            label3.ImageAlign = ContentAlignment.MiddleLeft;
            label3.Location = new Point(54, 289);
            label3.Name = "label3";
            label3.Size = new Size(146, 28);
            label3.TabIndex = 44;
            label3.Text = "Mã đặt phòng";
            // 
            // dtpNgaySD
            // 
            dtpNgaySD.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            dtpNgaySD.Location = new Point(226, 447);
            dtpNgaySD.Name = "dtpNgaySD";
            dtpNgaySD.Size = new Size(316, 31);
            dtpNgaySD.TabIndex = 42;
            // 
            // dtpGioSD
            // 
            dtpGioSD.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            dtpGioSD.Location = new Point(227, 510);
            dtpGioSD.Name = "dtpGioSD";
            dtpGioSD.Size = new Size(315, 31);
            dtpGioSD.TabIndex = 41;
            // 
            // txtMaDP
            // 
            txtMaDP.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtMaDP.Location = new Point(227, 287);
            txtMaDP.Name = "txtMaDP";
            txtMaDP.Size = new Size(315, 34);
            txtMaDP.TabIndex = 39;
            // 
            // txtTenKH
            // 
            txtTenKH.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtTenKH.Location = new Point(228, 223);
            txtTenKH.Name = "txtTenKH";
            txtTenKH.Size = new Size(314, 34);
            txtTenKH.TabIndex = 38;
            // 
            // dgvDSDV
            // 
            dgvDSDV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDSDV.BackgroundColor = Color.FromArgb(5, 21, 36);
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Transparent;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(246, 224, 175);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvDSDV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvDSDV.ColumnHeadersHeight = 29;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.Transparent;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvDSDV.DefaultCellStyle = dataGridViewCellStyle2;
            dgvDSDV.Location = new Point(695, 155);
            dgvDSDV.Name = "dgvDSDV";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.Transparent;
            dataGridViewCellStyle3.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(246, 224, 175);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvDSDV.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvDSDV.RowHeadersWidth = 51;
            dataGridViewCellStyle4.BackColor = Color.Transparent;
            dataGridViewCellStyle4.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dgvDSDV.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvDSDV.RowTemplate.Height = 29;
            dgvDSDV.Size = new Size(831, 300);
            dgvDSDV.TabIndex = 54;
            dgvDSDV.CellContentClick += dgvDSDV_CellContentClick;
            dgvDSDV.SelectionChanged += dgvDSDV_SelectionChanged;
            // 
            // dgvPhongDaDat
            // 
            dgvPhongDaDat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhongDaDat.BackgroundColor = Color.FromArgb(5, 21, 36);
            dgvPhongDaDat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPhongDaDat.Location = new Point(695, 590);
            dgvPhongDaDat.Name = "dgvPhongDaDat";
            dgvPhongDaDat.RowHeadersWidth = 51;
            dgvPhongDaDat.RowTemplate.Height = 29;
            dgvPhongDaDat.Size = new Size(831, 460);
            dgvPhongDaDat.TabIndex = 55;
            dgvPhongDaDat.CellContentClick += dgvPhongDaDat_CellContentClick;
            // 
            // frmDatDV
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1538, 1098);
            Controls.Add(dgvPhongDaDat);
            Controls.Add(dgvDSDV);
            Controls.Add(lblSL);
            Controls.Add(numSL);
            Controls.Add(btnDatDV);
            Controls.Add(cbLoaiDV);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(dtpNgaySD);
            Controls.Add(dtpGioSD);
            Controls.Add(txtMaDP);
            Controls.Add(txtTenKH);
            ForeColor = Color.Black;
            Name = "frmDatDV";
            Text = "Số lượng";
            Load += frmDatDV_Load;
            ((System.ComponentModel.ISupportInitialize)numSL).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDSDV).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPhongDaDat).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label10;
        private Label lblSL;
        private NumericUpDown numTreEm;
        private NumericUpDown numSL;
        private Button btnDatDV;
        private ComboBox cbLoaiDV;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private DateTimePicker dtpNgaySD;
        private DateTimePicker dtpGioSD;
        private TextBox txtMaDP;
        private TextBox txtTenKH;
        private DataGridView dgvDSDV;
        private DataGridView dgvPhongDaDat;
    }
}