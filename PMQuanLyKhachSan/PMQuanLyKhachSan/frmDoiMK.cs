using PhanMemQuanLyKhachSan.Model;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PMQuanLyKhachSan
{
    public partial class frmDoiMK : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public DataContext db = new DataContext();
        private NhanVien quanLyDangNhap;

        private TaiKhoan _taiKhoanCache;

        public frmDoiMK()
        {
            InitializeComponent();
        }

        public frmDoiMK(string tenDangNhap)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(tenDangNhap))
            {
                txtTenDangNhap.Text = tenDangNhap;
                txtTenDangNhap.ReadOnly = true; 
            }
            else
            {
                txtTenDangNhap.Clear();
                txtTenDangNhap.ReadOnly = false;
            }
        }

        public frmDoiMK(NhanVien ql)
        {
            InitializeComponent();

            quanLyDangNhap = ql;

            if (ql != null)
            {
                var tk = db.TaiKhoans.FirstOrDefault(x => x.MaNV == ql.MaNV);
                if (tk != null)
                {
                    txtTenDangNhap.Text = tk.TenDangNhap;
                    txtTenDangNhap.ReadOnly = true;

                    _taiKhoanCache = tk; 
                }
            }
            else
            {
                txtTenDangNhap.Clear();
                txtTenDangNhap.ReadOnly = false;
            }
        }

        //================ LOAD FORM ====================
        private void frmDoiMK_Load(object sender, EventArgs e)
        {
            this.FormClosed += FrmDoiMK_FormClosed;

            SendMessage(txtTenDangNhap.Handle, EM_SETCUEBANNER, (IntPtr)1, "Nhập tên đăng nhập...");
            SendMessage(txtMKMoi.Handle, EM_SETCUEBANNER, (IntPtr)1, "Nhập mật khẩu mới...");
            SendMessage(txtXacNhan.Handle, EM_SETCUEBANNER, (IntPtr)1, "Xác nhận mật khẩu mới...");
        }

        //================ ĐỔI MẬT KHẨU ====================
        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            string user = txtTenDangNhap.Text.Trim();
            string mkMoi = txtMKMoi.Text.Trim();
            string xacNhan = txtXacNhan.Text.Trim();

            if (string.IsNullOrWhiteSpace(user) ||
                string.IsNullOrWhiteSpace(mkMoi) ||
                string.IsNullOrWhiteSpace(xacNhan))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mkMoi != xacNhan)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhan.Focus();
                return;
            }

            if (mkMoi.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMKMoi.Focus();
                return;
            }

            TaiKhoan tk = (_taiKhoanCache != null &&
                           txtTenDangNhap.ReadOnly &&
                           _taiKhoanCache.TenDangNhap == user)
                            ? _taiKhoanCache
                            : db.TaiKhoans.FirstOrDefault(x => x.TenDangNhap == user);

            if (tk == null)
            {
                MessageBox.Show("Tên đăng nhập không tồn tại!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenDangNhap.Focus();
                return;
            }

            if (tk.MatKhau == mkMoi)
            {
                MessageBox.Show("Vui lòng không sử dụng mật khẩu cũ!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMKMoi.Clear();
                txtXacNhan.Clear();
                txtMKMoi.Focus();
                return;
            }

            try
            {
                tk.MatKhau = mkMoi;
                db.SaveChanges();

                MessageBox.Show("Đổi mật khẩu thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                frmDangNhap frm = new frmDangNhap();
                frm.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đổi mật khẩu thất bại!\n\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            frmDangNhap fDangNhap = new frmDangNhap();
            fDangNhap.Show();
            this.Close();
        }

        private void FrmDoiMK_FormClosed(object sender, FormClosedEventArgs e)
        {
            db?.Dispose();
        }
    }
}