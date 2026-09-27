using Microsoft.EntityFrameworkCore;
using PhanMemQuanLyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMQuanLyKhachSan
{
    public partial class frmSuaTTNV : Form
    {
        public DataContext db = new DataContext();
        private NhanVien nhanVienDangNhap;

        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
        private static readonly Regex SdtRegex = new Regex(@"^0\d{9}$", RegexOptions.Compiled);

        #region Win32 API hỗ trợ Placeholder cho TextBox

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private const int EM_SETCUEBANNER = 0x1501;

        private void SetPlaceholder(TextBox textBox, string placeholderText)
        {
            if (textBox != null && !textBox.IsDisposed)
            {
                SendMessage(textBox.Handle, EM_SETCUEBANNER, (IntPtr)1, placeholderText);
            }
        }

        #endregion

        public frmSuaTTNV()
        {
            InitializeComponent();
        }

        public frmSuaTTNV(NhanVien nv)
        {
            InitializeComponent();
            nhanVienDangNhap = nv;
        }

        private void frmSuaTTNV_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

            try
            {
                var dsChucVu = db.ChucVus.AsNoTracking().ToList();
                cbChucVu.DataSource = dsChucVu;
                cbChucVu.DisplayMember = "TenChucVu";
                cbChucVu.ValueMember = "MaChucVu";

                cbChucVu.Enabled = false;
                txtTenDangNhap.ReadOnly = true;

                SetupPlaceholders();

                LoadThongTinChiTiet();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            db.Dispose();
            base.OnFormClosed(e);
        }

        private void SetupPlaceholders()
        {
            SetPlaceholder(txtHoTen, "Nhập họ và tên đầy đủ...");
            SetPlaceholder(txtSDT, "Nhập số điện thoại (10 chữ số)...");
            SetPlaceholder(txtEmail, "Nhập địa chỉ email (VD: example@gmail.com)...");
            SetPlaceholder(txtTenDangNhap, "Tên đăng nhập...");
            SetPlaceholder(txtMatKhau, "Nhập mật khẩu hiện tại để xác thực...");
            SetPlaceholder(txtMKMoi, "Bỏ trống nếu không muốn đổi mật khẩu...");
            SetPlaceholder(txtXacNhanMK, "Nhập lại mật khẩu mới...");
        }

        private void LoadThongTinChiTiet()
        {
            if (nhanVienDangNhap == null) return;

            var nv = db.NhanViens.Find(nhanVienDangNhap.MaNV);

            if (nv != null)
            {
                nhanVienDangNhap = nv;

                txtHoTen.Text = nv.HoTen ?? "";
                dtpNgaySinh.Value = nv.NgaySinh;

                if (nv.GioiTinh == true)
                    radNam.Checked = true;
                else
                    radNu.Checked = true;

                txtSDT.Text = nv.SDT ?? "";
                txtEmail.Text = nv.Email ?? "";
                cbChucVu.SelectedValue = nv.MaChucVu;

                var tk = db.TaiKhoans.Find(nv.MaTK);
                if (tk != null)
                {
                    txtTenDangNhap.Text = tk.TenDangNhap ?? "";
                }

                txtMatKhau.Clear();
                txtMKMoi.Clear();
                txtXacNhanMK.Clear();
            }
        }

        private void btnHoanTat_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string email = txtEmail.Text.Trim();

            string mkHienTai = txtMatKhau.Text.Trim();
            string mkMoi = txtMKMoi.Text.Trim();
            string xacNhanMK = txtXacNhanMK.Text.Trim();

            if (string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(sdt))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin cá nhân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(mkHienTai))
            {
                MessageBox.Show("Vui lòng nhập Mật khẩu hiện tại để xác thực!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }

            if (!EmailRegex.IsMatch(email))
            {
                MessageBox.Show("Định dạng Email không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (!SdtRegex.IsMatch(sdt))
            {
                MessageBox.Show("Số điện thoại phải gồm 10 chữ số và bắt đầu bằng số 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            DateTime ngaySinh = dtpNgaySinh.Value;
            int tuoi = DateTime.Now.Year - ngaySinh.Year;
            if (ngaySinh.Date > DateTime.Now.AddYears(-tuoi)) tuoi--;

            if (tuoi < 18)
            {
                MessageBox.Show($"Nhân viên chưa đủ 18 tuổi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaySinh.Focus();
                return;
            }

            var trungThongTin = db.NhanViens
                .Where(x => x.MaNV != nhanVienDangNhap.MaNV && (x.SDT == sdt || x.Email.ToLower() == email.ToLower()))
                .Select(x => new { x.SDT, x.Email })
                .ToList();

            if (trungThongTin.Any(x => x.SDT == sdt))
            {
                MessageBox.Show("Số điện thoại này đã thuộc về nhân viên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            if (trungThongTin.Any(x => x.Email.ToLower() == email.ToLower()))
            {
                MessageBox.Show("Email này đã thuộc về nhân viên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            try
            {
                var nvDB = db.NhanViens.Find(nhanVienDangNhap.MaNV);
                if (nvDB == null) return;

                var tkDB = db.TaiKhoans.Find(nvDB.MaTK);
                if (tkDB == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin tài khoản tương ứng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (tkDB.MatKhau != mkHienTai)
                {
                    MessageBox.Show("Mật khẩu hiện tại không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(mkMoi))
                {
                    if (mkMoi == mkHienTai)
                    {
                        MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu hiện tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMKMoi.Focus();
                        return;
                    }

                    if (mkMoi.Length < 6)
                    {
                        MessageBox.Show("Mật khẩu mới phải có tối thiểu 6 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMKMoi.Focus();
                        return;
                    }

                    if (mkMoi != xacNhanMK)
                    {
                        MessageBox.Show("Xác nhận mật khẩu mới không trùng khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtXacNhanMK.Focus();
                        return;
                    }

                    tkDB.MatKhau = mkMoi;
                }

                nvDB.HoTen = hoTen;
                nvDB.NgaySinh = ngaySinh;
                nvDB.GioiTinh = radNam.Checked;
                nvDB.SDT = sdt;
                nvDB.Email = email;

                db.SaveChanges();

                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                QuayLaiTTNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            QuayLaiTTNhanVien();
        }

        private void QuayLaiTTNhanVien()
        {
            frmTTNhanVien frm = new frmTTNhanVien(nhanVienDangNhap);
            frm.Show();
            this.Close();
        }
    }
}