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
    public partial class frmTaoTaiKhoanNV : Form
    {
        public DataContext db = new DataContext();
        private NhanVien nhanVienDangNhap;

        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
        private static readonly Regex SdtRegex = new Regex(@"^0\d{9}$", RegexOptions.Compiled);

        #region Win32 API hỗ trợ Placeholder cho TextBox

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private const int EM_SETCUEBANNER = 0x1501;

        /// <summary>
        /// Gán chữ hiển thị gợi ý (Placeholder) cho TextBox
        /// </summary>
        private void SetPlaceholder(TextBox textBox, string placeholderText)
        {
            if (textBox != null && !textBox.IsDisposed)
            {
                // wParam = 1: Giữ lại placeholder ngay cả khi TextBox nhận Focus
                SendMessage(textBox.Handle, EM_SETCUEBANNER, (IntPtr)1, placeholderText);
            }
        }

        #endregion

        public frmTaoTaiKhoanNV(NhanVien nv)
        {
            InitializeComponent();
            nhanVienDangNhap = nv;
        }

        private void frmTaoTaiKhoanNV_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

            SetupPlaceholders();

            try
            {
                var dsChucVu = db.ChucVus
                    .AsNoTracking()
                    .Where(x => x.TenChucVu.ToLower().Contains("quản lý") ||
                                x.TenChucVu.ToLower().Contains("lễ tân") ||
                                x.TenChucVu.ToLower().Contains("quan ly") ||
                                x.TenChucVu.ToLower().Contains("le tan"))
                    .ToList();

                if (dsChucVu.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu chức vụ Quản lý hoặc Lễ tân trong hệ thống!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                cbChucVu.DataSource = dsChucVu;
                cbChucVu.DisplayMember = "TenChucVu";
                cbChucVu.ValueMember = "MaChucVu";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách chức vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            SetPlaceholder(txtEmail, "Nhập địa chỉ email (VD: nv@gmail.com)...");
            SetPlaceholder(txtSDT, "Nhập số điện thoại (10 chữ số)...");
            SetPlaceholder(txtTenDangNhap, "Nhập tên đăng nhập hệ thống...");
            SetPlaceholder(txtMatKhau, "Tối thiểu 6 ký tự...");
            SetPlaceholder(txtXacNhanMK, "Nhập lại mật khẩu vừa đặt...");
        }

        private void btnTaoTK_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string email = txtEmail.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string xacNhanMK = txtXacNhanMK.Text.Trim();

            if (string.IsNullOrWhiteSpace(hoTen) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(sdt) ||
                string.IsNullOrWhiteSpace(tenDangNhap) ||
                string.IsNullOrWhiteSpace(matKhau) ||
                string.IsNullOrWhiteSpace(xacNhanMK))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ toàn bộ thông tin!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!radNam.Checked && !radNu.Checked)
            {
                MessageBox.Show("Vui lòng chọn giới tính!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbChucVu.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn chức vụ hợp lệ!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!EmailRegex.IsMatch(email))
            {
                MessageBox.Show("Định dạng Email không hợp lệ (Ví dụ: nv@gmail.com)!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (!SdtRegex.IsMatch(sdt))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! (Phải bắt đầu bằng số 0 và đúng 10 chữ số)",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            if (matKhau.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có độ dài tối thiểu 6 ký tự!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }

            if (matKhau != xacNhanMK)
            {
                MessageBox.Show("Mật khẩu xác nhận không trùng khớp!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhanMK.Focus();
                return;
            }

            DateTime ngaySinh = dtpNgaySinh.Value;
            int tuoi = DateTime.Now.Year - ngaySinh.Year;
            if (ngaySinh.Date > DateTime.Now.AddYears(-tuoi)) tuoi--;

            if (tuoi < 18)
            {
                MessageBox.Show($"Nhân viên chưa đủ 18 tuổi (Tuổi hiện tại: {tuoi})!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaySinh.Focus();
                return;
            }

            if (db.TaiKhoans.Any(x => x.TenDangNhap.ToLower() == tenDangNhap.ToLower()))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại trong hệ thống!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return;
            }

            var trungThongTin = db.NhanViens
                .Where(x => x.SDT == sdt || x.Email.ToLower() == email.ToLower())
                .Select(x => new { x.SDT, x.Email })
                .ToList();

            if (trungThongTin.Any(x => x.SDT == sdt))
            {
                MessageBox.Show("Số điện thoại này đã được đăng ký cho nhân viên khác!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            if (trungThongTin.Any(x => x.Email.ToLower() == email.ToLower()))
            {
                MessageBox.Show("Email này đã được đăng ký cho nhân viên khác!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    NhanVien nv = new NhanVien()
                    {
                        HoTen = hoTen,
                        NgaySinh = ngaySinh,
                        GioiTinh = radNam.Checked,
                        SDT = sdt,
                        Email = email,
                        MaChucVu = Convert.ToInt32(cbChucVu.SelectedValue)
                    };

                    db.NhanViens.Add(nv);
                    db.SaveChanges(); 

                    TaiKhoan tk = new TaiKhoan()
                    {
                        TenDangNhap = tenDangNhap,
                        MatKhau = matKhau,
                        MaNV = nv.MaNV 
                    };

                    db.TaiKhoans.Add(tk);
                    db.SaveChanges();

                    transaction.Commit();

                    MessageBox.Show("Tạo tài khoản nhân viên mới thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    XoaForm();
                }
                catch (DbUpdateException dbEx)
                {
                    transaction.Rollback();
                    MessageBox.Show("Lỗi cơ sở dữ liệu khi lưu thông tin: " + (dbEx.InnerException?.Message ?? dbEx.Message),
                        "Lỗi DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Lỗi phát sinh ngoài dự kiến: " + ex.Message,
                        "Lỗi System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            XoaForm();
        }

        private void XoaForm()
        {
            txtHoTen.Clear();
            txtEmail.Clear();
            txtSDT.Clear();
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            txtXacNhanMK.Clear();

            radNam.Checked = false;
            radNu.Checked = false;

            if (cbChucVu.Items.Count > 0)
                cbChucVu.SelectedIndex = 0;

            dtpNgaySinh.Value = DateTime.Now;
            txtHoTen.Focus();
        }


    }
}