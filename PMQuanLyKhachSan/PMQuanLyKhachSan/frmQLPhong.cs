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
    public partial class frmQLPhong : Form
    {
        private NhanVien nhanVienDangNhap;

        #region Win32 API hỗ trợ Placeholder cho TextBox

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private const int EM_SETCUEBANNER = 0x1501;

        /// <summary>
        /// Gán text gợi ý (placeholder) cho TextBox
        /// </summary>
        private void SetPlaceholder(TextBox textBox, string placeholderText)
        {
            if (textBox != null && !textBox.IsDisposed)
            {
                SendMessage(textBox.Handle, EM_SETCUEBANNER, (IntPtr)1, placeholderText);
            }
        }

        #endregion

        public frmQLPhong()
        {
            InitializeComponent();
        }

        public frmQLPhong(NhanVien nv) : this()
        {
            nhanVienDangNhap = nv;
        }

        private void frmQLPhong_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

            txtMaPhong.ReadOnly = true;
            txtGiaPhong.ReadOnly = true;
            txtMoTa.ReadOnly = true;

            if (txtTrangThai != null)
            {
                txtTrangThai.ReadOnly = true;
            }

            SetupPlaceholders();

            SetupDataGridViewStyle();

            LoadComboboxLoaiPhong();

            CapNhatTrangThaiPhongThucTe();
            LoadDataPhong();
            LamMoiForm();
        }

        #region Helper Methods (Các hàm trợ giúp)

        private void SetupPlaceholders()
        {
            SetPlaceholder(txtMaPhong, "Mã tự động...");
            SetPlaceholder(txtTenPhong, "Nhập tên phòng (VD: Phòng 101)...");
            SetPlaceholder(txtGiaPhong, "Giá hiển thị theo loại phòng...");
            SetPlaceholder(txtMoTa, "Mô tả loại phòng...");

            if (txtTrangThai != null)
            {
                SetPlaceholder(txtTrangThai, "Trạng thái phòng...");
            }
        }

        private void SetupDataGridViewStyle()
        {
            if (dgvPhong == null) return;

            dgvPhong.ReadOnly = true;
            dgvPhong.AllowUserToAddRows = false;
            dgvPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhong.MultiSelect = false;
            dgvPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

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

            dgvPhong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvPhong.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            dgvPhong.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 122, 204);
            dgvPhong.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void FormatGridViewColumns()
        {
            if (dgvPhong == null) return;

            if (dgvPhong.Columns["MaPhong"] != null) dgvPhong.Columns["MaPhong"].HeaderText = "Mã phòng";
            if (dgvPhong.Columns["TenPhong"] != null) dgvPhong.Columns["TenPhong"].HeaderText = "Tên phòng";
            if (dgvPhong.Columns["TenLoai"] != null) dgvPhong.Columns["TenLoai"].HeaderText = "Loại phòng";
            if (dgvPhong.Columns["Gia"] != null) dgvPhong.Columns["Gia"].HeaderText = "Giá phòng";
            if (dgvPhong.Columns["MoTa"] != null) dgvPhong.Columns["MoTa"].HeaderText = "Mô tả";
            if (dgvPhong.Columns["TrangThai"] != null) dgvPhong.Columns["TrangThai"].HeaderText = "Trạng thái";
            if (dgvPhong.Columns["NgayDen"] != null) dgvPhong.Columns["NgayDen"].HeaderText = "Ngày đến";
            if (dgvPhong.Columns["NgayDi"] != null) dgvPhong.Columns["NgayDi"].HeaderText = "Ngày đi";

            if (dgvPhong.Columns["MaLoai"] != null) dgvPhong.Columns["MaLoai"].Visible = false;
        }

        public void CapNhatTrangThaiPhongThucTe()
        {
            try
            {
                using (var db = new DataContext())
                {
                    DateTime homNay = DateTime.Now.Date;

                    var dsDatPhongHomNay = db.DatPhongs
                        .Where(dp => dp.NgayNhan.Date <= homNay
                                  && dp.NgayTra.Date >= homNay
                                  && dp.TrangThai != "Đã trả phòng"
                                  && dp.TrangThai != "Đã hủy")
                        .ToList();

                    var dsPhong = db.Phongs.ToList();

                    foreach (var p in dsPhong)
                    {
                        var datPhong = dsDatPhongHomNay.FirstOrDefault(dp => dp.MaPhong == p.MaPhong);

                        if (datPhong != null)
                        {
                            p.TrangThai = datPhong.TrangThai;
                        }
                        else if (p.TrangThai != "Bảo trì")
                        {
                            p.TrangThai = "Trống";
                        }
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi CapNhatTrangThaiPhongThucTe: {ex.Message}");
            }
        }

        #endregion

        #region Load Data & Events

        private void LoadComboboxLoaiPhong()
        {
            try
            {
                using (var db = new DataContext())
                {
                    var listLoai = db.LoaiPhongs.AsNoTracking().ToList();

                    // Thêm mục "None" vào ComboBox loại phòng chính (dùng cho Thêm/Sửa)
                    var listLoaiForm = new List<LoaiPhong>(listLoai);
                    listLoaiForm.Insert(0, new LoaiPhong { MaLoai = 0, TenLoai = "None" });

                    cboLoaiPhong.SelectedIndexChanged -= cboLoaiPhong_SelectedIndexChanged;
                    cboLoaiPhong.DataSource = listLoaiForm;
                    cboLoaiPhong.DisplayMember = "TenLoai";
                    cboLoaiPhong.ValueMember = "MaLoai";
                    cboLoaiPhong.SelectedIndexChanged += cboLoaiPhong_SelectedIndexChanged;

                    // ComboBox Tra cứu / Tìm kiếm
                    var listLoaiTimKiem = new List<LoaiPhong>(listLoai);
                    listLoaiTimKiem.Insert(0, new LoaiPhong { MaLoai = 0, TenLoai = "--- Tất cả ---" });

                    cboTimLoaiPhong.SelectedIndexChanged -= cboTimLoaiPhong_SelectedIndexChanged;
                    cboTimLoaiPhong.DataSource = listLoaiTimKiem;
                    cboTimLoaiPhong.DisplayMember = "TenLoai";
                    cboTimLoaiPhong.ValueMember = "MaLoai";
                    cboTimLoaiPhong.SelectedIndexChanged += cboTimLoaiPhong_SelectedIndexChanged;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách loại phòng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadDataPhong()
        {
            try
            {
                using (var db = new DataContext())
                {
                    DateTime homNay = DateTime.Now.Date;

                    var dsDatPhongHomNay = db.DatPhongs
                        .AsNoTracking()
                        .Where(dp => dp.NgayNhan.Date <= homNay
                                  && dp.NgayTra.Date >= homNay
                                  && dp.TrangThai != "Đã trả phòng"
                                  && dp.TrangThai != "Đã hủy")
                        .ToList();

                    var dsPhong = db.Phongs
                        .AsNoTracking()
                        .Include(p => p.LoaiPhong)
                        .ToList();

                    var listPhong = dsPhong.Select(p =>
                    {
                        var datPhong = dsDatPhongHomNay.FirstOrDefault(dp => dp.MaPhong == p.MaPhong);

                        return new
                        {
                            p.MaPhong,
                            p.TenPhong,
                            TenLoai = p.LoaiPhong != null ? p.LoaiPhong.TenLoai : "",
                            Gia = p.LoaiPhong != null ? p.LoaiPhong.Gia.ToString("N0") + " VNĐ" : "0 VNĐ",
                            MoTa = p.LoaiPhong != null ? p.LoaiPhong.MoTa : "",
                            TrangThai = p.TrangThai,
                            NgayDen = datPhong != null ? datPhong.NgayNhan.ToString("dd/MM/yyyy") : "-",
                            NgayDi = datPhong != null ? datPhong.NgayTra.ToString("dd/MM/yyyy") : "-",
                            p.MaLoai
                        };
                    }).ToList();

                    if (dgvPhong != null)
                    {
                        dgvPhong.DataSource = listPhong;
                        FormatGridViewColumns();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách phòng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvPhong != null)
            {
                DataGridViewRow row = dgvPhong.Rows[e.RowIndex];

                txtMaPhong.Text = row.Cells["MaPhong"].Value?.ToString();
                txtTenPhong.Text = row.Cells["TenPhong"].Value?.ToString();

                if (row.Cells["MaLoai"].Value != null)
                {
                    cboLoaiPhong.SelectedValue = Convert.ToInt32(row.Cells["MaLoai"].Value);
                }
                else
                {
                    cboLoaiPhong.SelectedValue = 0;
                }

                if (row.Cells["TrangThai"].Value != null && txtTrangThai != null)
                {
                    txtTrangThai.Text = row.Cells["TrangThai"].Value.ToString();
                }
            }
        }

        private void cboLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiPhong.SelectedValue != null && int.TryParse(cboLoaiPhong.SelectedValue.ToString(), out int maLoai) && maLoai > 0)
            {
                using (var db = new DataContext())
                {
                    var loaiPhong = db.LoaiPhongs.AsNoTracking().FirstOrDefault(l => l.MaLoai == maLoai);
                    if (loaiPhong != null)
                    {
                        txtGiaPhong.Text = loaiPhong.Gia.ToString("N0") + " VNĐ";
                        txtMoTa.Text = loaiPhong.MoTa;
                    }
                }
            }
            else
            {
                // Chọn "None" -> Xóa trống Giá và Mô tả
                txtGiaPhong.Clear();
                txtMoTa.Clear();
            }
        }

        private void cboTimLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimTheoLoaiPhong();
        }

        #endregion

        #region CRUD Operations (Thêm, Sửa, Xóa, Làm mới)

        private void btThem_Click_1(object sender, EventArgs e)
        {
            try
            {
                string tenPhong = txtTenPhong.Text.Trim();

                if (string.IsNullOrWhiteSpace(tenPhong))
                {
                    MessageBox.Show("Vui lòng nhập tên phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenPhong.Focus();
                    return;
                }

                // Kiểm tra loại phòng (chọn "None" - maLoai == 0 sẽ không hợp lệ)
                if (cboLoaiPhong.SelectedValue == null ||
                    !int.TryParse(cboLoaiPhong.SelectedValue.ToString(), out int maLoai) ||
                    maLoai <= 0)
                {
                    MessageBox.Show("Vui lòng chọn loại phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var db = new DataContext())
                {
                    bool tonTai = db.Phongs.Any(p => p.TenPhong.ToLower() == tenPhong.ToLower());
                    if (tonTai)
                    {
                        MessageBox.Show("Tên phòng đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTenPhong.Focus();
                        return;
                    }

                    Phong phongMoi = new Phong
                    {
                        TenPhong = tenPhong,
                        MaLoai = maLoai,
                        TrangThai = "Trống"
                    };

                    db.Phongs.Add(phongMoi);
                    db.SaveChanges();

                    txtMaPhong.Text = phongMoi.MaPhong.ToString();
                    MessageBox.Show("Thêm phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CapNhatTrangThaiPhongThucTe();
                LoadDataPhong();
                LamMoiForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm phòng:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaPhong.Text) || !int.TryParse(txtMaPhong.Text, out int maPhong))
                {
                    MessageBox.Show("Vui lòng chọn phòng cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string tenPhongMoi = txtTenPhong.Text.Trim();
                if (string.IsNullOrWhiteSpace(tenPhongMoi))
                {
                    MessageBox.Show("Vui lòng nhập tên phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenPhong.Focus();
                    return;
                }

                if (cboLoaiPhong.SelectedValue == null ||
                    !int.TryParse(cboLoaiPhong.SelectedValue.ToString(), out int maLoaiMoi) ||
                    maLoaiMoi <= 0)
                {
                    MessageBox.Show("Vui lòng chọn loại phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var db = new DataContext())
                {
                    var pSua = db.Phongs.Find(maPhong);
                    if (pSua != null)
                    {
                        bool tonTai = db.Phongs.Any(p => p.MaPhong != maPhong && p.TenPhong.ToLower() == tenPhongMoi.ToLower());
                        if (tonTai)
                        {
                            MessageBox.Show("Tên phòng đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtTenPhong.Focus();
                            return;
                        }

                        pSua.TenPhong = tenPhongMoi;
                        pSua.MaLoai = maLoaiMoi;

                        if (txtTrangThai != null && !string.IsNullOrWhiteSpace(txtTrangThai.Text))
                        {
                            pSua.TrangThai = txtTrangThai.Text.Trim();
                        }

                        db.SaveChanges();

                        MessageBox.Show("Cập nhật thông tin phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CapNhatTrangThaiPhongThucTe();
                        LoadDataPhong();
                        LamMoiForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi sửa thông tin: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaPhong.Text) || !int.TryParse(txtMaPhong.Text, out int maPhong))
                {
                    MessageBox.Show("Vui lòng chọn phòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var db = new DataContext())
                {
                    bool coLichSuDatPhong = db.DatPhongs.Any(dp => dp.MaPhong == maPhong);
                    if (coLichSuDatPhong)
                    {
                        MessageBox.Show("Không thể xóa phòng này vì đang chứa lịch sử đặt phòng!", "Lỗi ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                    {
                        return;
                    }

                    var pXoa = db.Phongs.Find(maPhong);
                    if (pXoa != null)
                    {
                        db.Phongs.Remove(pXoa);
                        db.SaveChanges();
                        MessageBox.Show("Xóa phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CapNhatTrangThaiPhongThucTe();
                        LoadDataPhong();
                        LamMoiForm();
                    }
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Không thể xóa phòng này vì đang chứa lịch sử đặt phòng!", "Lỗi ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            LamMoiForm();
            CapNhatTrangThaiPhongThucTe();
            LoadDataPhong();
        }

        private void LamMoiForm()
        {
            txtMaPhong.Clear();
            txtTenPhong.Clear();
            txtGiaPhong.Clear();
            txtMoTa.Clear();

            if (txtTrangThai != null)
            {
                txtTrangThai.Text = "Trống";
            }

            if (cboLoaiPhong.Items.Count > 0)
            {
                cboLoaiPhong.SelectedValue = 0; // Đặt về mặc định "None"
            }

            if (dgvPhong != null)
            {
                dgvPhong.ClearSelection();
            }

            txtTenPhong.Focus();
        }

        #endregion

        #region Search & Filter (Tìm kiếm & Tra cứu)

        private void TimTheoLoaiPhong()
        {
            try
            {
                if (cboTimLoaiPhong.SelectedValue == null) return;

                if (!int.TryParse(cboTimLoaiPhong.SelectedValue.ToString(), out int maLoaiTim))
                    return;

                using (var db = new DataContext())
                {
                    DateTime homNay = DateTime.Now.Date;

                    var dsDatPhongHomNay = db.DatPhongs
                        .AsNoTracking()
                        .Where(dp => dp.NgayNhan.Date <= homNay
                                  && dp.NgayTra.Date >= homNay
                                  && dp.TrangThai != "Đã trả phòng"
                                  && dp.TrangThai != "Đã hủy")
                        .ToList();

                    var query = db.Phongs
                        .AsNoTracking()
                        .Include(p => p.LoaiPhong)
                        .AsQueryable();

                    if (maLoaiTim > 0)
                        query = query.Where(p => p.MaLoai == maLoaiTim);

                    var result = query.ToList().Select(p =>
                    {
                        var datPhong = dsDatPhongHomNay.FirstOrDefault(dp => dp.MaPhong == p.MaPhong);

                        return new
                        {
                            p.MaPhong,
                            p.TenPhong,
                            TenLoai = p.LoaiPhong != null ? p.LoaiPhong.TenLoai : "",
                            Gia = p.LoaiPhong != null ? p.LoaiPhong.Gia.ToString("N0") + " VNĐ" : "0 VNĐ",
                            MoTa = p.LoaiPhong != null ? p.LoaiPhong.MoTa : "",
                            TrangThai = p.TrangThai,
                            NgayDen = datPhong != null ? datPhong.NgayNhan.ToString("dd/MM/yyyy") : "-",
                            NgayDi = datPhong != null ? datPhong.NgayTra.ToString("dd/MM/yyyy") : "-",
                            p.MaLoai
                        };
                    }).ToList();

                    if (dgvPhong != null)
                    {
                        dgvPhong.DataSource = result;
                        FormatGridViewColumns();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTraCuu_Click_1(object sender, EventArgs e)
        {
            try
            {
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date;

                if (tuNgay > denNgay)
                {
                    MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var db = new DataContext())
                {
                    var dsDatPhongLoc = db.DatPhongs
                        .AsNoTracking()
                        .Where(dp => dp.TrangThai != "Đã trả phòng"
                                  && dp.TrangThai != "Đã hủy"
                                  && dp.NgayNhan.Date < denNgay.AddDays(1)
                                  && dp.NgayTra.Date > tuNgay)
                        .OrderBy(dp => dp.NgayNhan)
                        .ToList();

                    var dsPhong = db.Phongs.AsNoTracking().Include(p => p.LoaiPhong).ToList();

                    var listResult = dsPhong.Select(p =>
                    {
                        var datPhong = dsDatPhongLoc.FirstOrDefault(dp => dp.MaPhong == p.MaPhong);

                        string trangThaiHienThi = p.TrangThai;
                        string ngayDen = "-";
                        string ngayDi = "-";

                        if (p.TrangThai == "Bảo trì")
                        {
                            trangThaiHienThi = "Bảo trì";
                        }
                        else if (datPhong != null)
                        {
                            trangThaiHienThi = datPhong.TrangThai;
                            ngayDen = datPhong.NgayNhan.ToString("dd/MM/yyyy");
                            ngayDi = datPhong.NgayTra.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            trangThaiHienThi = "Trống";
                        }

                        return new
                        {
                            p.MaPhong,
                            p.TenPhong,
                            TenLoai = p.LoaiPhong != null ? p.LoaiPhong.TenLoai : "",
                            Gia = p.LoaiPhong != null ? p.LoaiPhong.Gia.ToString("N0") + " VNĐ" : "0 VNĐ",
                            MoTa = p.LoaiPhong != null ? p.LoaiPhong.MoTa : "",
                            TrangThai = trangThaiHienThi,
                            NgayDen = ngayDen,
                            NgayDi = ngayDi,
                            p.MaLoai
                        };
                    }).ToList();

                    if (dgvPhong != null)
                    {
                        dgvPhong.DataSource = listResult;
                        FormatGridViewColumns();
                    }
                }

                MessageBox.Show($"Đã tra cứu tình trạng phòng từ {tuNgay:dd/MM/yyyy} đến {denNgay:dd/MM/yyyy}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tra cứu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void dgvPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}