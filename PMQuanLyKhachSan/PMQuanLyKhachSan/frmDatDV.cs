using PhanMemQuanLyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace PMQuanLyKhachSan
{
    public partial class frmDatDV : Form
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        private const int MA_DV_KHONG = 0;

        public DataContext db = new DataContext();

        private KhachHang khachDangNhap;
        private NhanVien nhanVienDangNhap;

        private bool laKhachHang;

        private List<DichVu> _dsDichVu;

        private class PhongDaDatVM
        {
            public int MaDatPhong { get; set; }
            public string HoTen { get; set; }
            public string TenPhong { get; set; }
            public string LoaiPhong { get; set; }
            public DateTime NgayNhan { get; set; }
            public DateTime NgayTra { get; set; }
            public string TrangThai { get; set; }
        }

        //================ KHÁCH HÀNG ===================
        public frmDatDV(KhachHang kh)
        {
            InitializeComponent();

            khachDangNhap = kh;
            laKhachHang = true;

            this.FormClosed += frmDatDV_FormClosed;
        }

        //================ NHÂN VIÊN ====================
        public frmDatDV(NhanVien nv)
        {
            InitializeComponent();

            nhanVienDangNhap = nv;
            laKhachHang = false;

            this.FormClosed += frmDatDV_FormClosed;
        }

        private void frmDatDV_Load(object sender, EventArgs e)
        {
            SendMessage(txtMaDP.Handle, EM_SETCUEBANNER, (IntPtr)1, "Nhập mã đặt phòng...");
            SendMessage(txtTenKH.Handle, EM_SETCUEBANNER, (IntPtr)1, "Nhập họ tên khách hàng...");

            txtMaDP.ReadOnly = false;
            txtMaDP.KeyDown -= txtMaDP_KeyDown;
            txtMaDP.KeyDown += txtMaDP_KeyDown;

            dtpNgaySD.Value = DateTime.Now;
            dtpNgaySD.MinDate = DateTime.Today;

            dtpGioSD.Value = DateTime.Now;
            dtpGioSD.Format = DateTimePickerFormat.Time;
            dtpGioSD.ShowUpDown = true;

            numSL.Minimum = 1;
            numSL.Maximum = 20;
            numSL.Value = 1;

            _dsDichVu = db.DichVus
                .AsNoTracking()
                .ToList();

            LoadComboBox();

            LoadDGVDichVu();

            LoadDGVPhongDaDat();

            if (laKhachHang)
            {
                txtTenKH.Text = khachDangNhap.HoTen;
                txtTenKH.ReadOnly = true;
            }
            else
            {
                txtTenKH.Clear();
                txtTenKH.ReadOnly = false;

                txtTenKH.KeyDown -= txtTenKH_KeyDown;
                txtTenKH.KeyDown += txtTenKH_KeyDown;
            }

            dgvPhongDaDat.CellClick -= dgvPhongDaDat_CellClick;
            dgvPhongDaDat.CellClick += dgvPhongDaDat_CellClick;

            if (dgvDSDV.Rows.Count > 0)
            {
                dgvDSDV.Rows[0].Selected = true;
                dgvDSDV.CurrentCell = dgvDSDV.Rows[0].Cells["TenDV"];
            }
        }

        //=============================LOAD COMBOBOX DỊCH VỤ =======================
        private void LoadComboBox()
        {
            // Nhân bản danh sách từ cache để thêm tùy chọn "(Không)" vào đầu danh sách ComboBox
            var dsCB = new List<DichVu>(_dsDichVu);
            dsCB.Insert(0, new DichVu { MaDV = MA_DV_KHONG, TenDV = "(None)", Gia = 0 });

            cbLoaiDV.DataSource = dsCB;
            cbLoaiDV.DisplayMember = "TenDV";
            cbLoaiDV.ValueMember = "MaDV";
        }

        //=================================================
        // LOAD DGV DỊCH VỤ
        //=================================================
        private void LoadDGVDichVu()
        {
            dgvDSDV.AutoGenerateColumns = false;
            dgvDSDV.Columns.Clear();
            dgvDSDV.DataSource = null;

            dgvDSDV.Columns.Add("MaDV", "Mã DV");
            dgvDSDV.Columns.Add("TenDV", "Tên dịch vụ");
            dgvDSDV.Columns.Add("Gia", "Đơn giá");

            dgvDSDV.Columns["MaDV"].DataPropertyName = "MaDV";
            dgvDSDV.Columns["TenDV"].DataPropertyName = "TenDV";
            dgvDSDV.Columns["Gia"].DataPropertyName = "Gia";

            dgvDSDV.DataSource = _dsDichVu
                .Select(x => new { x.MaDV, x.TenDV, x.Gia })
                .ToList();

            dgvDSDV.Columns["Gia"].DefaultCellStyle.Format = "N0";
            dgvDSDV.RowHeadersVisible = false;

            ApplyGridStyle(dgvDSDV);
        }

        private List<PhongDaDatVM> LayDanhSachPhongDaDat(string tenTimKiem = null, int? maDatPhongTimKiem = null)
        {
            var query = db.DatPhongs
                .AsNoTracking()
                .Include(x => x.KhachHang)
                .Include(x => x.Phong)
                .ThenInclude(x => x.LoaiPhong)
                .Where(x => x.NgayTra > DateTime.Now);

            if (laKhachHang)
            {
                query = query.Where(x => x.MaKH == khachDangNhap.MaKH);
            }

            if (maDatPhongTimKiem.HasValue)
            {
                query = query.Where(x => x.MaDatPhong == maDatPhongTimKiem.Value);
            }

            if (!string.IsNullOrEmpty(tenTimKiem))
            {
                query = query.Where(x => x.KhachHang.HoTen.ToLower().Contains(tenTimKiem));
            }

            return query
                .Select(x => new PhongDaDatVM
                {
                    MaDatPhong = x.MaDatPhong,
                    HoTen = x.KhachHang.HoTen,
                    TenPhong = x.Phong.TenPhong,
                    LoaiPhong = x.Phong.LoaiPhong.TenLoai,
                    NgayNhan = x.NgayNhan,
                    NgayTra = x.NgayTra,
                    TrangThai = x.TrangThai
                })
                .ToList();
        }

        //=================================================
        // LOAD DGV PHÒNG ĐÃ ĐẶT
        //=================================================
        private void LoadDGVPhongDaDat()
        {
            dgvPhongDaDat.AutoGenerateColumns = false;
            dgvPhongDaDat.Columns.Clear();
            dgvPhongDaDat.DataSource = null;

            dgvPhongDaDat.Columns.Add("MaDatPhong", "Mã ĐP");
            dgvPhongDaDat.Columns.Add("HoTen", "Tên khách");
            dgvPhongDaDat.Columns.Add("TenPhong", "Phòng");
            dgvPhongDaDat.Columns.Add("LoaiPhong", "Loại phòng");
            dgvPhongDaDat.Columns.Add("NgayNhan", "Ngày nhận");
            dgvPhongDaDat.Columns.Add("NgayTra", "Ngày trả");
            dgvPhongDaDat.Columns.Add("TrangThai", "Trạng thái");

            dgvPhongDaDat.Columns["MaDatPhong"].DataPropertyName = "MaDatPhong";
            dgvPhongDaDat.Columns["HoTen"].DataPropertyName = "HoTen";
            dgvPhongDaDat.Columns["TenPhong"].DataPropertyName = "TenPhong";
            dgvPhongDaDat.Columns["LoaiPhong"].DataPropertyName = "LoaiPhong";
            dgvPhongDaDat.Columns["NgayNhan"].DataPropertyName = "NgayNhan";
            dgvPhongDaDat.Columns["NgayTra"].DataPropertyName = "NgayTra";
            dgvPhongDaDat.Columns["TrangThai"].DataPropertyName = "TrangThai";

            dgvPhongDaDat.DataSource = LayDanhSachPhongDaDat();

            dgvPhongDaDat.Columns["NgayNhan"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvPhongDaDat.Columns["NgayTra"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvPhongDaDat.RowHeadersVisible = false;

            ApplyGridStyle(dgvPhongDaDat);
        }

        private void ApplyGridStyle(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 55);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gold;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 35;

            dgv.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
            dgv.GridColor = Color.Gray;

            dgv.RowsDefaultCellStyle.BackColor = Color.White;
            dgv.RowsDefaultCellStyle.ForeColor = Color.Black;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
        }

        // LỌC THÔNG TIN PHÒNG ĐÃ ĐẶT THEO MÃ ĐẶT PHÒNG KHI NHẤN ENTER
        private void txtMaDP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            e.SuppressKeyPress = true; 

            string input = txtMaDP.Text.Trim();

            if (string.IsNullOrEmpty(input))
            {
                dgvPhongDaDat.DataSource = LayDanhSachPhongDaDat(txtTenKH.Text.Trim().ToLower());
                return;
            }

            if (int.TryParse(input, out int maDP))
            {
                string tenTimKiem = txtTenKH.Text.Trim().ToLower();
                var result = LayDanhSachPhongDaDat(tenTimKiem, maDP);

                if (result == null || result.Count == 0)
                {
                    MessageBox.Show($"Không tìm thấy thông tin đặt phòng ứng với mã: {maDP}",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dgvPhongDaDat.DataSource = null;
                    txtMaDP.SelectAll();
                    txtMaDP.Focus();
                    return;
                }

                dgvPhongDaDat.DataSource = result;
                if (dgvPhongDaDat.Rows.Count > 0)
                {
                    dgvPhongDaDat.Rows[0].Selected = true;
                    if (!laKhachHang)
                    {
                        txtTenKH.Text = result[0].HoTen;
                    }
                }
            }
            else
            {
                MessageBox.Show("Mã đặt phòng phải là số nguyên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDP.SelectAll();
                txtMaDP.Focus();
            }
        }
        private void txtTenKH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            e.SuppressKeyPress = true;

            string rawInput = txtTenKH.Text.Trim();

            if (string.IsNullOrEmpty(rawInput))
            {
                int? maDPNull = null;
                if (int.TryParse(txtMaDP.Text.Trim(), out int tempMaDPNull))
                {
                    maDPNull = tempMaDPNull;
                }
                dgvPhongDaDat.DataSource = LayDanhSachPhongDaDat(null, maDPNull);
                return;
            }

            if (Regex.IsMatch(rawInput, @"[\d!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]"))
            {
                MessageBox.Show("Tên khách hàng không hợp lệ! Vui lòng chỉ nhập chữ cái.",
                                "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.SelectAll();
                txtTenKH.Focus();
                return;
            }

            string tenTimKiem = rawInput.ToLower();

            int? maDP = null;
            if (int.TryParse(txtMaDP.Text.Trim(), out int tempMaDP))
            {
                maDP = tempMaDP;
            }

            var result = LayDanhSachPhongDaDat(tenTimKiem, maDP);

            if (result == null || result.Count == 0)
            {
                MessageBox.Show($"Không tìm thấy phòng nào đã đặt cho khách hàng: \"{rawInput}\"",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                dgvPhongDaDat.DataSource = null;
                txtTenKH.SelectAll();
                txtTenKH.Focus();
                return;
            }

            dgvPhongDaDat.DataSource = result;
        }

        private void dgvPhongDaDat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            txtMaDP.Text = dgvPhongDaDat.Rows[e.RowIndex].Cells["MaDatPhong"].Value.ToString();
            txtTenKH.Text = dgvPhongDaDat.Rows[e.RowIndex].Cells["HoTen"].Value.ToString();
        }

        private void dgvPhongDaDat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgvDSDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int maDV = Convert.ToInt32(dgvDSDV.Rows[e.RowIndex].Cells["MaDV"].Value);
            cbLoaiDV.SelectedValue = maDV;
        }

        //=================================================
        // XỬ LÝ SỰ KIỆN CHỌN DỊCH VỤ TRÊN COMBOBOX
        //=================================================
        private void cbLoaiDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(cbLoaiDV.SelectedItem is DichVu dv))
                return;

            if (dv.MaDV == MA_DV_KHONG)
            {
                lblSL.Text = "Số lượng";
                numSL.Enabled = false;
                numSL.Value = 1;
                dgvDSDV.ClearSelection();
                return;
            }

            numSL.Enabled = true;
            lblSL.Text = "Số lượng";
            numSL.Minimum = 1;
            numSL.Maximum = 20;

            // Đồng bộ chọn dòng tương ứng trên DataGridView Dịch vụ
            foreach (DataGridViewRow row in dgvDSDV.Rows)
            {
                if (Convert.ToInt32(row.Cells["MaDV"].Value) == dv.MaDV)
                {
                    row.Selected = true;
                    dgvDSDV.CurrentCell = row.Cells["TenDV"];
                    break;
                }
            }
        }

        //=================================================
        // NÚT ĐẶT DỊCH VỤ
        //=================================================
        private void btnDatDV_Click(object sender, EventArgs e)
        {
            if (cbLoaiDV.SelectedValue == null || (int)cbLoaiDV.SelectedValue == MA_DV_KHONG)
            {
                MessageBox.Show("Vui lòng chọn một dịch vụ cụ thể!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbLoaiDV.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMaDP.Text))
            {
                MessageBox.Show("Vui lòng nhập hoặc chọn một phòng đã đặt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDP.Focus();
                return;
            }

            if (!int.TryParse(txtMaDP.Text.Trim(), out int maDP))
            {
                MessageBox.Show("Mã đặt phòng không hợp lệ!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaDP.SelectAll();
                txtMaDP.Focus();
                return;
            }

            if (!(cbLoaiDV.SelectedItem is DichVu dv))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var datPhongInfo = db.DatPhongs
                .AsNoTracking()
                .FirstOrDefault(x => x.MaDatPhong == maDP);

            if (datPhongInfo == null)
            {
                MessageBox.Show($"Mã đặt phòng {maDP} không tồn tại hoặc không hợp lệ!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaDP.SelectAll();
                txtMaDP.Focus();
                return;
            }

            DateTime ngaySD = dtpNgaySD.Value.Date;
            DateTime homNay = DateTime.Today;
            DateTime ngayNhan = datPhongInfo.NgayNhan.Date;
            DateTime ngayTra = datPhongInfo.NgayTra.Date;

            if (ngaySD < homNay)
            {
                MessageBox.Show($"Ngày sử dụng dịch vụ không được nhỏ hơn ngày hiện tại ({homNay:dd/MM/yyyy})!",
                                "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpNgaySD.Focus();
                return;
            }

            if (ngaySD < ngayNhan || ngaySD > ngayTra)
            {
                MessageBox.Show($"Ngày sử dụng dịch vụ không hợp lệ!\n" +
                                $"Thời gian sử dụng phải nằm trong khoảng thuê phòng " +
                                $"(từ {ngayNhan:dd/MM/yyyy} đến {ngayTra:dd/MM/yyyy}).",
                                "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpNgaySD.Focus();
                return;
            }

            try
            {
                DateTime ngayGioSuDung = dtpNgaySD.Value.Date + dtpGioSD.Value.TimeOfDay;

                ChiTietDichVu ct = new ChiTietDichVu
                {
                    MaDatPhong = maDP,
                    MaDV = dv.MaDV,
                    SoLuong = (int)numSL.Value,
                    DonGia = dv.Gia,
                    NgaySuDung = ngayGioSuDung
                };

                ct.ThanhTien = ct.SoLuong * ct.DonGia;

                db.ChiTietDichVus.Add(ct);
                db.SaveChanges();

                MessageBox.Show("Đặt dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtMaDP.Clear();
                if (!laKhachHang) txtTenKH.Clear();
                cbLoaiDV.SelectedIndex = 0; 
                dtpNgaySD.Value = DateTime.Now;
                dtpGioSD.Value = DateTime.Now;
                numSL.Value = 1;
                dgvPhongDaDat.ClearSelection();
                dgvDSDV.ClearSelection();

                LoadDGVPhongDaDat();
            }
            catch (Exception ex)
            {
                string innerError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show($"Lỗi khi lưu vào Database:\n{innerError}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDSDV_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void frmDatDV_FormClosed(object sender, FormClosedEventArgs e)
        {
            db?.Dispose();
        }
    }
}