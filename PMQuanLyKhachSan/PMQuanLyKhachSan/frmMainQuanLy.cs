using Microsoft.EntityFrameworkCore;
using PhanMemQuanLyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMQuanLyKhachSan
{
    public partial class frmMainQuanLy : Form
    {
        public DataContext db = new DataContext();
        public NhanVien nhanVienDangNhap;

        public frmMainQuanLy(NhanVien nv)
        {
            InitializeComponent();
            nhanVienDangNhap = nv;
        }

        private void BoGocButton(Button btn, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            path.StartFigure();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, btn.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            btn.Region = new Region(path);
        }

        private void frmMainQuanLy_Load(object sender, EventArgs e)
        {
            try
            {
                if (nhanVienDangNhap != null)
                {
                    btnQuanLy.Text = nhanVienDangNhap.HoTen ?? "Quản lý";

                    // Nạp lại NhanVien kèm theo ChucVu từ DB để tránh bị null ChucVu
                    var nvFull = db.NhanViens
                                  .Include(x => x.ChucVu)
                                  .FirstOrDefault(x => x.MaNV == nhanVienDangNhap.MaNV);

                    if (nvFull != null)
                    {
                        nhanVienDangNhap = nvFull;
                        lblChucVu.Text = nvFull.ChucVu != null ? nvFull.ChucVu.TenChucVu : "Quản lý";
                    }
                    else
                    {
                        lblChucVu.Text = "Quản lý";
                    }

                    BoGocButton(btnQuanLy, 30);

                    // Hiển thị Trang chủ mặc định
                    OpenChildForm(new frmTrangChu(nhanVienDangNhap));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải giao diện chính: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnQuanLy_Click(object sender, EventArgs e)
        {
            // Truyền nhanVienDangNhap sang Form thông tin cá nhân
            frmTTNhanVien frm = new frmTTNhanVien(nhanVienDangNhap);
            frm.Show();
            this.Close(); // Đóng Form main để giải phóng tài nguyên
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {

        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmTrangChu(nhanVienDangNhap));
        }

        private void btnTaoTK_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmTaoTaiKhoanNV(nhanVienDangNhap));
        }

        private void lblChucVu_Click(object sender, EventArgs e)
        {

        }

        private void OpenChildForm(Form childForm)
        {
            pnlContent.Controls.Clear();

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;

            childForm.Show();
        }

        private void btnQLPhong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQLPhong());
        }



        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQLDatPhong(nhanVienDangNhap));
        }

        private void btnDatDV_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmDatDV(nhanVienDangNhap));
        }



        private void btnThoat_Click_3(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void btnDV_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQLDichVu(nhanVienDangNhap));
        }

        private void btnQLKhach_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQLKhachHang(nhanVienDangNhap));
        }

        private void btnQLNV_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQLNhanVien(nhanVienDangNhap));
        }



        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmDatPhong(nhanVienDangNhap));
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmXemLichSu(nhanVienDangNhap));
        }

        private void btnThongKe_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new frmThongKe(nhanVienDangNhap));
        }

        private void btnCaLamViec_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQLLichPhanCong(nhanVienDangNhap));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frm_DatPhong(object sender, EventArgs e)
        {

        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            frmDangNhap fDangNhap = new frmDangNhap();
            fDangNhap.Show();
            this.Close();
        }

        private void btnQuanLy_Click_1(object sender, EventArgs e)
        {
            //OpenChildForm(new frmTTNhanVien(nhanVienDangNhap));

            frmTTNhanVien frm = new frmTTNhanVien(nhanVienDangNhap);
            frm.Show();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}