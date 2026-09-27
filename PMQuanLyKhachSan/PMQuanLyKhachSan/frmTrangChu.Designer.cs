namespace PMQuanLyKhachSan
{
    partial class frmTrangChu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrangChu));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            label4 = new Label();
            label1 = new Label();
            pnlDoanhThu = new Panel();
            lblTang = new Label();
            lblDoanhThu = new Label();
            label6 = new Label();
            pictureBox2 = new PictureBox();
            pnlKhach = new Panel();
            lblKhach = new Label();
            label13 = new Label();
            label10 = new Label();
            label7 = new Label();
            pictureBox3 = new PictureBox();
            pnlDV = new Panel();
            lblDV = new Label();
            label14 = new Label();
            label11 = new Label();
            label8 = new Label();
            pictureBox4 = new PictureBox();
            pnlPhong = new Panel();
            lblPhong = new Label();
            label5 = new Label();
            label3 = new Label();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            pnlTKDoanhThu = new Panel();
            chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            label15 = new Label();
            pnlDatPhongMoi = new Panel();
            dgvDatPhong = new DataGridView();
            label17 = new Label();
            pnlTTPhong = new Panel();
            lblSoPhong = new Label();
            chartPhong = new System.Windows.Forms.DataVisualization.Charting.Chart();
            label16 = new Label();
            pnlHD = new Panel();
            dgvHDMoi = new DataGridView();
            label18 = new Label();
            pnlDoanhThu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            pnlKhach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            pnlDV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            pnlPhong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlTKDoanhThu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartDoanhThu).BeginInit();
            pnlDatPhongMoi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDatPhong).BeginInit();
            pnlTTPhong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartPhong).BeginInit();
            pnlHD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHDMoi).BeginInit();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(4, 17, 33);
            label4.FlatStyle = FlatStyle.Flat;
            label4.Font = new Font("Windsor", 31.8000011F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.FromArgb(245, 210, 105);
            label4.ImageAlign = ContentAlignment.MiddleLeft;
            label4.Location = new Point(40, 14);
            label4.Name = "label4";
            label4.Size = new Size(776, 91);
            label4.TabIndex = 23;
            label4.Text = "Chào mừng Quản trị viên";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(88, 101);
            label1.Name = "label1";
            label1.Size = new Size(530, 25);
            label1.TabIndex = 24;
            label1.Text = "Trang quản lý tổng quan hệ thống khách sạn LaLa Hotel HaNoi";
            // 
            // pnlDoanhThu
            // 
            pnlDoanhThu.Controls.Add(lblTang);
            pnlDoanhThu.Controls.Add(lblDoanhThu);
            pnlDoanhThu.Controls.Add(label6);
            pnlDoanhThu.Controls.Add(pictureBox2);
            pnlDoanhThu.Location = new Point(378, 156);
            pnlDoanhThu.Name = "pnlDoanhThu";
            pnlDoanhThu.Size = new Size(426, 190);
            pnlDoanhThu.TabIndex = 28;
            pnlDoanhThu.Paint += panel4_Paint;
            // 
            // lblTang
            // 
            lblTang.AutoSize = true;
            lblTang.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            lblTang.ForeColor = Color.White;
            lblTang.Location = new Point(140, 150);
            lblTang.Name = "lblTang";
            lblTang.Size = new Size(185, 25);
            lblTang.TabIndex = 36;
            lblTang.Text = "+ ?% so với hôm qua";
            // 
            // lblDoanhThu
            // 
            lblDoanhThu.AutoSize = true;
            lblDoanhThu.Font = new Font("Segoe UI Semibold", 28.2F, FontStyle.Bold, GraphicsUnit.Point);
            lblDoanhThu.ForeColor = Color.FromArgb(240, 204, 119);
            lblDoanhThu.Location = new Point(117, 67);
            lblDoanhThu.Name = "lblDoanhThu";
            lblDoanhThu.Size = new Size(243, 62);
            lblDoanhThu.TabIndex = 37;
            lblDoanhThu.Text = "31.100.000";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.White;
            label6.Location = new Point(125, 16);
            label6.Name = "label6";
            label6.Size = new Size(219, 31);
            label6.TabIndex = 36;
            label6.Text = "Doanh thu hôm nay";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(16, 49);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(100, 100);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // pnlKhach
            // 
            pnlKhach.Controls.Add(lblKhach);
            pnlKhach.Controls.Add(label13);
            pnlKhach.Controls.Add(label10);
            pnlKhach.Controls.Add(label7);
            pnlKhach.Controls.Add(pictureBox3);
            pnlKhach.Location = new Point(817, 156);
            pnlKhach.Name = "pnlKhach";
            pnlKhach.Size = new Size(346, 190);
            pnlKhach.TabIndex = 29;
            // 
            // lblKhach
            // 
            lblKhach.AutoSize = true;
            lblKhach.Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);
            lblKhach.ForeColor = Color.FromArgb(240, 204, 119);
            lblKhach.Location = new Point(157, 63);
            lblKhach.Name = "lblKhach";
            lblKhach.Size = new Size(87, 67);
            lblKhach.TabIndex = 37;
            lblKhach.Text = "12";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label13.ForeColor = Color.White;
            label13.Location = new Point(250, 98);
            label13.Name = "label13";
            label13.Size = new Size(60, 25);
            label13.TabIndex = 37;
            label13.Text = "Khách";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label10.ForeColor = Color.White;
            label10.Location = new Point(158, 150);
            label10.Name = "label10";
            label10.Size = new Size(86, 25);
            label10.TabIndex = 37;
            label10.Text = "Hôm nay";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.White;
            label7.Location = new Point(125, 16);
            label7.Name = "label7";
            label7.Size = new Size(200, 31);
            label7.TabIndex = 34;
            label7.Text = "Số khách check-in";
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(19, 49);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(100, 100);
            pictureBox3.TabIndex = 2;
            pictureBox3.TabStop = false;
            // 
            // pnlDV
            // 
            pnlDV.Controls.Add(lblDV);
            pnlDV.Controls.Add(label14);
            pnlDV.Controls.Add(label11);
            pnlDV.Controls.Add(label8);
            pnlDV.Controls.Add(pictureBox4);
            pnlDV.Location = new Point(1174, 156);
            pnlDV.Name = "pnlDV";
            pnlDV.Size = new Size(346, 190);
            pnlDV.TabIndex = 29;
            // 
            // lblDV
            // 
            lblDV.AutoSize = true;
            lblDV.Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);
            lblDV.ForeColor = Color.FromArgb(240, 204, 119);
            lblDV.Location = new Point(151, 63);
            lblDV.Name = "lblDV";
            lblDV.Size = new Size(87, 67);
            lblDV.TabIndex = 38;
            lblDV.Text = "16";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label14.ForeColor = Color.White;
            label14.Location = new Point(236, 95);
            label14.Name = "label14";
            label14.Size = new Size(47, 25);
            label14.TabIndex = 37;
            label14.Text = "Đơn";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label11.ForeColor = Color.White;
            label11.Location = new Point(184, 150);
            label11.Name = "label11";
            label11.Size = new Size(86, 25);
            label11.TabIndex = 38;
            label11.Text = "Hôm nay";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label8.ForeColor = Color.White;
            label8.Location = new Point(141, 16);
            label8.Name = "label8";
            label8.Size = new Size(197, 31);
            label8.TabIndex = 35;
            label8.Text = "Đơn hàng dịch vụ";
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(30, 49);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(100, 100);
            pictureBox4.TabIndex = 3;
            pictureBox4.TabStop = false;
            // 
            // pnlPhong
            // 
            pnlPhong.Controls.Add(lblPhong);
            pnlPhong.Controls.Add(label5);
            pnlPhong.Controls.Add(label3);
            pnlPhong.Controls.Add(label2);
            pnlPhong.Controls.Add(pictureBox1);
            pnlPhong.Location = new Point(27, 155);
            pnlPhong.Name = "pnlPhong";
            pnlPhong.Size = new Size(340, 190);
            pnlPhong.TabIndex = 29;
            // 
            // lblPhong
            // 
            lblPhong.AutoSize = true;
            lblPhong.Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);
            lblPhong.ForeColor = Color.FromArgb(240, 204, 119);
            lblPhong.Location = new Point(154, 62);
            lblPhong.Name = "lblPhong";
            lblPhong.Size = new Size(87, 67);
            lblPhong.TabIndex = 36;
            lblPhong.Text = "25";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.White;
            label5.Location = new Point(142, 150);
            label5.Name = "label5";
            label5.Size = new Size(86, 25);
            label5.TabIndex = 35;
            label5.Text = "Hôm nay";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.White;
            label3.Location = new Point(247, 95);
            label3.Name = "label3";
            label3.Size = new Size(66, 25);
            label3.TabIndex = 34;
            label3.Text = "Phòng";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.White;
            label2.Location = new Point(133, 16);
            label2.Name = "label2";
            label2.Size = new Size(183, 31);
            label2.TabIndex = 33;
            label2.Text = "Phòng đặt trước";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(22, 49);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 100);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pnlTKDoanhThu
            // 
            pnlTKDoanhThu.Controls.Add(chartDoanhThu);
            pnlTKDoanhThu.Controls.Add(label15);
            pnlTKDoanhThu.Location = new Point(28, 365);
            pnlTKDoanhThu.Name = "pnlTKDoanhThu";
            pnlTKDoanhThu.Size = new Size(775, 350);
            pnlTKDoanhThu.TabIndex = 30;
            // 
            // chartDoanhThu
            // 
            chartDoanhThu.BackColor = Color.Transparent;
            chartArea1.AxisX.LabelStyle.ForeColor = Color.LightSlateGray;
            chartArea1.AxisX.LineColor = Color.LightSlateGray;
            chartArea1.AxisX.MajorGrid.LineColor = Color.SlateGray;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.LabelStyle.ForeColor = Color.LightSlateGray;
            chartArea1.AxisY.LineColor = Color.LightSlateGray;
            chartArea1.AxisY.MajorGrid.LineColor = Color.SlateGray;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.BackColor = Color.Transparent;
            chartArea1.Name = "ChartArea1";
            chartDoanhThu.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            chartDoanhThu.Legends.Add(legend1);
            chartDoanhThu.Location = new Point(22, 38);
            chartDoanhThu.Name = "chartDoanhThu";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = Color.Gold;
            series1.Legend = "Legend1";
            series1.MarkerColor = Color.Gold;
            series1.MarkerSize = 8;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Doanh thu";
            chartDoanhThu.Series.Add(series1);
            chartDoanhThu.Size = new Size(578, 297);
            chartDoanhThu.TabIndex = 40;
            chartDoanhThu.Text = "Doanh thu";
            chartDoanhThu.Click += chartDoanhThu_Click;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label15.ForeColor = Color.FromArgb(245, 210, 105);
            label15.Location = new Point(22, 7);
            label15.Name = "label15";
            label15.Size = new Size(252, 31);
            label15.TabIndex = 38;
            label15.Text = "Doanh thu theo tháng";
            // 
            // pnlDatPhongMoi
            // 
            pnlDatPhongMoi.Controls.Add(dgvDatPhong);
            pnlDatPhongMoi.Controls.Add(label17);
            pnlDatPhongMoi.Location = new Point(28, 732);
            pnlDatPhongMoi.Name = "pnlDatPhongMoi";
            pnlDatPhongMoi.Size = new Size(775, 350);
            pnlDatPhongMoi.TabIndex = 31;
            // 
            // dgvDatPhong
            // 
            dgvDatPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDatPhong.BackgroundColor = Color.FromArgb(6, 17, 30);
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(5, 21, 36);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvDatPhong.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvDatPhong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(5, 21, 36);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvDatPhong.DefaultCellStyle = dataGridViewCellStyle2;
            dgvDatPhong.EnableHeadersVisualStyles = false;
            dgvDatPhong.GridColor = Color.LightSlateGray;
            dgvDatPhong.Location = new Point(14, 48);
            dgvDatPhong.Name = "dgvDatPhong";
            dgvDatPhong.RowHeadersWidth = 51;
            dgvDatPhong.RowTemplate.Height = 29;
            dgvDatPhong.Size = new Size(745, 290);
            dgvDatPhong.TabIndex = 40;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label17.ForeColor = Color.FromArgb(245, 210, 105);
            label17.Location = new Point(22, 10);
            label17.Name = "label17";
            label17.Size = new Size(359, 31);
            label17.TabIndex = 39;
            label17.Text = "Danh sách nhận phòng hôm nay";
            // 
            // pnlTTPhong
            // 
            pnlTTPhong.Controls.Add(lblSoPhong);
            pnlTTPhong.Controls.Add(chartPhong);
            pnlTTPhong.Controls.Add(label16);
            pnlTTPhong.Location = new Point(816, 365);
            pnlTTPhong.Name = "pnlTTPhong";
            pnlTTPhong.Size = new Size(705, 352);
            pnlTTPhong.TabIndex = 31;
            // 
            // lblSoPhong
            // 
            lblSoPhong.AutoSize = true;
            lblSoPhong.BackColor = Color.Transparent;
            lblSoPhong.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            lblSoPhong.ForeColor = Color.FromArgb(240, 204, 119);
            lblSoPhong.Location = new Point(309, 164);
            lblSoPhong.Name = "lblSoPhong";
            lblSoPhong.Size = new Size(210, 31);
            lblSoPhong.TabIndex = 38;
            lblSoPhong.Text = "Tổng số phòng: 60";
            lblSoPhong.Click += lblSoPhong_Click;
            // 
            // chartPhong
            // 
            chartPhong.BackColor = Color.Transparent;
            chartArea2.BackColor = Color.Transparent;
            chartArea2.Name = "ChartArea1";
            chartPhong.ChartAreas.Add(chartArea2);
            legend2.BackColor = Color.Transparent;
            legend2.ForeColor = Color.White;
            legend2.Name = "Legend1";
            chartPhong.Legends.Add(legend2);
            chartPhong.Location = new Point(19, 45);
            chartPhong.Name = "chartPhong";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.CustomProperties = "DoughnutRadius=50";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chartPhong.Series.Add(series2);
            chartPhong.Size = new Size(372, 285);
            chartPhong.TabIndex = 40;
            chartPhong.Text = "chart2";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label16.ForeColor = Color.FromArgb(245, 210, 105);
            label16.Location = new Point(21, 8);
            label16.Name = "label16";
            label16.Size = new Size(202, 31);
            label16.TabIndex = 39;
            label16.Text = "Tình trạng phòng";
            // 
            // pnlHD
            // 
            pnlHD.Controls.Add(dgvHDMoi);
            pnlHD.Controls.Add(label18);
            pnlHD.Location = new Point(816, 732);
            pnlHD.Name = "pnlHD";
            pnlHD.Size = new Size(705, 350);
            pnlHD.TabIndex = 32;
            // 
            // dgvHDMoi
            // 
            dgvHDMoi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHDMoi.BackgroundColor = Color.FromArgb(6, 17, 30);
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(5, 21, 36);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvHDMoi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvHDMoi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(5, 21, 36);
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvHDMoi.DefaultCellStyle = dataGridViewCellStyle4;
            dgvHDMoi.EnableHeadersVisualStyles = false;
            dgvHDMoi.GridColor = Color.LightSlateGray;
            dgvHDMoi.Location = new Point(14, 49);
            dgvHDMoi.Name = "dgvHDMoi";
            dgvHDMoi.RowHeadersWidth = 51;
            dgvHDMoi.RowTemplate.Height = 29;
            dgvHDMoi.Size = new Size(675, 287);
            dgvHDMoi.TabIndex = 41;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label18.ForeColor = Color.FromArgb(245, 210, 105);
            label18.Location = new Point(21, 11);
            label18.Name = "label18";
            label18.Size = new Size(221, 31);
            label18.TabIndex = 40;
            label18.Text = "Hoạt động gần đây";
            // 
            // frmTrangChu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(1, 12, 25);
            ClientSize = new Size(1542, 1098);
            Controls.Add(pnlHD);
            Controls.Add(pnlTTPhong);
            Controls.Add(pnlDatPhongMoi);
            Controls.Add(pnlTKDoanhThu);
            Controls.Add(pnlDV);
            Controls.Add(pnlKhach);
            Controls.Add(pnlPhong);
            Controls.Add(pnlDoanhThu);
            Controls.Add(label1);
            Controls.Add(label4);
            Name = "frmTrangChu";
            Text = "frmTrangChu";
            Load += frmTrangChu_Load;
            pnlDoanhThu.ResumeLayout(false);
            pnlDoanhThu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            pnlKhach.ResumeLayout(false);
            pnlKhach.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            pnlDV.ResumeLayout(false);
            pnlDV.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            pnlPhong.ResumeLayout(false);
            pnlPhong.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlTKDoanhThu.ResumeLayout(false);
            pnlTKDoanhThu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartDoanhThu).EndInit();
            pnlDatPhongMoi.ResumeLayout(false);
            pnlDatPhongMoi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDatPhong).EndInit();
            pnlTTPhong.ResumeLayout(false);
            pnlTTPhong.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartPhong).EndInit();
            pnlHD.ResumeLayout(false);
            pnlHD.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHDMoi).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label4;
        private Label label1;
        private Panel pnlDoanhThu;
        private Panel pnlKhach;
        private Panel pnlDV;
        private Panel pnlPhong;
        private Panel pnlTKDoanhThu;
        private Panel pnlDatPhongMoi;
        private Panel pnlTTPhong;
        private Panel pnlHD;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private Label label2;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label5;
        private Label label3;
        private Label lblTang;
        private Label label10;
        private Label label11;
        private Label label13;
        private Label label14;
        private Label lblPhong;
        private Label lblDoanhThu;
        private Label lblKhach;
        private Label lblDV;
        private Label label15;
        private Label label17;
        private Label label16;
        private Label label18;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPhong;
        private Label lblSoPhong;
        private DataGridView dgvDatPhong;
        private DataGridView dgvHDMoi;
    }
}