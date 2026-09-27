using System;
using System.Windows.Forms;

namespace PMQuanLyKhachSan
{
    public partial class frmWelcome : Form
    {
        // Chỉ định rõ ràng System.Windows.Forms.Timer để tránh xung đột
        private System.Windows.Forms.Timer timerSplash;

        public frmWelcome()
        {
            InitializeComponent();
        }

        private void frmWelcome_Load(object sender, EventArgs e)
        {
            // Khởi tạo Timer bằng code
            timerSplash = new System.Windows.Forms.Timer();
            timerSplash.Interval = 1000; // 1 giây
            timerSplash.Tick += timerSplash_Tick;
            timerSplash.Start();
        }

        private void timerSplash_Tick(object sender, EventArgs e)
        {
            // Dừng timer
            timerSplash.Stop();

            // Ẩn màn hình Chào mừng (frmWelcome)
            this.Hide();

            // Mở màn hình Đăng nhập Nhân viên
            frmDangNhap formDangNhap = new frmDangNhap();

            // Đóng hẳn ứng dụng khi tắt form đăng nhập
            formDangNhap.FormClosed += (s, args) => this.Close();

            formDangNhap.Show();
        }
    }
}