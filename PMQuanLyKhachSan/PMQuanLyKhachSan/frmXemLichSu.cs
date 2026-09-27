using Microsoft.EntityFrameworkCore;
using PhanMemQuanLyKhachSan.Model;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices; 
using System.Windows.Forms;

namespace PMQuanLyKhachSan
{
    public partial class frmXemLichSu : Form
    {
        public DataContext db = new DataContext();
        private NhanVien nhanVienDangNhap;
        private int maKH = -1;

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

        public frmXemLichSu(NhanVien nv, int maKH)
        {
            InitializeComponent();
            this.nhanVienDangNhap = nv;
            this.maKH = maKH;
        }

        public frmXemLichSu(NhanVien nv)
        {
            InitializeComponent();
            this.nhanVienDangNhap = nv;
            this.maKH = -1;
        }

        private void frmXemLichSu_Load_1(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

            SetupPlaceholders();
            KhoiTaoForm();
        }

        private void SetupPlaceholders()
        {
            SetPlaceholder(txtMaDP, "Nhập mã đặt phòng...");
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            KhoiTaoForm();
        }

        private void KhoiTaoForm()
        {
            cbTrangThai.Items.Clear();
            cbTrangThai.Items.AddRange(new string[] {
                "Tất cả",
                "Đã đặt",
                "Đang thuê",
                "Đã trả phòng",
                "Đã hủy",
                "Đã thanh toán"
            });

            if (cbTrangThai.Items.Count > 0)
            {
                cbTrangThai.SelectedIndex = 0;
            }

            dtpThoiGian.Value = DateTime.Today;
            dtpThoiGian.Checked = false;
            LoadDGV();
        }

        private void LoadDGV()
        {
            db = new DataContext();

            FormatGridStyles();

            var query = db.DatPhongs.AsQueryable();

            if (this.maKH > 0)
            {
                query = query.Where(x => x.MaKH == this.maKH);
            }

            HienThiDanhSach(query);
        }

        private void btnTim_Click_1(object sender, EventArgs e)
        {
            var query = db.DatPhongs.AsQueryable();

            if (this.maKH > 0)
            {
                query = query.Where(x => x.MaKH == this.maKH);
            }

            if (dtpThoiGian.Checked)
            {
                DateTime ngay = dtpThoiGian.Value.Date;
                query = query.Where(x => x.NgayDat.Date == ngay);
            }

            if (cbTrangThai.SelectedIndex > 0)
            {
                string tt = cbTrangThai.Text.Trim();
                query = query.Where(x => x.TrangThai == tt);
            }

            if (!string.IsNullOrWhiteSpace(txtMaDP.Text))
            {
                if (int.TryParse(txtMaDP.Text.Trim(), out int maDP))
                {
                    query = query.Where(x => x.MaDatPhong == maDP);
                }
                else
                {
                    MessageBox.Show("Mã đặt phòng phải là số nguyên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            FormatGridStyles();
            HienThiDanhSach(query);
        }
        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            txtMaDP.Clear();

            if (cbTrangThai.Items.Count == 0)
            {
                cbTrangThai.Items.AddRange(new string[] {
                    "Tất cả",
                    "Đã đặt",
                    "Đang thuê",
                    "Đã trả phòng",
                    "Đã hủy",
                    "Đã thanh toán"
                });
            }

            if (cbTrangThai.Items.Count > 0)
            {
                cbTrangThai.SelectedIndex = 0;
            }

            dtpThoiGian.Value = DateTime.Today;
            dtpThoiGian.Checked = false;

            LoadDGV();
        }
        private void HienThiDanhSach(IQueryable<DatPhong> query)
        {
            var listDatPhong = query
                .Include(x => x.Phong)
                .ThenInclude(p => p.LoaiPhong)
                .OrderByDescending(x => x.NgayDat)
                .ToList();

            var listMaDP = listDatPhong.Select(x => x.MaDatPhong).ToList();

            var dsChiTietDV = db.ChiTietDichVus
                .Include(x => x.DichVu)
                .Where(ct => listMaDP.Contains(ct.MaDatPhong))
                .Select(ct => new
                {
                    ct.MaDatPhong,
                    TenDV = ct.DichVu != null ? ct.DichVu.TenDV : "",
                    ThanhTien = ((decimal?)ct.ThanhTien) ?? 0m
                })
                .ToList();

            var dsHoaDon = db.HoaDons
                .Where(hd => listMaDP.Contains(hd.MaDatPhong))
                .ToList();

            var dsHienThi = listDatPhong.Select(x =>
            {
                DateTime ngayNhan = x.NgayNhan != default ? x.NgayNhan : DateTime.Today;
                DateTime ngayTra = x.NgayTra != default ? x.NgayTra : DateTime.Today;

                int soDem = (ngayTra - ngayNhan).Days;
                if (soDem <= 0) soDem = 1;

                decimal giaPhong = (x.Phong != null && x.Phong.LoaiPhong != null) ? x.Phong.LoaiPhong.Gia : 0m;
                decimal tienPhong = giaPhong * soDem;

                var listDV = dsChiTietDV.Where(ct => ct.MaDatPhong == x.MaDatPhong).ToList();
                string tenDichVu = listDV.Count > 0
                    ? string.Join(", ", listDV.Select(ct => ct.TenDV).Where(t => !string.IsNullOrEmpty(t)))
                    : "Không có";

                decimal tienDichVu = listDV.Sum(ct => ct.ThanhTien);
                decimal tongTien = tienPhong + tienDichVu;

                var hoaDon = dsHoaDon.FirstOrDefault(hd => hd.MaDatPhong == x.MaDatPhong);
                string pttt = "";

                if (hoaDon != null && !string.IsNullOrEmpty(hoaDon.PhuongThucTT))
                {
                    pttt = hoaDon.PhuongThucTT;
                }
                else if (x.TrangThai == "Đã thanh toán")
                {
                    pttt = "Chuyển khoản";
                }

                return new
                {
                    MaDatPhong = x.MaDatPhong,
                    Phong = x.Phong != null ? (x.Phong.TenPhong ?? "N/A") : "N/A",
                    LoaiPhong = (x.Phong != null && x.Phong.LoaiPhong != null) ? (x.Phong.LoaiPhong.TenLoai ?? "N/A") : "N/A",
                    NgayDat = x.NgayDat,
                    NgayNhan = ngayNhan,
                    NgayTra = ngayTra,
                    SoDem = soDem,
                    DonGia = giaPhong,
                    DichVu = tenDichVu,
                    TongTien = tongTien,
                    TrangThai = x.TrangThai ?? "Đã đặt",
                    PhuongThucTT = pttt
                };
            }).ToList();

            dgvLichSu.DataSource = dsHienThi;

            SetGridHeader();

            bool isSearching = txtMaDP.Text.Length > 0 || cbTrangThai.SelectedIndex > 0 || dtpThoiGian.Checked;
            if (dsHienThi.Count == 0 && isSearching)
            {
                MessageBox.Show("Không tìm thấy đơn đặt phòng hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FormatGridStyles()
        {
            dgvLichSu.Columns.Clear();

            dgvLichSu.RowHeadersVisible = false;

            dgvLichSu.EnableHeadersVisualStyles = false;
            dgvLichSu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 55);
            dgvLichSu.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gold;
            dgvLichSu.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvLichSu.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvLichSu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvLichSu.ColumnHeadersHeight = 35;

            dgvLichSu.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
            dgvLichSu.GridColor = Color.Gray;

            dgvLichSu.RowsDefaultCellStyle.BackColor = Color.White;
            dgvLichSu.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvLichSu.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dgvLichSu.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
        }

        private void SetGridHeader()
        {
            if (dgvLichSu.Columns["MaDatPhong"] == null) return;

            dgvLichSu.Columns["MaDatPhong"].HeaderText = "Mã đặt";
            dgvLichSu.Columns["Phong"].HeaderText = "Phòng";
            dgvLichSu.Columns["LoaiPhong"].HeaderText = "Loại phòng";
            dgvLichSu.Columns["NgayDat"].HeaderText = "Ngày đặt";
            dgvLichSu.Columns["NgayNhan"].HeaderText = "Ngày nhận";
            dgvLichSu.Columns["NgayTra"].HeaderText = "Ngày trả";
            dgvLichSu.Columns["SoDem"].HeaderText = "Số đêm";
            dgvLichSu.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvLichSu.Columns["DichVu"].HeaderText = "Dịch vụ";
            dgvLichSu.Columns["TongTien"].HeaderText = "Tổng tiền";
            dgvLichSu.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvLichSu.Columns["PhuongThucTT"].HeaderText = "Phương thức thanh toán";

            dgvLichSu.Columns["NgayDat"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvLichSu.Columns["NgayNhan"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvLichSu.Columns["NgayTra"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvLichSu.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            dgvLichSu.Columns["TongTien"].DefaultCellStyle.Format = "N0";

            if (dgvLichSu.Columns["btnXemChiTiet"] == null)
            {
                DataGridViewButtonColumn btnChiTiet = new DataGridViewButtonColumn();
                btnChiTiet.Name = "btnXemChiTiet";
                btnChiTiet.HeaderText = "Chi tiết";
                btnChiTiet.Text = "Xem chi tiết";
                btnChiTiet.UseColumnTextForButtonValue = true;
                btnChiTiet.FlatStyle = FlatStyle.Standard;

                dgvLichSu.Columns.Add(btnChiTiet);
            }

            if (dgvLichSu.Columns["btnHuyPhong"] == null)
            {
                DataGridViewButtonColumn btnHuy = new DataGridViewButtonColumn();
                btnHuy.Name = "btnHuyPhong";
                btnHuy.HeaderText = "Hủy phòng";
                btnHuy.Text = "Hủy";
                btnHuy.UseColumnTextForButtonValue = true;
                btnHuy.FlatStyle = FlatStyle.Standard;

                dgvLichSu.Columns.Add(btnHuy);
            }

            dgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dgvLichSu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var cellValue = dgvLichSu.Rows[e.RowIndex].Cells["MaDatPhong"].Value;
            if (cellValue == null || !int.TryParse(cellValue.ToString(), out int maDP)) return;

            string colName = dgvLichSu.Columns[e.ColumnIndex].Name;

            if (colName == "btnXemChiTiet")
            {
                var trangThaiVal = dgvLichSu.Rows[e.RowIndex].Cells["TrangThai"].Value?.ToString();

                frmHoaDonKhach frm = new frmHoaDonKhach(maDP);

                if (trangThaiVal == "Đã thanh toán" || trangThaiVal == "Đang thuê" || trangThaiVal == "Đang ở" || trangThaiVal == "Đã trả phòng" || trangThaiVal == "Đã hủy")
                {
                    Control[] btns = frm.Controls.Find("btnThanhToan", true);
                    if (btns.Length > 0 && btns[0] is Button btn)
                    {
                        btn.Enabled = false;
                    }
                }

                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
                LoadDGV();
            }
            else if (colName == "btnHuyPhong")
            {
                XuLyHuyPhong(maDP, e.RowIndex);
            }
        }

        private void XuLyHuyPhong(int maDP, int rowIndex)
        {
            var datPhong = db.DatPhongs
                .Include(x => x.Phong)
                .ThenInclude(p => p.LoaiPhong)
                .FirstOrDefault(x => x.MaDatPhong == maDP);

            if (datPhong == null)
            {
                MessageBox.Show("Không tìm thấy dữ liệu đặt phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (datPhong.TrangThai == "Đã hủy")
            {
                MessageBox.Show("Đơn đặt phòng này đã được hủy trước đó rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (datPhong.TrangThai == "Đã trả phòng")
            {
                MessageBox.Show("Đơn đặt phòng này đã hoàn tất dịch vụ, không thể hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime thoiGianHienTai = DateTime.Now;
            DateTime thoiGianNhanPhong = datPhong.NgayNhan;

            if (thoiGianNhanPhong.TimeOfDay == TimeSpan.Zero)
            {
                thoiGianNhanPhong = thoiGianNhanPhong.Date.AddHours(14);
            }

            TimeSpan thoiGianConLai = thoiGianNhanPhong - thoiGianHienTai;

            if (thoiGianConLai.TotalHours < 0)
            {
                MessageBox.Show($"Không thể hủy phòng!\n\nĐã quá thời gian nhận phòng quy định.\n(Thời gian nhận phòng: {thoiGianNhanPhong:dd/MM/yyyy HH:mm})",
                                "Chặn hủy phòng", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (thoiGianConLai.TotalHours < 24)
            {
                MessageBox.Show($"Không thể hủy phòng!\n\nTheo quy định, không hỗ trợ hủy phòng trong vòng 24 giờ trước giờ nhận phòng.\n" +
                                $"• Thời gian nhận phòng: {thoiGianNhanPhong:dd/MM/yyyy HH:mm}\n" +
                                $"• Thời gian còn lại: {Math.Round(thoiGianConLai.TotalHours, 1)} giờ",
                                "Chặn hủy phòng", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            int soDem = (datPhong.NgayTra - datPhong.NgayNhan).Days;
            if (soDem <= 0) soDem = 1;

            decimal giaPhong = (datPhong.Phong != null && datPhong.Phong.LoaiPhong != null) ? datPhong.Phong.LoaiPhong.Gia : 0m;
            decimal tienPhong = giaPhong * soDem;

            decimal tienDV = db.ChiTietDichVus
                .Where(ct => ct.MaDatPhong == maDP)
                .Sum(ct => ((decimal?)ct.ThanhTien) ?? 0m);

            decimal tongTienDon = tienPhong + tienDV;

            var hoaDon = db.HoaDons.FirstOrDefault(hd => hd.MaDatPhong == maDP);
            bool daThanhToan = (datPhong.TrangThai == "Đã thanh toán") || (hoaDon != null && (hoaDon.TongTien ?? 0m) > 0);
            decimal soTienHoanTra = hoaDon != null ? (hoaDon.TongTien ?? 0m) : (daThanhToan ? tongTienDon : 0m);

            string msgXacNhan = "";
            if (daThanhToan)
            {
                msgXacNhan = $"Đơn đặt phòng mã #{maDP} ĐÃ THANH TOÁN.\n\n" +
                             $"Tổng số tiền cần HOÀN TRẢ lại cho khách: {soTienHoanTra:N0} đ\n\n" +
                             $"Bạn có chắc chắn muốn tiến hành HỦY ĐƠN và HOÀN TIỀN không?";
            }
            else
            {
                msgXacNhan = $"Bạn có chắc chắn muốn hủy đơn đặt phòng [Mã: {maDP}] không?";
            }

            DialogResult dialogResult = MessageBox.Show(msgXacNhan,
                                                       "Xác nhận hủy phòng",
                                                       MessageBoxButtons.YesNo,
                                                       MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    datPhong.TrangThai = "Đã hủy";

                    if (datPhong.Phong != null)
                    {
                        datPhong.Phong.TrangThai = "Trống";
                    }

                    db.SaveChanges();

                    if (daThanhToan)
                    {
                        MessageBox.Show($"Hủy đặt phòng thành công!\n\n" +
                                        $"Vui lòng thực hiện HOÀN TRẢ số tiền {soTienHoanTra:N0} đ cho khách hàng.",
                                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Hủy đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    LoadDGV();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật trạng thái hủy: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}