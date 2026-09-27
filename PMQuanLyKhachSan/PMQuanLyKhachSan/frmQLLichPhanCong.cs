using Microsoft.EntityFrameworkCore;
using PhanMemQuanLyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PMQuanLyKhachSan
{
    public partial class frmQLLichPhanCong : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        private void SetPlaceholder(TextBox textBox, string placeholderText)
        {
            SendMessage(textBox.Handle, EM_SETCUEBANNER, (IntPtr)1, placeholderText);
        }

        private readonly DataContext db = new DataContext();
        private NhanVien nhanVienHienTai;
        private int maLichDangChon = -1;

        public frmQLLichPhanCong()
        {
            InitializeComponent();
        }

        public frmQLLichPhanCong(NhanVien nv) : this()
        {
            this.nhanVienHienTai = nv;
        }

        public frmQLLichPhanCong(object obj) : this()
        {
            if (obj is NhanVien nv)
            {
                this.nhanVienHienTai = nv;
            }
        }

        private void frmQLLichPhanCong_Load(object sender, EventArgs e)
        {
            KhoiTaoDuLieuControl();

            dgvLichPhanCong.CellClick -= dgvLichPhanCong_CellClick;
            dgvLichPhanCong.CellClick += dgvLichPhanCong_CellClick;

            CapNhatThongKe();
            LoadDGVAll();
        }

        private void KhoiTaoDuLieuControl()
        {
            txtNV.KeyDown -= txtNV_KeyDown;
            txtNV.KeyDown += txtNV_KeyDown;

            SetPlaceholder(txtNV, "Nhập Mã NV hoặc Họ tên...");
            SetPlaceholder(txtGhiChu, "Nhập ghi chú phân công (nếu có)...");

            txtGhiChu.Multiline = true;
            txtGhiChu.ScrollBars = ScrollBars.None;
            txtGhiChu.Size = new Size(txtGhiChu.Width, 135);

            cbCaLam.Items.Clear();
            cbCaLam.Items.AddRange(new string[] {
                "Ca sáng (6:00 - 14:00)",
                "Ca chiều (14:00 - 22:00)",
                "Ca đêm (22:00 - 6:00)"
            });
            cbCaLam.SelectedIndex = 0;

            cbTimCaLam.Items.Clear();
            cbTimCaLam.Items.AddRange(new string[] {
                "Tất cả",
                "Ca sáng",
                "Ca chiều",
                "Ca đêm"
            });
            cbTimCaLam.SelectedIndex = 0;

            dtpTimNgayLam.ShowCheckBox = false;
            dtpNgayLam.Value = DateTime.Today;
            dtpTimNgayLam.Value = DateTime.Today;

            if (label19 != null)
                label19.Text = "DANH SÁCH LỊCH PHÂN CÔNG";
        }

        private void txtNV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string tuKhoa = txtNV.Text.Trim().ToLower();
                if (string.IsNullOrEmpty(tuKhoa))
                {
                    LoadDGVAll();
                    return;
                }

                try
                {
                    var query = db.LichPhanCongs.AsQueryable();

                    int maNV = ParseMaNV(tuKhoa);
                    if (maNV > 0)
                    {
                        query = query.Where(x => x.MaNV == maNV);
                    }
                    else
                    {
                        query = query.Where(x => x.NhanVien != null && x.NhanVien.HoTen.ToLower().Contains(tuKhoa));
                    }

                    HienThiDanhSach(query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                var query = db.LichPhanCongs.AsQueryable();

                string tuKhoa = txtNV.Text.Trim().ToLower();
                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    int maNV = ParseMaNV(tuKhoa);
                    if (maNV > 0)
                    {
                        query = query.Where(x => x.MaNV == maNV);
                    }
                    else
                    {
                        query = query.Where(x => x.NhanVien != null && x.NhanVien.HoTen.ToLower().Contains(tuKhoa));
                    }
                }

                DateTime ngayTim = dtpTimNgayLam.Value.Date;
                query = query.Where(x => x.NgayLam.Date == ngayTim);

                if (cbTimCaLam.SelectedIndex > 0)
                {
                    string caChon = cbTimCaLam.SelectedItem.ToString().Split('(')[0].Trim().ToLower();
                    query = query.Where(x => x.CaLam != null && x.CaLam.ToLower().Contains(caChon));
                }

                HienThiDanhSach(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= CẬP NHẬT THỐNG KÊ ==================
        private void CapNhatThongKe()
        {
            try
            {
                DateTime homNay = DateTime.Today;

                int tongNV = db.NhanViens.AsNoTracking().Count();
                lblTongNV.Text = tongNV.ToString();

                var thongKeCa = db.LichPhanCongs
                    .AsNoTracking()
                    .Where(x => x.NgayLam.Date == homNay)
                    .Select(x => new { x.CaLam })
                    .ToList();

                int caSang = thongKeCa.Count(x => x.CaLam != null && x.CaLam.ToLower().Contains("sáng"));
                int caChieu = thongKeCa.Count(x => x.CaLam != null && x.CaLam.ToLower().Contains("chiều"));
                int caDem = thongKeCa.Count(x => x.CaLam != null && (x.CaLam.ToLower().Contains("đêm") || x.CaLam.ToLower().Contains("tối")));

                lblSang.Text = caSang.ToString();
                lblChieu.Text = caChieu.ToString();
                lblDem.Text = caDem.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thống kê: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= TẢI DỮ LIỆU LÊN GRIDVIEW ==================
        private void LoadDGVAll()
        {
            HienThiDanhSach(db.LichPhanCongs.AsQueryable());
        }

        private void HienThiDanhSach(IQueryable<LichPhanCong> query)
        {
            try
            {
                FormatGridStyles();

                var listData = query
                    .AsNoTracking()
                    .Select(x => new
                    {
                        MaLich = x.MaLich,
                        MaNV = x.MaNV ?? 0,
                        TenNV = x.NhanVien != null ? x.NhanVien.HoTen : (x.MaNV.HasValue ? "NV #" + x.MaNV : "Chưa phân công"),
                        SDT = x.NhanVien != null ? x.NhanVien.SDT : "",
                        NgayLam = x.NgayLam,
                        CaLam = x.CaLam ?? "",
                        GioBatDau = x.GioBatDau.HasValue ? x.GioBatDau.Value.ToString(@"hh\:mm") : "",
                        GioKetThuc = x.GioKetThuc.HasValue ? x.GioKetThuc.Value.ToString(@"hh\:mm") : "",
                        GhiChu = x.GhiChu ?? ""
                    })
                    .OrderByDescending(x => x.NgayLam)
                    .ThenByDescending(x => x.MaLich)
                    .ToList();

                dgvLichPhanCong.DataSource = listData;
                SetGridHeader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị danh sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGridStyles()
        {
            dgvLichPhanCong.Columns.Clear();
            dgvLichPhanCong.RowHeadersVisible = false;

            dgvLichPhanCong.EnableHeadersVisualStyles = false;
            dgvLichPhanCong.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 55);
            dgvLichPhanCong.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gold;
            dgvLichPhanCong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvLichPhanCong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvLichPhanCong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvLichPhanCong.ColumnHeadersHeight = 35;

            dgvLichPhanCong.RowsDefaultCellStyle.BackColor = Color.White;
            dgvLichPhanCong.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvLichPhanCong.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dgvLichPhanCong.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            dgvLichPhanCong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLichPhanCong.MultiSelect = false;
        }

        private void SetGridHeader()
        {
            if (dgvLichPhanCong.Columns["MaLich"] == null) return;

            dgvLichPhanCong.Columns["MaLich"].HeaderText = "Mã lịch";
            dgvLichPhanCong.Columns["TenNV"].HeaderText = "Tên nhân viên";
            dgvLichPhanCong.Columns["SDT"].HeaderText = "Số điện thoại";
            dgvLichPhanCong.Columns["NgayLam"].HeaderText = "Ngày làm";
            dgvLichPhanCong.Columns["CaLam"].HeaderText = "Ca làm";
            dgvLichPhanCong.Columns["GioBatDau"].HeaderText = "Giờ bắt đầu";
            dgvLichPhanCong.Columns["GioKetThuc"].HeaderText = "Giờ kết thúc";
            dgvLichPhanCong.Columns["GhiChu"].HeaderText = "Ghi chú";

            if (dgvLichPhanCong.Columns["MaNV"] != null)
                dgvLichPhanCong.Columns["MaNV"].Visible = false;

            dgvLichPhanCong.Columns["NgayLam"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvLichPhanCong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // ================= CLICK DÒNG TRÊN DATAGRIDVIEW ==================
        private void dgvLichPhanCong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvLichPhanCong.Rows[e.RowIndex];
            if (row.Cells["MaLich"].Value == null) return;

            maLichDangChon = Convert.ToInt32(row.Cells["MaLich"].Value);

            string tenNV = row.Cells["TenNV"].Value?.ToString() ?? "";
            int maNV = row.Cells["MaNV"].Value != null ? Convert.ToInt32(row.Cells["MaNV"].Value) : 0;

            txtNV.Text = maNV > 0 ? $"{maNV} - {tenNV}" : tenNV;

            if (row.Cells["NgayLam"].Value != null && row.Cells["NgayLam"].Value != DBNull.Value)
            {
                dtpNgayLam.Value = Convert.ToDateTime(row.Cells["NgayLam"].Value);
            }

            string caLamGrid = row.Cells["CaLam"].Value?.ToString() ?? "";
            bool matched = false;
            for (int i = 0; i < cbCaLam.Items.Count; i++)
            {
                string itemText = cbCaLam.Items[i].ToString();
                if (!string.IsNullOrEmpty(caLamGrid) && itemText.ToLower().Contains(caLamGrid.ToLower()))
                {
                    cbCaLam.SelectedIndex = i;
                    matched = true;
                    break;
                }
            }
            if (!matched && cbCaLam.Items.Count > 0) cbCaLam.SelectedIndex = 0;

            txtGhiChu.Text = row.Cells["GhiChu"].Value?.ToString() ?? "";
        }

        private (TimeSpan gioBD, TimeSpan gioKT) GetGioTheoCa(string caLamStr)
        {
            string lowerStr = caLamStr.ToLower();
            if (lowerStr.Contains("sáng"))
                return (new TimeSpan(6, 0, 0), new TimeSpan(14, 0, 0));
            if (lowerStr.Contains("chiều"))
                return (new TimeSpan(14, 0, 0), new TimeSpan(22, 0, 0));

            return (new TimeSpan(22, 0, 0), new TimeSpan(6, 0, 0));
        }

        private int ParseMaNV(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return -1;

            string firstPart = input.Split('-')[0].Trim();
            if (int.TryParse(firstPart, out int id))
            {
                return id;
            }

            var idNV = db.NhanViens.AsNoTracking()
                .Where(x => x.HoTen.ToLower() == input.Trim().ToLower())
                .Select(x => x.MaNV)
                .FirstOrDefault();

            return idNV > 0 ? idNV : -1;
        }

        // ================= HÀM KIỂM TRA TRÙNG LỊCH / CHỒNG CHÉO THỜI GIAN ==================
        private bool KiemTraTrungGioLam(int maNV, DateTime ngayLamMoi, TimeSpan gioBDMoi, TimeSpan gioKTMoi, int maLichBoQua = -1)
        {
            DateTime dtStartMoi = ngayLamMoi.Date.Add(gioBDMoi);
            DateTime dtEndMoi = gioKTMoi < gioBDMoi
                ? ngayLamMoi.Date.AddDays(1).Add(gioKTMoi)
                : ngayLamMoi.Date.Add(gioKTMoi);

            DateTime ngayTu = ngayLamMoi.Date.AddDays(-1);
            DateTime ngayDen = ngayLamMoi.Date.AddDays(1);

            var dsLichCu = db.LichPhanCongs
                .AsNoTracking()
                .Where(x => x.MaNV == maNV && x.MaLich != maLichBoQua && x.NgayLam.Date >= ngayTu && x.NgayLam.Date <= ngayDen)
                .Select(x => new { x.NgayLam, x.GioBatDau, x.GioKetThuc })
                .ToList();

            foreach (var lich in dsLichCu)
            {
                if (!lich.GioBatDau.HasValue || !lich.GioKetThuc.HasValue) continue;

                DateTime dtStartCu = lich.NgayLam.Date.Add(lich.GioBatDau.Value);
                DateTime dtEndCu = lich.GioKetThuc.Value < lich.GioBatDau.Value
                    ? lich.NgayLam.Date.AddDays(1).Add(lich.GioKetThuc.Value)
                    : lich.NgayLam.Date.Add(lich.GioKetThuc.Value);

                if (dtStartMoi < dtEndCu && dtEndMoi > dtStartCu)
                {
                    return true;
                }
            }

            return false;
        }

        // ================= SỰ KIỆN NÚT LỌC CA TRONG NGÀY HÔM NAY ==================
        private void LocTheoCaHomNay(string tenCa)
        {
            DateTime homNay = DateTime.Today;
            dtpTimNgayLam.Value = homNay;

            var query = db.LichPhanCongs
                .Where(x => x.NgayLam.Date == homNay
                         && x.CaLam != null
                         && x.CaLam.ToLower().Contains(tenCa.ToLower()));

            HienThiDanhSach(query);
        }

        private void btnSang_Click_1(object sender, EventArgs e)
        {
            LocTheoCaHomNay("sáng");
        }

        private void btnChieu_Click_1(object sender, EventArgs e)
        {
            LocTheoCaHomNay("chiều");
        }

        private void btnDem_Click_1(object sender, EventArgs e)
        {
            DateTime homNay = DateTime.Today;
            dtpTimNgayLam.Value = homNay;

            var query = db.LichPhanCongs
                .Where(x => x.NgayLam.Date == homNay
                         && x.CaLam != null
                         && (x.CaLam.ToLower().Contains("đêm") || x.CaLam.ToLower().Contains("tối")));

            HienThiDanhSach(query);
        }

        // ================= NÚT BẤM SỰ KIỆN KHÁC ==================
        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            txtNV.Clear();
            txtGhiChu.Clear();
            dtpNgayLam.Value = DateTime.Today;
            cbCaLam.SelectedIndex = 0;
            maLichDangChon = -1;
            txtNV.Focus();
        }

        private void btnHomNay_Click(object sender, EventArgs e)
        {
            DateTime homNay = DateTime.Today;
            dtpTimNgayLam.Value = homNay;
            cbTimCaLam.SelectedIndex = 0;

            var query = db.LichPhanCongs.Where(x => x.NgayLam.Date == homNay);
            HienThiDanhSach(query);
        }

        private void btnHienThi_Click_1(object sender, EventArgs e)
        {
            cbTimCaLam.SelectedIndex = 0;
            dtpTimNgayLam.Value = DateTime.Today;
            LoadDGVAll();
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            int maNV = ParseMaNV(txtNV.Text);
            if (maNV <= 0)
            {
                MessageBox.Show("Vui lòng nhập Mã nhân viên (hoặc Tên) hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNV.Focus();
                return;
            }

            try
            {
                // Chỉ Select thuộc tính HoTen để tránh nạp các thuộc tính Null bị lỗi
                var tenNV = db.NhanViens
                    .AsNoTracking()
                    .Where(x => x.MaNV == maNV)
                    .Select(x => x.HoTen)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(tenNV))
                {
                    MessageBox.Show($"Không tìm thấy nhân viên có mã {maNV} trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string caLamChon = cbCaLam.SelectedItem?.ToString() ?? "Ca sáng (6:00 - 14:00)";
                var (gioBD, gioKT) = GetGioTheoCa(caLamChon);
                string tenCaNgan = caLamChon.Split('(')[0].Trim();
                DateTime ngayChon = dtpNgayLam.Value.Date;

                bool daCoLich = db.LichPhanCongs.Any(x => x.MaNV == maNV
                                                        && x.NgayLam.Date == ngayChon
                                                        && x.CaLam != null
                                                        && x.CaLam.ToLower().Contains(tenCaNgan.ToLower()));
                if (daCoLich)
                {
                    MessageBox.Show($"Nhân viên [{tenNV}] đã được phân công {tenCaNgan} trong ngày {ngayChon:dd/MM/yyyy}!",
                                    "Cảnh báo trùng ca", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (KiemTraTrungGioLam(maNV, ngayChon, gioBD, gioKT))
                {
                    MessageBox.Show($"Nhân viên [{tenNV}] đã có lịch làm việc khác trùng/chồng chéo với khung giờ này!\n" +
                                    $"Vui lòng kiểm tra lại ca đêm ngày hôm trước hoặc các ca khác trong ngày.",
                                    "Cảnh báo trùng thời gian làm việc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LichPhanCong lpc = new LichPhanCong
                {
                    MaNV = maNV,
                    NgayLam = ngayChon,
                    CaLam = tenCaNgan,
                    GioBatDau = gioBD,
                    GioKetThuc = gioKT,
                    GhiChu = txtGhiChu.Text.Trim()
                };

                db.LichPhanCongs.Add(lpc);
                db.SaveChanges();

                MessageBox.Show("Thêm lịch phân công thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLamMoi_Click_1(sender, e);
                CapNhatThongKe();
                LoadDGVAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm phân công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            if (maLichDangChon <= 0)
            {
                MessageBox.Show("Vui lòng chọn 1 dòng lịch phân công bên dưới bảng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var lpc = db.LichPhanCongs.Find(maLichDangChon);
            if (lpc == null)
            {
                MessageBox.Show("Không tìm thấy dữ liệu lịch phân công cần sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int maNV = ParseMaNV(txtNV.Text);
            if (maNV <= 0)
            {
                MessageBox.Show("Mã nhân viên không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string caLamChon = cbCaLam.SelectedItem?.ToString() ?? "Ca sáng (6:00 - 14:00)";
            var (gioBD, gioKT) = GetGioTheoCa(caLamChon);
            string tenCaNgan = caLamChon.Split('(')[0].Trim();
            DateTime ngayChon = dtpNgayLam.Value.Date;

            bool trùngLich = db.LichPhanCongs.Any(x => x.MaLich != maLichDangChon
                                                       && x.MaNV == maNV
                                                       && x.NgayLam.Date == ngayChon
                                                       && x.CaLam != null
                                                       && x.CaLam.ToLower().Contains(tenCaNgan.ToLower()));
            if (trùngLich)
            {
                MessageBox.Show("Nhân viên này đã có lịch làm việc trong ca và ngày này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (KiemTraTrungGioLam(maNV, ngayChon, gioBD, gioKT, maLichDangChon))
            {
                MessageBox.Show("Khung giờ làm việc mới bị trùng/chồng chéo với lịch khác của nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                lpc.MaNV = maNV;
                lpc.NgayLam = ngayChon;
                lpc.CaLam = tenCaNgan;
                lpc.GioBatDau = gioBD;
                lpc.GioKetThuc = gioKT;
                lpc.GhiChu = txtGhiChu.Text.Trim();

                db.SaveChanges();

                MessageBox.Show("Cập nhật lịch phân công thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLamMoi_Click_1(sender, e);
                CapNhatThongKe();
                LoadDGVAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa phân công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if (maLichDangChon <= 0)
            {
                MessageBox.Show("Vui lòng chọn 1 dòng lịch phân công để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa lịch phân công mã [{maLichDangChon}] không?",
                                              "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    var lpc = db.LichPhanCongs.Find(maLichDangChon);
                    if (lpc != null)
                    {
                        db.LichPhanCongs.Remove(lpc);
                        db.SaveChanges();
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnLamMoi_Click_1(sender, e);
                        CapNhatThongKe();
                        LoadDGVAll();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }
    }
}