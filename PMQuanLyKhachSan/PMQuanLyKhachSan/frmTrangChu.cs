using Microsoft.EntityFrameworkCore;
using PhanMemQuanLyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PMQuanLyKhachSan
{
    public partial class frmTrangChu : Form
    {
        public DataContext db = new DataContext();
        private NhanVien nhanVienDangNhap;

        public frmTrangChu(NhanVien nv)
        {
            InitializeComponent();
            nhanVienDangNhap = nv;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
        }

        private void LoadTongSoPhong()
        {
            DateTime homNay = DateTime.Now.Date;
            int soPhongDatTruocHomNay = db.DatPhongs
                .Where(dp => dp.TrangThai == "Đã thanh toán" && dp.NgayDat.Date == homNay)
                .Select(dp => dp.MaPhong)
                .Count();

            lblPhong.Text = soPhongDatTruocHomNay.ToString();
        }

        private decimal LoadDoanhThu()
        {
            DateTime homNay = DateTime.Now.Date;

            decimal tongDoanhThu = db.HoaDons
                .Where(hd => hd.NgayLap.HasValue && hd.NgayLap.Value.Date == homNay)
                .Sum(hd => (decimal?)hd.TongTien) ?? 0m;

            lblDoanhThu.Text = tongDoanhThu.ToString("N0") + " đ";

            return tongDoanhThu;
        }

        private void LoadTyleSoVoiHomQua(decimal dtHomNay)
        {
            DateTime homQua = DateTime.Now.Date.AddDays(-1);

            decimal dtHomQua = db.HoaDons
                .Where(hd => hd.NgayLap.HasValue && hd.NgayLap.Value.Date == homQua)
                .Sum(hd => (decimal?)hd.TongTien) ?? 0m;

            if (dtHomQua == 0)
            {
                lblTang.Text = dtHomNay > 0 ? "+ 100% so với hôm qua" : "0% so với hôm qua";
                lblTang.ForeColor = dtHomNay > 0 ? Color.LightGreen : Color.Gray;
                return;
            }

            decimal phanTram = ((dtHomNay - dtHomQua) / dtHomQua) * 100;

            if (phanTram > 0)
            {
                lblTang.Text = $"+ {phanTram:0.##}% so với hôm qua";
                lblTang.ForeColor = Color.LightGreen;
            }
            else if (phanTram < 0)
            {
                lblTang.Text = $"- {Math.Abs(phanTram):0.##}% so với hôm qua";
                lblTang.ForeColor = Color.Tomato;
            }
            else
            {
                lblTang.Text = "0% so với hôm qua";
                lblTang.ForeColor = Color.Gray;
            }
        }

        private void LoadKhachHomNay()
        {
            lblKhach.Text = db.DatPhongs
                  .Count(x => x.NgayNhan.Date == DateTime.Now.Date
                           && x.TrangThai != "Đã hủy")
                  .ToString();
        }

        private void LoadDichVuHomNay()
        {
            lblDV.Text = db.ChiTietDichVus
               .Count(x => x.NgaySuDung.HasValue
                        && x.NgaySuDung.Value.Date == DateTime.Now.Date
                        && x.DatPhong.TrangThai != "Đã hủy")
               .ToString();
        }

        private void LoadChartDoanhThu()
        {
            chartDoanhThu.Series.Clear();

            ChartArea area = chartDoanhThu.ChartAreas[0];
            area.AxisX.Title = "";
            area.AxisY.Title = "";

            area.AxisX.Interval = 1;
            area.AxisX.IsLabelAutoFit = false;
            area.AxisX.LabelStyle.Angle = 0;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            area.AxisX.LabelStyle.IsStaggered = false;

            area.AxisY.Minimum = 0;
            area.AxisY.Maximum = 600000000;
            area.AxisY.Interval = 100000000;
            area.AxisY.LabelStyle.Format = "#,0,,M";

            area.AxisY.MajorGrid.LineColor = Color.Gray;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;

            area.BackColor = Color.Transparent;

            Series s = new Series("DoanhThu");
            s.ChartType = SeriesChartType.Line;
            s.BorderWidth = 4;

            s.MarkerStyle = MarkerStyle.Circle;
            s.MarkerSize = 10;

            s.Color = Color.Gold;
            s.MarkerColor = Color.Gold;

            s.IsXValueIndexed = true;
            chartDoanhThu.Series.Add(s);

            DateTime now = DateTime.Now;

            for (int i = 7; i >= 0; i--)
            {
                DateTime thang = new DateTime(now.Year, now.Month, 1).AddMonths(-i);

                DateTime ngayDauThang = new DateTime(thang.Year, thang.Month, 1);
                DateTime ngayCuoiThang = ngayDauThang.AddMonths(1).AddTicks(-1);

                decimal tongDoanhThu = db.HoaDons
                    .Where(hd => hd.NgayLap.HasValue
                              && hd.NgayLap.Value >= ngayDauThang
                              && hd.NgayLap.Value <= ngayCuoiThang)
                    .Sum(hd => (decimal?)hd.TongTien) ?? 0m;

                s.Points.AddXY("Tháng " + thang.Month, (double)tongDoanhThu);
            }
        }

        private void LoadChartPhong()
        {
            chartPhong.Series.Clear();

            Series s = new Series();
            s.ChartType = SeriesChartType.Pie;
            chartPhong.Series.Add(s);

            int tongPhong = db.Phongs.Count();
            DateTime homNay = DateTime.Now.Date;

            int daDat = db.DatPhongs
                .Where(x => x.TrangThai == "Đã thanh toán" && x.NgayNhan.Date >= homNay)
                .Select(x => x.MaPhong)
                .Distinct()
                .Count();

            int dangO = db.DatPhongs
                .Where(x => x.TrangThai == "Đang thuê")
                .Select(x => x.MaPhong)
                .Distinct()
                .Count();

            int dangDon = db.DatPhongs
                .Where(x => x.TrangThai == "Đã trả phòng" && x.NgayTra.Date == homNay)
                .Select(x => x.MaPhong)
                .Distinct()
                .Count();

            int trong = tongPhong - daDat - dangO - dangDon;
            if (trong < 0) trong = 0;

            if (daDat > 0)
            {
                int idx = s.Points.AddXY("Đã đặt", daDat);
                s.Points[idx].Color = Color.Gold;
            }

            if (dangO > 0)
            {
                int idx = s.Points.AddXY("Đang thuê", dangO);
                s.Points[idx].Color = Color.OrangeRed;
            }

            if (dangDon > 0)
            {
                int idx = s.Points.AddXY("Đang dọn", dangDon);
                s.Points[idx].Color = Color.DeepSkyBlue;
            }

            if (trong > 0)
            {
                int idx = s.Points.AddXY("Trống", trong);
                s.Points[idx].Color = Color.ForestGreen;
            }

            lblSoPhong.Text = "Tổng số phòng: " + db.Phongs.Count();
        }

        private void LoadDgvNhanPhongHN()
        {
            DateTime homNay = DateTime.Now.Date;

            var dsDatPhong = (
                from dp in db.DatPhongs
                join kh in db.KhachHangs on dp.MaKH equals kh.MaKH
                join p in db.Phongs on dp.MaPhong equals p.MaPhong
                where dp.NgayNhan.Date == homNay
                   && dp.TrangThai != "Đã hủy"
                select new
                {
                    MaDatPhong = dp.MaDatPhong,
                    TenKH = kh.HoTen,
                    TenPhong = p.TenPhong,
                    NgayNhan = dp.NgayNhan,
                    NgayTra = dp.NgayTra,
                    TrangThai = dp.TrangThai
                }
            ).ToList();

            dgvDatPhong.DataSource = dsDatPhong;

            if (dgvDatPhong.Columns["MaDatPhong"] != null) dgvDatPhong.Columns["MaDatPhong"].HeaderText = "Mã đặt phòng";
            if (dgvDatPhong.Columns["TenKH"] != null) dgvDatPhong.Columns["TenKH"].HeaderText = "Tên khách hàng";
            if (dgvDatPhong.Columns["TenPhong"] != null) dgvDatPhong.Columns["TenPhong"].HeaderText = "Tên phòng";
            if (dgvDatPhong.Columns["NgayNhan"] != null) dgvDatPhong.Columns["NgayNhan"].HeaderText = "Ngày nhận";
            if (dgvDatPhong.Columns["NgayTra"] != null) dgvDatPhong.Columns["NgayTra"].HeaderText = "Ngày trả";
            if (dgvDatPhong.Columns["TrangThai"] != null) dgvDatPhong.Columns["TrangThai"].HeaderText = "Trạng thái";

            if (dgvDatPhong.Columns["NgayNhan"] != null) dgvDatPhong.Columns["NgayNhan"].DefaultCellStyle.Format = "dd/MM/yyyy";
            if (dgvDatPhong.Columns["NgayTra"] != null) dgvDatPhong.Columns["NgayTra"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvDatPhong.RowHeadersVisible = false;
        }

        private Image LoadImageFromFile(string fileName)
        {
            try
            {
                string path = System.IO.Path.Combine(Application.StartupPath, "Images", fileName);
                if (System.IO.File.Exists(path))
                {
                    return Image.FromFile(path);
                }
            }
            catch
            {
            }
            return null;
        }

        private void LoadHoatDongGanDay()
        {
            // 1. Kiểm tra và tạo cột cho DataGridView nếu chưa có
            if (dgvHDMoi.Columns.Count == 0)
            {
                DataGridViewImageColumn colIcon = new DataGridViewImageColumn
                {
                    Name = "colIcon",
                    HeaderText = "",
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Width = 42,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                dgvHDMoi.Columns.Add(colIcon);

                dgvHDMoi.Columns.Add("colNoiDung", "");
                dgvHDMoi.Columns["colNoiDung"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgvHDMoi.Columns.Add("colThoiGian", "");
                dgvHDMoi.Columns["colThoiGian"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvHDMoi.Columns["colThoiGian"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvHDMoi.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvHDMoi.ColumnHeadersVisible = false;
                dgvHDMoi.RowHeadersVisible = false;
            }

            // 2. Xóa các dòng cũ
            dgvHDMoi.Rows.Clear();

            // 3. Truy vấn dữ liệu
            var dsDatPhong = db.DatPhongs
                .Include(dp => dp.KhachHang)
                .Include(dp => dp.Phong)
                .ToList()
                .Select(dp => new
                {
                    LoaiHD = "DatPhong",
                    NoiDung = $"Đặt phòng mới cho khách {dp.KhachHang?.HoTen ?? "N/A"} - Phòng {dp.Phong?.TenPhong ?? "N/A"}",
                    ThoiGian = (DateTime?)dp.NgayDat
                });

            var dsDichVu = db.ChiTietDichVus
                .Include(ct => ct.DichVu)
                .Include(ct => ct.DatPhong)
                    .ThenInclude(dp => dp.Phong)
                .Where(ct => ct.NgaySuDung.HasValue)
                .ToList()
                .Select(ct => new
                {
                    LoaiHD = "DichVu",
                    NoiDung = $"Đặt dịch vụ {ct.DichVu?.TenDV ?? "N/A"} - Phòng {ct.DatPhong?.Phong?.TenPhong ?? "N/A"}",
                    ThoiGian = ct.NgaySuDung
                });

            var dsHoaDon = db.HoaDons
                .ToList()
                .Select(hd => new
                {
                    LoaiHD = "HoaDon",
                    NoiDung = $"Thanh toán hóa đơn HD{hd.MaHD} - {(hd.TongTien ?? 0):N0} đ",
                    ThoiGian = hd.NgayLap
                });

            var hoatDongMoiNhat = dsDatPhong
                .Concat(dsDichVu)
                .Concat(dsHoaDon)
                .OrderByDescending(x => x.ThoiGian)
                .Take(10)
                .ToList();

            // 4. Load icon và đổ dữ liệu vào dòng
            Image iconDatPhong = LoadImageFromFile("icon_bed.png");
            Image iconDichVu = LoadImageFromFile("icon_service.png");
            Image iconHoaDon = LoadImageFromFile("icon_card.png");
            Image iconTaiKhoan = LoadImageFromFile("icon_user.png");

            foreach (var item in hoatDongMoiNhat)
            {
                Image iconTuongUng = iconDatPhong;

                switch (item.LoaiHD)
                {
                    case "DichVu":
                        iconTuongUng = iconDichVu;
                        break;
                    case "HoaDon":
                        iconTuongUng = iconHoaDon;
                        break;
                    case "TaiKhoan":
                        iconTuongUng = iconTaiKhoan;
                        break;
                }

                string stringThoiGian = item.ThoiGian.HasValue ? item.ThoiGian.Value.ToString("HH:mm dd/MM/yyyy") : "";
                dgvHDMoi.Rows.Add(iconTuongUng, item.NoiDung, stringThoiGian);
            }
        }

        private void LoadDashboard()
        {
            LoadTongSoPhong();

            decimal dtHomNay = LoadDoanhThu();
            LoadTyleSoVoiHomQua(dtHomNay);

            LoadKhachHomNay();

            LoadDichVuHomNay();

            LoadChartDoanhThu();

            LoadChartPhong();

            LoadDgvNhanPhongHN();

            LoadHoatDongGanDay();

            Color viyenVang = Color.FromArgb(240, 204, 119);
            int banKinhBoGoc = 15;
            int doDayVien = 2;

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Panel panel)
                {
                    panel.Paint += (s, pe) => DrawRoundedPanel(panel, pe, banKinhBoGoc, viyenVang, doDayVien);
                    panel.Invalidate();
                }
            }
        }

        private void DrawRoundedPanel(Panel panel, PaintEventArgs e, int cornerRadius, Color borderColor, int borderThickness)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
            int diameter = cornerRadius * 2;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
                path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
                path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
                path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
                path.CloseFigure();

                panel.Region = new Region(path);

                using (Pen pen = new Pen(borderColor, borderThickness))
                {
                    pen.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            LoadDashboard();
        }

        private void label12_Click(object sender, EventArgs e)
        {
        }

        private void label19_Click(object sender, EventArgs e)
        {
        }

        private void chartDoanhThu_Click(object sender, EventArgs e)
        {
        }

        private void lblSoPhong_Click(object sender, EventArgs e)
        {
        }
    }
}