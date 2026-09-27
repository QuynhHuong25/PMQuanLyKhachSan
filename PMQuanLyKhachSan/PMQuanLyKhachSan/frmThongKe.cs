using PhanMemQuanLyKhachSan.Model;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PMQuanLyKhachSan
{
    public partial class frmThongKe : Form
    {
        private DataContext db = new DataContext();

        private string loaiThongKeDangChon = "HD";

        private const string TRANG_THAI_DANG_THUE = "Đang có khách";
        private const string TRANG_THAI_TRONG = "Trống";
        private const string TRANG_THAI_DA_HUY = "Đã hủy";
        private const string TRANG_THAI_DA_TRA = "Đã trả phòng";

        public frmThongKe()
        {
            InitializeComponent();
        }

        public frmThongKe(object? obj) : this()
        {
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            dtpNgayBD.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpNgayKT.Value = DateTime.Now.Date;

            btnHD.Click += btnHD_Click;
            btnDV.Click += btnDV_Click;
            btnKH.Click += btnKH_Click;
            btnPhong.Click += btnPhong_Click;
            btnLoc.Click += btnLoc_Click;

            CauHinhDGV();
            LoadLabelThongKe();
            HienThiHoaDon();
        }
        private void CauHinhDGV()
        {
            dgv.EnableHeadersVisualStyles = false;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 55);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gold;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 35;

            dgv.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
            dgv.GridColor = Color.Gray;

            dgv.RowsDefaultCellStyle.BackColor = Color.White;
            dgv.RowsDefaultCellStyle.ForeColor = Color.Black;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadLabelThongKe()
        {
            try
            {
                lblHD.Text = db.HoaDons.Count().ToString();

                var soDonCoDV = (from cthd in db.ChiTietHoaDons
                                 join hd in db.HoaDons on cthd.MaHD equals hd.MaHD
                                 where cthd.MaCTDV != null
                                 select hd.MaHD).Distinct().Count();

                lblDV.Text = soDonCoDV.ToString();

                lblKH.Text = db.KhachHangs.Count().ToString();
                lblPhong.Text = db.Phongs.Count().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu đếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnHD_Click(object? sender, EventArgs e)
        {
            loaiThongKeDangChon = "HD";
            HienThiHoaDon();
        }

        private void HienThiHoaDon()
        {
            DateTime ngayBD = dtpNgayBD.Value.Date;
            DateTime ngayKT = dtpNgayKT.Value.Date.AddDays(1).AddTicks(-1);

            var ds = (from hd in db.HoaDons
                      join dp in db.DatPhongs on hd.MaDatPhong equals dp.MaDatPhong
                      join kh in db.KhachHangs on dp.MaKH equals kh.MaKH
                      join p in db.Phongs on dp.MaPhong equals p.MaPhong
                      join lp in db.LoaiPhongs on p.MaLoai equals lp.MaLoai into lpGroup
                      from lp in lpGroup.DefaultIfEmpty()
                      where hd.NgayLap >= ngayBD && hd.NgayLap <= ngayKT
                      select new
                      {
                          MaHD = hd.MaHD,
                          MaKH = kh.MaKH,
                          TenKH = kh.HoTen ?? "",
                          MaDatPhong = hd.MaDatPhong,
                          TienPhong = lp != null ? lp.Gia : 0m,
                          TienDichVu = db.ChiTietHoaDons
                                        .Where(ct => ct.MaHD == hd.MaHD && ct.MaCTDV != null)
                                        .Sum(ct => (decimal?)ct.ThanhTien) ?? 0m,
                          NgayTao = hd.NgayLap,
                          TongTien = hd.TongTien ?? 0m,
                          PhuongThucTT = hd.PhuongThucTT ?? "Tiền mặt"
                      }).ToList();

            dgv.DataSource = ds;

            if (dgv.Columns["MaHD"] != null) dgv.Columns["MaHD"].HeaderText = "Mã HD";
            if (dgv.Columns["MaKH"] != null) dgv.Columns["MaKH"].HeaderText = "Mã KH";
            if (dgv.Columns["TenKH"] != null) dgv.Columns["TenKH"].HeaderText = "Tên KH";
            if (dgv.Columns["MaDatPhong"] != null) dgv.Columns["MaDatPhong"].HeaderText = "Mã đặt phòng";
            if (dgv.Columns["TienPhong"] != null) { dgv.Columns["TienPhong"].HeaderText = "Tiền phòng"; dgv.Columns["TienPhong"].DefaultCellStyle.Format = "N0"; }
            if (dgv.Columns["TienDichVu"] != null) { dgv.Columns["TienDichVu"].HeaderText = "Tiền dịch vụ"; dgv.Columns["TienDichVu"].DefaultCellStyle.Format = "N0"; }
            if (dgv.Columns["NgayTao"] != null) { dgv.Columns["NgayTao"].HeaderText = "Ngày tạo"; dgv.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"; }
            if (dgv.Columns["TongTien"] != null) { dgv.Columns["TongTien"].HeaderText = "Tổng tiền"; dgv.Columns["TongTien"].DefaultCellStyle.Format = "N0"; }
            if (dgv.Columns["PhuongThucTT"] != null) dgv.Columns["PhuongThucTT"].HeaderText = "Phương thức TT";

            decimal tongTienHD = ds.Sum(x => x.TongTien);
            lblTong.Text = tongTienHD.ToString("N0") + " VNĐ";
        }

        private void btnDV_Click(object? sender, EventArgs e)
        {
            loaiThongKeDangChon = "DV";
            HienThiDichVu();
        }

        private void HienThiDichVu()
        {
            DateTime ngayBD = dtpNgayBD.Value.Date;
            DateTime ngayKT = dtpNgayKT.Value.Date.AddDays(1).AddTicks(-1);

            var ds = (from cthd in db.ChiTietHoaDons
                      join hd in db.HoaDons on cthd.MaHD equals hd.MaHD
                      join dp in db.DatPhongs on hd.MaDatPhong equals dp.MaDatPhong
                      join kh in db.KhachHangs on dp.MaKH equals kh.MaKH
                      join p in db.Phongs on dp.MaPhong equals p.MaPhong
                      where cthd.MaCTDV != null && hd.NgayLap >= ngayBD && hd.NgayLap <= ngayKT
                      select new
                      {
                          MaDatPhong = dp.MaDatPhong,
                          MaKH = kh.MaKH,
                          TenKH = kh.HoTen ?? "",
                          Phong = p.TenPhong ?? "",
                          TenDV = "Dịch vụ phòng",
                          NgayDung = hd.NgayLap,
                          GioDung = hd.NgayLap.HasValue ? hd.NgayLap.Value.ToString("HH:mm") : "",
                          DonGia = cthd.ThanhTien,
                          SoLuong = 1,
                          ThanhTien = cthd.ThanhTien
                      }).ToList();

            dgv.DataSource = ds;

            if (dgv.Columns["MaDatPhong"] != null) dgv.Columns["MaDatPhong"].HeaderText = "Mã đặt phòng";
            if (dgv.Columns["MaKH"] != null) dgv.Columns["MaKH"].HeaderText = "Mã KH";
            if (dgv.Columns["TenKH"] != null) dgv.Columns["TenKH"].HeaderText = "Tên KH";
            if (dgv.Columns["Phong"] != null) dgv.Columns["Phong"].HeaderText = "Phòng";
            if (dgv.Columns["TenDV"] != null) dgv.Columns["TenDV"].HeaderText = "Tên dịch vụ";
            if (dgv.Columns["NgayDung"] != null) { dgv.Columns["NgayDung"].HeaderText = "Ngày dùng"; dgv.Columns["NgayDung"].DefaultCellStyle.Format = "dd/MM/yyyy"; }
            if (dgv.Columns["GioDung"] != null) dgv.Columns["GioDung"].HeaderText = "Giờ dùng";
            if (dgv.Columns["DonGia"] != null) { dgv.Columns["DonGia"].HeaderText = "Đơn giá"; dgv.Columns["DonGia"].DefaultCellStyle.Format = "N0"; }
            if (dgv.Columns["SoLuong"] != null) dgv.Columns["SoLuong"].HeaderText = "Số lượng";
            if (dgv.Columns["ThanhTien"] != null) { dgv.Columns["ThanhTien"].HeaderText = "Thành tiền"; dgv.Columns["ThanhTien"].DefaultCellStyle.Format = "N0"; }

            decimal tongTienDV = ds.Sum(x => x.ThanhTien);
            lblTong.Text = tongTienDV.ToString("N0") + " VNĐ";
        }

        private void btnKH_Click(object? sender, EventArgs e)
        {
            loaiThongKeDangChon = "KH";
            HienThiKhachHang();
        }

        private void HienThiKhachHang()
        {
            var query = from kh in db.KhachHangs
                        select new
                        {
                            kh.MaKH,
                            HoTen = kh.HoTen,
                            NgaySinhRaw = (DateTime?)kh.NgaySinh,
                            GioiTinhRaw = (bool?)kh.GioiTinh,
                            kh.CCCD,
                            kh.SDT,
                            kh.TenDangNhap
                        };

            var rawList = query.ToList();

            var ds = rawList.Select(kh => new
            {
                MaKH = kh.MaKH,
                HoTen = kh.HoTen ?? "",
                NgaySinh = kh.NgaySinhRaw.HasValue ? kh.NgaySinhRaw.Value.ToString("dd/MM/yyyy") : "",
                GioiTinh = kh.GioiTinhRaw.HasValue ? (kh.GioiTinhRaw.Value ? "Nam" : "Nữ") : "Nam",
                CCCD = kh.CCCD ?? "",
                SDT = kh.SDT ?? "",
                SoLanO = db.DatPhongs.Count(dp => dp.MaKH == kh.MaKH),
                TongChiTieu = (from dp in db.DatPhongs
                               join hd in db.HoaDons on dp.MaDatPhong equals hd.MaDatPhong
                               where dp.MaKH == kh.MaKH
                               select (decimal?)hd.TongTien).Sum() ?? 0m,
                TaiKhoan = kh.TenDangNhap ?? ""
            }).ToList();

            dgv.DataSource = ds;

            if (dgv.Columns["MaKH"] != null) dgv.Columns["MaKH"].HeaderText = "Mã KH";
            if (dgv.Columns["HoTen"] != null) dgv.Columns["HoTen"].HeaderText = "Họ tên";
            if (dgv.Columns["NgaySinh"] != null) dgv.Columns["NgaySinh"].HeaderText = "Ngày sinh";
            if (dgv.Columns["GioiTinh"] != null) dgv.Columns["GioiTinh"].HeaderText = "Giới tính";
            if (dgv.Columns["CCCD"] != null) dgv.Columns["CCCD"].HeaderText = "CCCD";
            if (dgv.Columns["SDT"] != null) dgv.Columns["SDT"].HeaderText = "SĐT";
            if (dgv.Columns["SoLanO"] != null) dgv.Columns["SoLanO"].HeaderText = "Số lần ở";
            if (dgv.Columns["TongChiTieu"] != null) { dgv.Columns["TongChiTieu"].HeaderText = "Tổng chi tiêu"; dgv.Columns["TongChiTieu"].DefaultCellStyle.Format = "N0"; }
            if (dgv.Columns["TaiKhoan"] != null) dgv.Columns["TaiKhoan"].HeaderText = "Tài khoản";

            decimal tongChiTieuKH = ds.Sum(x => x.TongChiTieu);
            lblTong.Text = tongChiTieuKH.ToString("N0") + " VNĐ";
        }

        private void btnPhong_Click(object? sender, EventArgs e)
        {
            loaiThongKeDangChon = "PHONG";
            HienThiPhong();
        }

        private void HienThiPhong()
        {
            DateTime ngayBD = dtpNgayBD.Value.Date;
            DateTime ngayKT = dtpNgayKT.Value.Date;

            var maPhongDangThue = db.DatPhongs
                .Where(dp =>
                    dp.TrangThai != TRANG_THAI_DA_HUY &&
                    dp.TrangThai != TRANG_THAI_DA_TRA &&
                    ngayBD < dp.NgayTra &&
                    ngayKT > dp.NgayNhan)
                .Select(dp => dp.MaPhong)
                .Distinct()
                .ToList();

            var ds = (from p in db.Phongs
                      join lp in db.LoaiPhongs on p.MaLoai equals lp.MaLoai into lpGroup
                      from lp in lpGroup.DefaultIfEmpty()
                      select new
                      {
                          MaPhong = p.MaPhong,
                          TenPhong = p.TenPhong ?? "",
                          LoaiPhong = lp != null ? lp.TenLoai : "",
                          Gia = lp != null ? lp.Gia : 0m,
                          MoTa = lp != null ? lp.MoTa : "",
                          TrangThai = maPhongDangThue.Contains(p.MaPhong) ? TRANG_THAI_DANG_THUE : TRANG_THAI_TRONG
                      }).ToList();

            dgv.DataSource = ds;

            if (dgv.Columns["MaPhong"] != null)
            {
                dgv.Columns["MaPhong"].HeaderText = "Mã phòng";
                dgv.Columns["MaPhong"].FillWeight = 85;
            }
            if (dgv.Columns["TenPhong"] != null)
            {
                dgv.Columns["TenPhong"].HeaderText = "Tên phòng";
                dgv.Columns["TenPhong"].FillWeight = 110;
            }
            if (dgv.Columns["LoaiPhong"] != null)
            {
                dgv.Columns["LoaiPhong"].HeaderText = "Loại phòng";
                dgv.Columns["LoaiPhong"].FillWeight = 100;
            }
            if (dgv.Columns["Gia"] != null)
            {
                dgv.Columns["Gia"].HeaderText = "Giá";
                dgv.Columns["Gia"].DefaultCellStyle.Format = "N0";
                dgv.Columns["Gia"].FillWeight = 95;
            }
            if (dgv.Columns["MoTa"] != null)
            {
                dgv.Columns["MoTa"].HeaderText = "Mô tả";
                dgv.Columns["MoTa"].FillWeight = 330;
            }
            if (dgv.Columns["TrangThai"] != null)
            {
                dgv.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgv.Columns["TrangThai"].FillWeight = 115;
            }

            lblTong.Text = "Tổng số phòng: " + ds.Count;
        }

        private void btnLoc_Click(object? sender, EventArgs e)
        {
            if (dtpNgayKT.Value.Date < dtpNgayBD.Value.Date)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (loaiThongKeDangChon)
            {
                case "HD":
                    HienThiHoaDon();
                    break;
                case "DV":
                    HienThiDichVu();
                    break;
                case "KH":
                    HienThiKhachHang();
                    break;
                case "PHONG":
                    HienThiPhong();
                    break;
            }
        }
    }
}