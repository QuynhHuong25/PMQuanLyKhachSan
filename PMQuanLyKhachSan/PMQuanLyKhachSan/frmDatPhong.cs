using PhanMemQuanLyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace PMQuanLyKhachSan
{
    public partial class frmDatPhong : Form
    {
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        private const string TRANG_THAI_DANG_THUE = "Đang có khách";
        private const string TRANG_THAI_TRONG = "Trống";
        private const string TRANG_THAI_DA_HUY = "Đã hủy";
        private const string TRANG_THAI_DA_TRA = "Đã trả phòng";
        private const string TRANG_THAI_DA_DAT = "Đã đặt";

        private const int MA_LOAI_TAT_CA = 0;

        private static readonly Regex RegexChuaChuSo = new Regex(@"\d", RegexOptions.Compiled);
        private static readonly Regex RegexTenHopLe = new Regex(
            @"^[a-zA-Zàáảãạăắằẳẵặâấầẩẫậèéẻẽẹêếềểễệđìíỉĩịòóỏõọôốồổỗộơớờởỡợùúủũụưứừửữựỳýỷỹỵ\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RegexCCCDHopLe = new Regex(@"^\d+$", RegexOptions.Compiled);
        private static readonly Regex RegexSDTHopLe = new Regex(@"^0\d{9}$", RegexOptions.Compiled);

        public DataContext db = new DataContext();

        private KhachHang khachDangDat;
        private NhanVien nhanVienDat;
        private bool laKhachHang;

        private class PhongVM
        {
            public int MaPhong { get; set; }
            public string TenPhong { get; set; }
            public string LoaiPhong { get; set; }
            public decimal Gia { get; set; }
            public string TrangThai { get; set; }
        }

        //================ KHÁCH HÀNG ===================
        public frmDatPhong(KhachHang kh)
        {
            InitializeComponent();
            khachDangDat = kh;
            laKhachHang = true;
        }

        //================ NHÂN VIÊN ====================
        public frmDatPhong(NhanVien nv)
        {
            InitializeComponent();
            nhanVienDat = nv;
            laKhachHang = false;
        }

        //================ TỰ ĐỘNG CẬP NHẬT TRẠNG THÁI PHÒNG ====================
        private void CapNhatTrangThaiPhongTuDong()
        {
            try
            {
                // Sử dụng DbContext riêng để không đè/hủy DbContext dùng chung của Form
                using (var context = new DataContext())
                {
                    DateTime homNay = DateTime.Today;

                    var dsMaPhongDangO = context.DatPhongs
                        .Where(dp => dp.TrangThai != TRANG_THAI_DA_HUY &&
                                     dp.TrangThai != TRANG_THAI_DA_TRA &&
                                     dp.NgayNhan.Date <= homNay &&
                                     homNay < dp.NgayTra.Date)
                        .Select(dp => dp.MaPhong)
                        .Distinct()
                        .ToList();

                    var dsPhong = context.Phongs.ToList();
                    bool coThayDoi = false;

                    foreach (var phong in dsPhong)
                    {
                        string trangThaiMoi = dsMaPhongDangO.Contains(phong.MaPhong) ? TRANG_THAI_DANG_THUE : TRANG_THAI_TRONG;

                        if (phong.TrangThai != trangThaiMoi)
                        {
                            phong.TrangThai = trangThaiMoi;
                            coThayDoi = true;
                        }
                    }

                    if (coThayDoi)
                    {
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                // Xử lý hoặc ghi log nếu cần
            }
        }

        //================ LOAD FORM ====================
        private void frmDatPhong_Load(object sender, EventArgs e)
        {
            this.FormClosed += frmDatPhong_FormClosed;
            frmQLDatPhong.TrangThaiPhongChanged += FrmQLDatPhong_TrangThaiPhongChanged;

            // Đăng ký sự kiện commit nhanh CheckBox trên DataGridView
            dgvPhong.CurrentCellDirtyStateChanged += dgvPhong_CurrentCellDirtyStateChanged;

            CapNhatTrangThaiPhongTuDong();

            DateTime homNay = DateTime.Today;
            dtpNgayNhan.Value = homNay;
            dtpNgayTra.Value = homNay.AddDays(1);

            SendMessage(txtTenKH.Handle, EM_SETCUEBANNER, (IntPtr)1, "Nhập họ và tên...");
            SendMessage(txtCCCD.Handle, EM_SETCUEBANNER, (IntPtr)1, "Nhập số CCCD/CMND...");
            SendMessage(txtSDT.Handle, EM_SETCUEBANNER, (IntPtr)1, "Nhập số điện thoại...");

            var dsLoaiPhong = db.LoaiPhongs.AsNoTracking().ToList();
            dsLoaiPhong.Insert(0, new LoaiPhong { MaLoai = MA_LOAI_TAT_CA, TenLoai = "None" });

            cbLoai.DataSource = dsLoaiPhong;
            cbLoai.DisplayMember = "TenLoai";
            cbLoai.ValueMember = "MaLoai";

            dtpNgayNhan.ValueChanged += dtpNgayNhan_NgayTra_ValueChanged;
            dtpNgayTra.ValueChanged += dtpNgayNhan_NgayTra_ValueChanged;
            cbLoai.SelectedIndexChanged += (s, ev) => TuDongLocTheoNgay();

            if (laKhachHang)
            {
                txtTenKH.Text = khachDangDat?.HoTen ?? "";
                txtCCCD.Text = khachDangDat?.CCCD ?? "";
                txtSDT.Text = khachDangDat?.SDT ?? "";

                txtTenKH.ReadOnly = true;
                txtCCCD.ReadOnly = true;
                txtSDT.ReadOnly = true;
            }
            else
            {
                txtTenKH.Clear();
                txtCCCD.Clear();
                txtSDT.Clear();

                txtTenKH.ReadOnly = false;
                txtCCCD.ReadOnly = false;
                txtSDT.ReadOnly = false;
            }

            TuDongLocTheoNgay();

            Color mauNenToi = Color.FromArgb(10, 25, 47);
            dgvPhong.RowHeadersDefaultCellStyle.BackColor = mauNenToi;
            dgvPhong.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 122, 204);
            dgvPhong.RowHeadersDefaultCellStyle.ForeColor = Color.White;

            int preference = 1;
            DwmSetWindowAttribute(this.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref preference, sizeof(int));
        }

        private void dgvPhong_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvPhong.IsCurrentCellDirty && dgvPhong.CurrentCell is DataGridViewCheckBoxCell)
            {
                dgvPhong.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        //================ SỰ KIỆN TỰ ĐỘNG LỌC PHÒNG THEO NGÀY VÀ LOẠI ====================
        private void dtpNgayNhan_NgayTra_ValueChanged(object sender, EventArgs e)
        {
            if (dtpNgayNhan.Value.Date >= dtpNgayTra.Value.Date)
            {
                dtpNgayTra.Value = dtpNgayNhan.Value.Date.AddDays(1);
            }

            TuDongLocTheoNgay();
        }

        private void TuDongLocTheoNgay()
        {
            DateTime ngayNhan = dtpNgayNhan.Value.Date;
            DateTime ngayTra = dtpNgayTra.Value.Date;
            DateTime homNay = DateTime.Today;

            if (ngayNhan < homNay || ngayTra < homNay || ngayTra <= ngayNhan)
            {
                return;
            }

            int maLoai = cbLoai.SelectedValue != null ? (int)cbLoai.SelectedValue : MA_LOAI_TAT_CA;

            var maPhongDangThue = db.DatPhongs
                .Where(dp =>
                    dp.TrangThai != TRANG_THAI_DA_HUY &&
                    dp.TrangThai != TRANG_THAI_DA_TRA &&
                    ngayNhan < dp.NgayTra &&
                    ngayTra > dp.NgayNhan)
                .Select(dp => dp.MaPhong);

            var query = db.Phongs.AsNoTracking();
            if (maLoai != MA_LOAI_TAT_CA)
            {
                query = query.Where(p => p.MaLoai == maLoai);
            }

            var ds = query.Select(p => new PhongVM
            {
                MaPhong = p.MaPhong,
                TenPhong = p.TenPhong,
                LoaiPhong = p.LoaiPhong.TenLoai,
                Gia = p.LoaiPhong.Gia,
                TrangThai = maPhongDangThue.Contains(p.MaPhong) ? TRANG_THAI_DANG_THUE : TRANG_THAI_TRONG
            }).ToList();

            BindGrid(ds);
        }

        //================ SỰ KIỆN XỬ LÝ KHI CÓ THÔNG BÁO BẤM NÚT TỪ QLDatPhong ====================
        private void FrmQLDatPhong_TrangThaiPhongChanged()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(FrmQLDatPhong_TrangThaiPhongChanged));
                return;
            }

            CapNhatTrangThaiPhongTuDong();
            TuDongLocTheoNgay();
        }

        //================ GẮN DỮ LIỆU VÀO GRID ====================
        private void BindGrid(List<PhongVM> ds)
        {
            dgvPhong.DataSource = null;
            dgvPhong.Columns.Clear();
            dgvPhong.DataSource = ds;

            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn
            {
                Name = "Chon",
                HeaderText = "Chọn"
            };
            dgvPhong.Columns.Insert(0, chk);
            dgvPhong.Columns["MaPhong"].HeaderText = "Mã phòng";
            dgvPhong.Columns["TenPhong"].HeaderText = "Tên phòng";
            dgvPhong.Columns["LoaiPhong"].HeaderText = "Loại phòng";

            var colGia = dgvPhong.Columns["Gia"];
            if (colGia != null)
            {
                colGia.HeaderText = "Giá";
                colGia.DefaultCellStyle.Format = "N0";
            }

            dgvPhong.Columns["TrangThai"].HeaderText = "Trạng thái";

            dgvPhong.RowHeadersVisible = false;
            dgvPhong.EnableHeadersVisualStyles = false;
            dgvPhong.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 55);
            dgvPhong.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gold;
            dgvPhong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvPhong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPhong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPhong.ColumnHeadersHeight = 35;

            dgvPhong.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
            dgvPhong.GridColor = Color.Gray;

            dgvPhong.RowsDefaultCellStyle.BackColor = Color.White;
            dgvPhong.RowsDefaultCellStyle.ForeColor = Color.Black;

            dgvPhong.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dgvPhong.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
        }

        //================ ĐẶT PHÒNG ====================
        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            if (cbLoai.SelectedValue != null && (int)cbLoai.SelectedValue == MA_LOAI_TAT_CA)
            {
                MessageBox.Show("Vui lòng chọn một loại phòng cụ thể và chọn phòng muốn đặt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbLoai.Focus();
                return;
            }

            DateTime ngayNhan = dtpNgayNhan.Value.Date;
            DateTime ngayTra = dtpNgayTra.Value.Date;
            DateTime homNay = DateTime.Today;

            if (ngayNhan < homNay)
            {
                MessageBox.Show("Ngày nhận phòng không được nhỏ hơn ngày hiện tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayNhan.Focus();
                return;
            }

            if (ngayTra < homNay)
            {
                MessageBox.Show("Ngày trả phòng không được nhỏ hơn ngày hiện tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayTra.Focus();
                return;
            }

            if (ngayTra <= ngayNhan)
            {
                MessageBox.Show("Ngày trả phòng phải lớn hơn ngày nhận phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayTra.Focus();
                return;
            }

            var phongDaChon = new List<(int MaPhong, string TenPhong)>();

            foreach (DataGridViewRow row in dgvPhong.Rows)
            {
                if (row.IsNewRow) continue;

                bool chon = row.Cells["Chon"].Value != null && Convert.ToBoolean(row.Cells["Chon"].Value);
                if (!chon) continue;

                int maPhong = Convert.ToInt32(row.Cells["MaPhong"].Value);
                string tenPhong = row.Cells["TenPhong"].Value?.ToString() ?? "";
                phongDaChon.Add((maPhong, tenPhong));
            }

            if (phongDaChon.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một phòng cụ thể trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var maPhongIds = phongDaChon.Select(p => p.MaPhong).ToList();
            var dsMaPhongTrung = db.DatPhongs
                .Where(dp =>
                    maPhongIds.Contains(dp.MaPhong) &&
                    dp.TrangThai != TRANG_THAI_DA_HUY &&
                    dp.TrangThai != TRANG_THAI_DA_TRA &&
                    ngayNhan < dp.NgayTra &&
                    ngayTra > dp.NgayNhan)
                .Select(dp => dp.MaPhong)
                .Distinct()
                .ToList();

            if (dsMaPhongTrung.Count > 0)
            {
                var dsTenPhongTrung = phongDaChon
                    .Where(p => dsMaPhongTrung.Contains(p.MaPhong))
                    .Select(p => p.TenPhong);

                string danhSachTenPhong = string.Join(", ", dsTenPhongTrung);

                MessageBox.Show($"Phòng {danhSachTenPhong} đã có người đặt trước trong khoảng thời gian này, vui lòng chọn phòng khác!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            KhachHang kh;
            if (laKhachHang)
            {
                kh = khachDangDat;
            }
            else
            {
                string tenKH = txtTenKH.Text.Trim();
                string cccd = txtCCCD.Text.Trim();
                string sdt = txtSDT.Text.Trim();

                if (string.IsNullOrEmpty(tenKH) || string.IsNullOrEmpty(cccd) || string.IsNullOrEmpty(sdt))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (RegexChuaChuSo.IsMatch(tenKH))
                {
                    MessageBox.Show("Tên khách hàng không hợp lệ! Tên không được chứa chữ số.",
                                    "Cảnh báo dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenKH.Focus();
                    return;
                }

                if (!RegexTenHopLe.IsMatch(tenKH))
                {
                    MessageBox.Show("Tên khách hàng không hợp lệ! Tên không được chứa ký tự đặc biệt.",
                                    "Cảnh báo dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenKH.Focus();
                    return;
                }

                if (!RegexCCCDHopLe.IsMatch(cccd))
                {
                    MessageBox.Show("Mã CCCD/CMND không hợp lệ! Chỉ được nhập số.",
                                    "Cảnh báo dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCCCD.Focus();
                    return;
                }

                if (!RegexSDTHopLe.IsMatch(sdt))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ! SĐT phải chứa đúng 10 chữ số và bắt đầu bằng số 0.",
                                    "Cảnh báo dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSDT.Focus();
                    return;
                }

                kh = db.KhachHangs.FirstOrDefault(x => x.CCCD == cccd);

                if (kh != null)
                {
                    kh.HoTen = tenKH;
                    kh.SDT = sdt;
                }
                else
                {
                    kh = new KhachHang()
                    {
                        HoTen = tenKH,
                        CCCD = cccd,
                        SDT = sdt,
                        NgaySinh = new DateTime(2000, 1, 1),
                        GioiTinh = true,
                        TenDangNhap = null,
                        MatKhau = null
                    };

                    db.KhachHangs.Add(kh);
                }

                db.SaveChanges();
            }

            var danhSachDatPhongMoi = new List<DatPhong>();

            foreach (var (maPhong, _) in phongDaChon)
            {
                var datPhongMoi = new DatPhong
                {
                    MaKH = kh.MaKH,
                    MaPhong = maPhong,
                    NgayDat = DateTime.Now,
                    NgayNhan = ngayNhan,
                    NgayTra = ngayTra,
                    TrangThai = TRANG_THAI_DA_DAT
                };

                db.DatPhongs.Add(datPhongMoi);
                danhSachDatPhongMoi.Add(datPhongMoi);
            }

            try
            {
                db.SaveChanges();

                string tenKhachHang = kh.HoTen;

                string danhSachMaDatPhong = string.Join(
                    ", ",
                    danhSachDatPhongMoi.Select(dp => dp.MaDatPhong)
                );

                MessageBox.Show(
                    "Đặt phòng thành công!\n\n" +
                    "👤 Tên khách hàng: " + tenKhachHang + "\n" +
                    "🆔 Mã đặt phòng: " + danhSachMaDatPhong + "\n\n" +
                    "📌 Thông tin lưu ý dành cho Quý khách:\n" +
                    "• Thời gian nhận phòng (Check-in): Từ 7:00\n" +
                    "• Thời gian trả phòng (Check-out): Trước 12:00\n\n" +
                    "Cảm ơn Quý khách đã lựa chọn dịch vụ của chúng tôi!",
                    "Xác Nhận Đặt Phòng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(LayThongDiepLoi(ex), "Lỗi lưu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CapNhatTrangThaiPhongTuDong();
            TuDongLocTheoNgay();
        }

        private static string LayThongDiepLoi(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;

            return ex.Message;
        }

        private void dgvPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void frmDatPhong_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmQLDatPhong.TrangThaiPhongChanged -= FrmQLDatPhong_TrangThaiPhongChanged;
            db?.Dispose();
        }

        //================ PHƯƠNG THỨC MỞ FORM CON HELPER ====================
        private void OpenChildForm(Form childForm)
        {
            // Kiểm tra nếu Form hiện tại nằm trong một Panel chứa (Main Form)
            if (this.Parent != null)
            {
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;

                Control parentControl = this.Parent;
                parentControl.Controls.Clear();
                parentControl.Controls.Add(childForm);
                parentControl.Tag = childForm;
                childForm.Show();
            }
            else
            {
                childForm.ShowDialog();
            }
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            if (laKhachHang)
            {
                MessageBox.Show("Chức năng tra cứu dành riêng cho nhân viên quản lý!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OpenChildForm(new frmXemLichSu(nhanVienDat));
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (laKhachHang)
            {
                MessageBox.Show("Chức năng quản lý đặt phòng dành riêng cho nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OpenChildForm(new frmQLDatPhong(nhanVienDat));
        }
    }
}