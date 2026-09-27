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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMQuanLyKhachSan
{
    public partial class frmTTNhanVien : Form
    {
        public DataContext db = new DataContext();
        private NhanVien nhanVienDangNhap;

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

        public frmTTNhanVien()
        {
            InitializeComponent();
        }
        public frmTTNhanVien(NhanVien nv)
        {
            InitializeComponent();
            nhanVienDangNhap = nv;
        }

        private void frmTTNhanVien_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

            try
            {
                var dsChucVu = db.ChucVus.ToList();
                cbChucVu.DataSource = dsChucVu;
                cbChucVu.DisplayMember = "TenChucVu";
                cbChucVu.ValueMember = "MaChucVu";

                VoHieuHoaChinhSua();
                SetupPlaceholders();
                LoadThongTinChiTiet();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin cá nhân: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetupPlaceholders()
        {
            SetPlaceholder(txtHoTen, "Chưa cập nhật họ tên...");
            SetPlaceholder(txtSDT, "Chưa cập nhật số điện thoại...");
            SetPlaceholder(txtEmail, "Chưa cập nhật email...");
            SetPlaceholder(txtTenDangNhap, "Chưa cập nhật tên đăng nhập...");
            SetPlaceholder(txtMatKhau, "Chưa cập nhật mật khẩu...");
        }

        private void VoHieuHoaChinhSua()
        {
            txtHoTen.ReadOnly = true;
            dtpNgaySinh.Enabled = false;
            radNam.Enabled = false;
            radNu.Enabled = false;

            txtSDT.ReadOnly = true;
            txtEmail.ReadOnly = true;
            cbChucVu.Enabled = false;
            txtTenDangNhap.ReadOnly = true;
            txtMatKhau.ReadOnly = true;
        }

        private void LoadThongTinChiTiet()
        {
            if (nhanVienDangNhap == null) return;
            var nv = db.NhanViens.FirstOrDefault(x => x.MaNV == nhanVienDangNhap.MaNV);

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

                var tk = db.TaiKhoans.FirstOrDefault(x => x.MaTK == nv.MaTK);
                if (tk != null)
                {
                    txtTenDangNhap.Text = tk.TenDangNhap ?? "";
                    txtMatKhau.Text = tk.MatKhau ?? "";
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            frmSuaTTNV frm = new frmSuaTTNV(nhanVienDangNhap);
            frm.Show();
            this.Close();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {

            if (nhanVienDangNhap == null)
            {
                this.Close();
                return;
            }

            var nv = db.NhanViens
                       .Include(x => x.ChucVu)
                       .FirstOrDefault(x => x.MaNV == nhanVienDangNhap.MaNV);

            if (nv == null)
            {
                MessageBox.Show("Không tìm thấy thông tin nhân viên!");
                return;
            }

            if (nv.ChucVu.TenChucVu.Trim().ToLower().Contains("quản lý") ||
                nv.ChucVu.TenChucVu.Trim().ToLower().Contains("quan ly"))
            {
                frmMainQuanLy frm = new frmMainQuanLy(nv);
                frm.Show();
            }
            else
            {
                frmLeTan frm = new frmLeTan(nv);
                frm.Show();
            }

            this.Hide();    
        }
    }
}