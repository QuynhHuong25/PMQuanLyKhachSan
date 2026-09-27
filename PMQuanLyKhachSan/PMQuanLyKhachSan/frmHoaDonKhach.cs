using Microsoft.EntityFrameworkCore;
using PhanMemQuanLyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PMQuanLyKhachSan
{
    public partial class frmHoaDonKhach : Form
    {
        public DataContext db = new DataContext();

        private int maDatPhong;

        private decimal tongTienPhong = 0;
        private decimal tongTienDV = 0;

        private decimal tienDVBanDau = 0;   
        private decimal tienDVPhatSinh = 0;
        private decimal tienDaTraTrongDB = 0;

        private DatPhong _datPhongCache;

        public frmHoaDonKhach()
        {
            InitializeComponent();
            using (var tempDb = new DataContext())
            {
                var dpMoiNhat = tempDb.DatPhongs
                    .AsNoTracking()
                    .OrderByDescending(x => x.MaDatPhong)
                    .FirstOrDefault();

                if (dpMoiNhat != null)
                {
                    this.maDatPhong = dpMoiNhat.MaDatPhong;
                }
            }
        }

        public frmHoaDonKhach(int maDP)
        {
            InitializeComponent();
            maDatPhong = maDP;
        }

        private void frmHoaDonKhach_Load(object sender, EventArgs e)
        {
            db = new DataContext();

            if (maDatPhong <= 0)
            {
                var dpMoiNhat = db.DatPhongs
                    .AsNoTracking()
                    .OrderByDescending(x => x.MaDatPhong)
                    .FirstOrDefault();

                if (dpMoiNhat != null)
                {
                    maDatPhong = dpMoiNhat.MaDatPhong;
                }
                else
                {
                    dgvPhong.DataSource = null;
                    dgvDV.DataSource = null;
                    TinhTongTien();
                    MessageBox.Show("Chưa có đơn đặt phòng nào trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            _datPhongCache = db.DatPhongs
                .AsNoTracking()
                .Include(x => x.KhachHang)
                .Include(x => x.Phong)
                .ThenInclude(p => p.LoaiPhong)
                .FirstOrDefault(x => x.MaDatPhong == maDatPhong);

            LoadThongTinKhach();
            LoadPhong();

            dgvDV.CellContentClick -= dgvDV_CellContentClick;
            dgvDV.CellContentClick += dgvDV_CellContentClick;

            LoadDichVu();
            LoadHoaDonDaLuu();
            TinhTongTien();
            CanLePhaiChoLabel();
        }

        //================ 1. ĐỌC THÔNG TIN HÓA ĐƠN TỪ DB ==================
        private void LoadHoaDonDaLuu()
        {
            var hd = db.HoaDons.AsNoTracking().FirstOrDefault(x => x.MaDatPhong == maDatPhong);
            if (hd != null)
            {
                tienDaTraTrongDB = hd.TongTien ?? 0m;
            }
            else
            {
                tienDaTraTrongDB = 0m;
            }
        }

        //================ 2. THÔNG TIN KHÁCH ==================
        private void LoadThongTinKhach()
        {
            var dp = _datPhongCache;

            if (dp != null)
            {
                var kh = dp.KhachHang;

                string tenKH = kh != null ? (kh.HoTen ?? "N/A") : "N/A";
                string cccd = kh != null ? (kh.CCCD ?? "Chưa có") : "Chưa có";
                string sdt = kh != null ? (kh.SDT ?? "Chưa có") : "Chưa có";

                lblTenKH.Text = tenKH;
                lblCCCD.Text = cccd;
                lblSDT.Text = sdt;

                lblNgayNhan.Text = dp.NgayNhan > DateTime.MinValue
                    ? dp.NgayNhan.ToString("dd/MM/yyyy HH:mm")
                    : "Chưa xác định";

                lblNgayTra.Text = dp.NgayTra > DateTime.MinValue
                    ? dp.NgayTra.ToString("dd/MM/yyyy HH:mm")
                    : "Chưa xác định";
            }
            else
            {
                MessageBox.Show("Không tìm thấy dữ liệu đặt phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //================ 3. PHÒNG ==================
        private void LoadPhong()
        {
            var listDP = _datPhongCache != null
                ? new List<DatPhong> { _datPhongCache }
                : new List<DatPhong>();

            var ds = listDP.Select(dp =>
            {
                DateTime ngayNhan = dp.NgayNhan;
                DateTime ngayTra = dp.NgayTra;

                int soNgay = (ngayTra - ngayNhan).Days;
                if (soNgay <= 0) soNgay = 1;

                string tenPhong = (dp.Phong != null && !string.IsNullOrEmpty(dp.Phong.TenPhong))
                                    ? dp.Phong.TenPhong
                                    : "N/A";

                string loaiPhong = (dp.Phong != null && dp.Phong.LoaiPhong != null && !string.IsNullOrEmpty(dp.Phong.LoaiPhong.TenLoai))
                                    ? dp.Phong.LoaiPhong.TenLoai
                                    : "N/A";

                decimal donGia = (dp.Phong != null && dp.Phong.LoaiPhong != null)
                                    ? dp.Phong.LoaiPhong.Gia
                                    : 0m;

                return new
                {
                    MaPhong = dp.MaPhong,
                    TenPhong = tenPhong,
                    LoaiPhong = loaiPhong,
                    NgayNhan = ngayNhan,
                    NgayTra = ngayTra,
                    DonGia = donGia,
                    SoNgay = soNgay,
                    ThanhTien = donGia * soNgay
                };
            }).ToList();

            dgvPhong.DataSource = ds;
            tongTienPhong = ds.Sum(x => x.ThanhTien);

            if (ds.Count > 0)
            {
                dgvPhong.Columns["MaPhong"].HeaderText = "Mã";
                dgvPhong.Columns["TenPhong"].HeaderText = "Tên phòng";
                dgvPhong.Columns["LoaiPhong"].HeaderText = "Loại phòng";
                dgvPhong.Columns["NgayNhan"].HeaderText = "Ngày nhận";
                dgvPhong.Columns["NgayTra"].HeaderText = "Ngày trả";
                dgvPhong.Columns["DonGia"].HeaderText = "Đơn giá";
                dgvPhong.Columns["SoNgay"].HeaderText = "Số đêm";
                dgvPhong.Columns["ThanhTien"].HeaderText = "Thành tiền";

                dgvPhong.Columns["NgayNhan"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvPhong.Columns["NgayTra"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvPhong.Columns["DonGia"].DefaultCellStyle.Format = "N0";
                dgvPhong.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";

                dgvPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvPhong.Columns["MaPhong"].FillWeight = 45;
                dgvPhong.Columns["TenPhong"].FillWeight = 100;
                dgvPhong.Columns["LoaiPhong"].FillWeight = 110;
                dgvPhong.Columns["NgayNhan"].FillWeight = 90;
                dgvPhong.Columns["NgayTra"].FillWeight = 90;
                dgvPhong.Columns["DonGia"].FillWeight = 90;
                dgvPhong.Columns["SoNgay"].FillWeight = 55;
                dgvPhong.Columns["ThanhTien"].FillWeight = 100;
            }

            FormatGridView(dgvPhong);
        }

        //================ 4. DỊCH VỤ & CỘT HỦY ==================
        private void LoadDichVu()
        {
            DateTime ngayNhanPhong = _datPhongCache != null ? _datPhongCache.NgayNhan.Date : DateTime.MinValue.Date;

            var listCTDV = db.ChiTietDichVus
                .AsNoTracking()
                .Include(ct => ct.DichVu)
                .Where(ct => ct.MaDatPhong == maDatPhong)
                .ToList();

            tienDVBanDau = listCTDV
                .Where(ct => ct.NgaySuDung.HasValue && ct.NgaySuDung.Value.Date <= ngayNhanPhong)
                .Sum(ct => ((decimal?)ct.ThanhTien) ?? 0m);

            tienDVPhatSinh = listCTDV
                .Where(ct => !ct.NgaySuDung.HasValue || ct.NgaySuDung.Value.Date > ngayNhanPhong)
                .Sum(ct => ((decimal?)ct.ThanhTien) ?? 0m);

            var ds = listCTDV.Select(ct =>
            {
                string ngayStr = ct.NgaySuDung.HasValue ? ct.NgaySuDung.Value.ToString("dd/MM/yyyy") : "N/A";
                string gioStr = ct.NgaySuDung.HasValue ? ct.NgaySuDung.Value.ToString("HH:mm") : "N/A";

                string tenDV = (ct.DichVu != null && !string.IsNullOrEmpty(ct.DichVu.TenDV))
                                ? ct.DichVu.TenDV
                                : "Dịch vụ phòng";

                decimal donGiaAnToan = ((decimal?)ct.DonGia) ?? 0m;
                decimal thanhTienAnToan = ((decimal?)ct.ThanhTien) ?? 0m;

                return new
                {
                    MaCTDV = ct.MaCTDV,
                    TenDV = tenDV,
                    NgaySuDung = ngayStr,
                    GioSuDung = gioStr,
                    ThoiGianSuDungGoc = ct.NgaySuDung,
                    DonGia = donGiaAnToan,
                    SoLuong = ct.SoLuong,
                    ThanhTien = thanhTienAnToan
                };
            }).ToList();

            dgvDV.DataSource = ds;
            tongTienDV = ds.Sum(x => x.ThanhTien);

            if (!dgvDV.Columns.Contains("btnHuyDV"))
            {
                DataGridViewButtonColumn colHuy = new DataGridViewButtonColumn
                {
                    Name = "btnHuyDV",
                    HeaderText = "Thao tác",
                    Text = "Hủy",
                    UseColumnTextForButtonValue = true
                };
                dgvDV.Columns.Add(colHuy);
            }

            if (ds.Count > 0)
            {
                if (dgvDV.Columns.Contains("MaCTDV")) dgvDV.Columns["MaCTDV"].Visible = false;
                if (dgvDV.Columns.Contains("ThoiGianSuDungGoc")) dgvDV.Columns["ThoiGianSuDungGoc"].Visible = false;

                dgvDV.Columns["TenDV"].HeaderText = "Tên dịch vụ";
                dgvDV.Columns["NgaySuDung"].HeaderText = "Ngày dùng";
                dgvDV.Columns["GioSuDung"].HeaderText = "Giờ dùng";
                dgvDV.Columns["DonGia"].HeaderText = "Đơn giá";
                dgvDV.Columns["SoLuong"].HeaderText = "Số lượng";
                dgvDV.Columns["ThanhTien"].HeaderText = "Thành tiền";

                dgvDV.Columns["DonGia"].DefaultCellStyle.Format = "N0";
                dgvDV.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";

                dgvDV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDV.Columns["TenDV"].FillWeight = 140;
                dgvDV.Columns["NgaySuDung"].FillWeight = 85;
                dgvDV.Columns["GioSuDung"].FillWeight = 70;
                dgvDV.Columns["DonGia"].FillWeight = 85;
                dgvDV.Columns["SoLuong"].FillWeight = 55;
                dgvDV.Columns["ThanhTien"].FillWeight = 95;
                dgvDV.Columns["btnHuyDV"].FillWeight = 60;
            }

            FormatGridView(dgvDV);
        }

        //================ 4.1 XỬ LÝ SỰ KIỆN HỦY DỊCH VỤ ==================
        private void dgvDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvDV.Columns["btnHuyDV"].Index)
                return;

            var row = dgvDV.Rows[e.RowIndex];
            int maCTDV = Convert.ToInt32(row.Cells["MaCTDV"].Value);
            string tenDV = row.Cells["TenDV"].Value?.ToString();
            DateTime? thoiGianSuDung = row.Cells["ThoiGianSuDungGoc"].Value as DateTime?;

            if (thoiGianSuDung.HasValue && thoiGianSuDung.Value <= DateTime.Now)
            {
                MessageBox.Show($"Dịch vụ \"{tenDV}\" đã qua ngày/giờ sử dụng ({thoiGianSuDung.Value:HH:mm dd/MM/yyyy}).\n" +
                                $"Hệ thống không cho phép hủy dịch vụ này!",
                                "Không thể hủy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn hủy dịch vụ \"{tenDV}\" không?",
                                                  "Xác nhận hủy dịch vụ",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var ctdv = db.ChiTietDichVus.FirstOrDefault(x => x.MaCTDV == maCTDV);
                    if (ctdv != null)
                    {
                        db.ChiTietDichVus.Remove(ctdv);
                        db.SaveChanges();

                        MessageBox.Show("Đã hủy dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadDichVu();
                        TinhTongTien();
                    }
                    else
                    {
                        MessageBox.Show("Dịch vụ không tồn tại hoặc đã bị xóa trước đó!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa dịch vụ: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //================ 5. HÀM CHUẨN HÓA GRIDVIEW ==================
        private void FormatGridView(DataGridView dgv)
        {
            dgv.RowHeadersVisible = false;
            dgv.EnableHeadersVisualStyles = false;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 55);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gold;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 45;

            dgv.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
            dgv.GridColor = Color.Gray;

            dgv.RowsDefaultCellStyle.BackColor = Color.White;
            dgv.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
        }

        //================ 6. TÍNH TỔNG & XỬ LÝ TRẠNG THÁI NÚT ==================
        private void TinhTongTien()
        {
            lblTienPhong.Text = tongTienPhong.ToString("N0") + " đ";
            lblTienDV.Text = tongTienDV.ToString("N0") + " đ";

            decimal tongTienHienTai = tongTienPhong + tongTienDV;
            lblTong.Text = tongTienHienTai.ToString("N0") + " đ";

            decimal giaTriDatPhong = tongTienHienTai;
            decimal tienDaThu = tienDaTraTrongDB;

            decimal phiPhatSinh = 0;
            if (tienDaThu > 0 && tongTienHienTai > tienDaThu)
            {
                phiPhatSinh = tongTienHienTai - tienDaThu;
            }

            lblGTDatPhong.Text = giaTriDatPhong.ToString("N0") + " đ";
            lblTienDaThu.Text = tienDaThu.ToString("N0") + " đ";
            lblPhiPhatSinh.Text = phiPhatSinh.ToString("N0") + " đ";

            decimal tienCanThanhToan = tongTienHienTai - tienDaThu;

            Color backColorChuan = Color.FromArgb(4, 17, 33);
            Color foreColorChuan = Color.FromArgb(246, 224, 175);

            bool isDaHuy = _datPhongCache != null &&
                           !string.IsNullOrEmpty(_datPhongCache.TrangThai) &&
                           _datPhongCache.TrangThai.Equals("Đã hủy", StringComparison.OrdinalIgnoreCase);

            if (isDaHuy)
            {
                btnThanhToan.Enabled = false;
                btnThanhToan.Text = "Đơn đã hủy";
                btnThanhToan.BackColor = backColorChuan;
                btnThanhToan.ForeColor = Color.Gray;
            }
            else if (tienCanThanhToan > 0)
            {
                btnThanhToan.Enabled = true;

                if (phiPhatSinh > 0)
                {
                    btnThanhToan.Text = $"Thanh toán phát sinh ({phiPhatSinh:N0} đ)";
                }
                else
                {
                    btnThanhToan.Text = $"Thanh toán ({tienCanThanhToan:N0} đ)";
                }

                btnThanhToan.BackColor = backColorChuan;
                btnThanhToan.ForeColor = foreColorChuan;
            }
            else
            {
                btnThanhToan.Enabled = false;
                btnThanhToan.Text = "Đã thanh toán";
                btnThanhToan.BackColor = backColorChuan;
                btnThanhToan.ForeColor = Color.Gray;
            }
        }

        //================ 7. THANH TOÁN SỐ TIỀN PHÁT SINH ==================
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (maDatPhong <= 0) return;

            try
            {
                var dp = db.DatPhongs.FirstOrDefault(x => x.MaDatPhong == maDatPhong);
                if (dp == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin đặt phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (dp.TrangThai == "Đã đặt" || dp.TrangThai == "Chờ thanh toán")
                {
                    dp.TrangThai = "Đã thanh toán";
                }

                decimal tongTienHienTai = tongTienPhong + tongTienDV;
                DateTime thoiDiemThanhToan = DateTime.Now;

                var hd = db.HoaDons.FirstOrDefault(x => x.MaDatPhong == maDatPhong);
                if (hd != null)
                {
                    hd.NgayLap = thoiDiemThanhToan;
                    hd.PhuongThucTT = "Chuyển khoản";
                    hd.TongTien = tongTienHienTai;

                    db.Entry(hd).Property(x => x.NgayLap).IsModified = true;
                    db.Entry(hd).Property(x => x.TongTien).IsModified = true;
                }
                else
                {
                    HoaDon hdMoi = new HoaDon
                    {
                        MaDatPhong = maDatPhong,
                        NgayLap = thoiDiemThanhToan,
                        TongTien = tongTienHienTai,
                        PhuongThucTT = "Chuyển khoản"
                    };
                    db.HoaDons.Add(hdMoi);
                }

                db.SaveChanges();

                MessageBox.Show($"Thanh toán thành công số tiền phát sinh vào lúc {thoiDiemThanhToan:HH:mm dd/MM/yyyy}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadHoaDonDaLuu();
                TinhTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu thông tin thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CanLePhaiChoLabel()
        {
            Label[] dsLabel = new Label[] { lblGTDatPhong, lblTienDaThu, lblPhiPhatSinh };

            foreach (var lbl in dsLabel)
            {
                lbl.AutoSize = false;
                lbl.TextAlign = ContentAlignment.MiddleRight;
                lbl.Size = new Size(180, 25);
            }
        }
    }
}