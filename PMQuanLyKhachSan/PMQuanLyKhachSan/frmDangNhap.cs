using PhanMemQuanLyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace PMQuanLyKhachSan
{
    public partial class frmDangNhap : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public DataContext db = new DataContext();
        public NhanVien nhanVienDangNhap;

        public frmDangNhap()
        {
            InitializeComponent();
            this.FormClosed += frmDangNhapNV_FormClosed;

            _ = WarmUpDatabaseAsync();
        }

        private async Task WarmUpDatabaseAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    using (var warmupDb = new DataContext())
                    {
                        warmupDb.Database.CanConnect();
                    }
                });
            }
            catch
            {
            }
        }

        //================ ĐĂNG NHẬP ====================
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtTenDangNhap.Text.Trim();
            string password = txtMatKhau.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Lấy tất cả tài khoản khớp tên đăng nhập (SQL mặc định không phân biệt hoa/thường)
            var taiKhoans = db.TaiKhoans
                .AsNoTracking()
                .Where(tk => tk.TenDangNhap == username)
                .ToList();

            // Kiểm tra phân biệt chữ hoa chữ thường chính xác trong C# (Ordinal)
            var taiKhoan = taiKhoans.FirstOrDefault(tk => string.Equals(tk.TenDangNhap, username, StringComparison.Ordinal));

            if (taiKhoan == null)
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!",
                    "Lỗi đăng nhập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtTenDangNhap.Focus();
                return;
            }

            if (taiKhoan.MatKhau != password)
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!",
                    "Lỗi đăng nhập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtMatKhau.Focus();
                return;
            }

            var nv = db.NhanViens
                .AsNoTracking()
                .Include(x => x.ChucVu)
                .FirstOrDefault(x => x.MaNV == taiKhoan.MaNV);

            if (nv == null || nv.ChucVu == null)
            {
                MessageBox.Show("Tài khoản chưa được gán thông tin nhân viên hoặc chức vụ!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            nhanVienDangNhap = nv;
            string tenChucVu = nv.ChucVu.TenChucVu;

            if (tenChucVu.Equals("Quản lý", StringComparison.OrdinalIgnoreCase))
            {
                frmMainQuanLy frm = new frmMainQuanLy(nv);
                frm.Show();
                this.Hide();
            }
            else if (tenChucVu.Equals("Lễ tân", StringComparison.OrdinalIgnoreCase))
            {
                frmLeTan frm = new frmLeTan(nv);
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tài khoản của bạn không có quyền truy cập hệ thống!",
                    "Cảnh báo truy cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        //================ XỬ LÝ QUÊN / ĐỔI MẬT KHẨU ====================
        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            string username = txtTenDangNhap.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Vui lòng nhập Tên đăng nhập trước khi thực hiện đổi / quên mật khẩu!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return;
            }

            var taiKhoans = db.TaiKhoans
                .AsNoTracking()
                .Where(tk => tk.TenDangNhap == username)
                .ToList();

            var taiKhoan = taiKhoans.FirstOrDefault(tk => string.Equals(tk.TenDangNhap, username, StringComparison.Ordinal));

            if (taiKhoan == null)
            {
                MessageBox.Show("Tên đăng nhập không tồn tại trong hệ thống!",
                    "Lỗi xác thực",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtTenDangNhap.Focus();
                return;
            }

            var nv = db.NhanViens.FirstOrDefault(x => x.MaTK == taiKhoan.MaTK);

            frmDoiMK frm = new frmDoiMK(nv);
            frm.Show();
            this.Hide();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmDangNhapNV_Load(object sender, EventArgs e)
        {
            SendMessage(txtTenDangNhap.Handle, EM_SETCUEBANNER, (IntPtr)1, "Nhập tên đăng nhập...");
            SendMessage(txtMatKhau.Handle, EM_SETCUEBANNER, (IntPtr)1, "Nhập mật khẩu...");
        }

        private void frmDangNhapNV_FormClosed(object sender, FormClosedEventArgs e)
        {
            db?.Dispose();
        }
    }
}