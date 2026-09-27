using Microsoft.EntityFrameworkCore;
using PhanMemQuanLyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PMQuanLyKhachSan
{
    public partial class frmQLDichVu : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        private readonly DataContext db = new DataContext();
        private NhanVien nhanVienDangNhap;

        public frmQLDichVu(NhanVien nv)
        {
            InitializeComponent();
            this.nhanVienDangNhap = nv;
        }

        private void frmQLDichVu_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            txtMaDV.ReadOnly = true;

            SendMessage(txtTenDV.Handle, EM_SETCUEBANNER, (IntPtr)1, "Nhập tên dịch vụ...");
            SendMessage(txtGiaDV.Handle, EM_SETCUEBANNER, (IntPtr)1, "Nhập giá dịch vụ (VNĐ)...");

            SetupDataGridViewStyle();
            LoadDataDichVu();
        }

        #region Tối Ưu Giao Diện & Helper

        private void SetupDataGridViewStyle()
        {
            if (dgvDichVu == null) return;

            dgvDichVu.ReadOnly = true;
            dgvDichVu.AllowUserToAddRows = false;
            dgvDichVu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDichVu.MultiSelect = false;
            dgvDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvDichVu.EnableHeadersVisualStyles = false;

            dgvDichVu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 55);
            dgvDichVu.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gold;
            dgvDichVu.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvDichVu.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDichVu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDichVu.ColumnHeadersHeight = 35;

            dgvDichVu.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
            dgvDichVu.GridColor = Color.Gray;

            dgvDichVu.RowsDefaultCellStyle.BackColor = Color.White;
            dgvDichVu.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvDichVu.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvDichVu.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            dgvDichVu.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 122, 204);
            dgvDichVu.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void FormatGridViewColumns()
        {
            if (dgvDichVu == null) return;

            if (dgvDichVu.Columns["MaDV"] != null)
            {
                dgvDichVu.Columns["MaDV"].HeaderText = "Mã Dịch Vụ";
                dgvDichVu.Columns["MaDV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvDichVu.Columns["TenDV"] != null)
            {
                dgvDichVu.Columns["TenDV"].HeaderText = "Tên Dịch Vụ";
            }

            if (dgvDichVu.Columns["Gia"] != null)
            {
                dgvDichVu.Columns["Gia"].HeaderText = "Giá Dịch Vụ (VNĐ)";
                dgvDichVu.Columns["Gia"].DefaultCellStyle.Format = "#,##0";
                dgvDichVu.Columns["Gia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void LamMoiForm()
        {
            txtMaDV.Clear();
            txtTenDV.Clear();
            txtGiaDV.Clear();
            txtTenDV.Focus();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            db.Dispose();
            base.OnFormClosed(e);
        }

        #endregion

        #region Tải Dữ Liệu & Sự Kiện GridView

        private void LoadDataDichVu()
        {
            try
            {
                var listDV = db.DichVus
                    .AsNoTracking()
                    .Select(dv => new
                    {
                        dv.MaDV,
                        dv.TenDV,
                        dv.Gia
                    })
                    .ToList();

                if (dgvDichVu != null)
                {
                    dgvDichVu.DataSource = listDV;
                    FormatGridViewColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nạp dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDichVu != null)
            {
                DataGridViewRow row = dgvDichVu.Rows[e.RowIndex];

                txtMaDV.Text = row.Cells["MaDV"].Value?.ToString();
                txtTenDV.Text = row.Cells["TenDV"].Value?.ToString();

                if (row.Cells["Gia"].Value != null)
                {
                    decimal gia = Convert.ToDecimal(row.Cells["Gia"].Value);
                    txtGiaDV.Text = gia.ToString("#,##0");
                }
            }
        }

        private void txtGiaDV_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGiaDV.Text)) return;

            txtGiaDV.TextChanged -= txtGiaDV_TextChanged;

            int selectionStart = txtGiaDV.SelectionStart;
            int lengthBefore = txtGiaDV.Text.Length;

            string rawValue = txtGiaDV.Text.Replace(",", "").Trim();
            if (decimal.TryParse(rawValue, out decimal number))
            {
                txtGiaDV.Text = number.ToString("#,##0");
                int lengthAfter = txtGiaDV.Text.Length;
                txtGiaDV.SelectionStart = Math.Max(0, selectionStart + (lengthAfter - lengthBefore));
            }

            txtGiaDV.TextChanged += txtGiaDV_TextChanged;
        }

        #endregion

        #region Xử Lý Các Nút Chức Năng (CRUD) Pass 100% Test Case

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string tenDV = txtTenDV.Text.Trim();
                string giaRaw = txtGiaDV.Text.Replace(",", "").Trim();

                // TC_QLDV_01: Bỏ trống tên dịch vụ
                if (string.IsNullOrWhiteSpace(tenDV))
                {
                    MessageBox.Show("Vui lòng nhập Tên dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDV.Focus();
                    return;
                }

                // TC_QLDV_02: Giá âm hoặc không phải số
                if (!decimal.TryParse(giaRaw, out decimal gia) || gia < 0)
                {
                    MessageBox.Show("Giá dịch vụ phải là số hợp lệ (>= 0)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGiaDV.Focus();
                    return;
                }

                // TC_QLDV_03: Tên dịch vụ đã tồn tại
                if (db.DichVus.Any(d => d.TenDV.ToLower() == tenDV.ToLower()))
                {
                    MessageBox.Show("Tên dịch vụ này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // TC_QLDV_04: Thêm thành công
                var dvMoi = new DichVu
                {
                    TenDV = tenDV,
                    Gia = gia
                };

                db.DichVus.Add(dvMoi);
                db.SaveChanges();

                MessageBox.Show("Thêm dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDataDichVu();
                LamMoiForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm dịch vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // TC_QLDV_05: Chưa chọn dòng nào từ danh sách
                if (string.IsNullOrWhiteSpace(txtMaDV.Text) || !int.TryParse(txtMaDV.Text, out int maDV))
                {
                    MessageBox.Show("Vui lòng chọn dịch vụ cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string tenDV = txtTenDV.Text.Trim();
                string giaRaw = txtGiaDV.Text.Replace(",", "").Trim();

                // TC_QLDV_06: Để trống tên dịch vụ
                if (string.IsNullOrWhiteSpace(tenDV))
                {
                    MessageBox.Show("Tên dịch vụ không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDV.Focus();
                    return;
                }

                // Kiểm tra Giá dịch vụ hợp lệ
                if (!decimal.TryParse(giaRaw, out decimal gia) || gia < 0)
                {
                    MessageBox.Show("Giá dịch vụ phải là số hợp lệ (>= 0)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGiaDV.Focus();
                    return;
                }

                // TC_QLDV_07: Tên mới trùng với dịch vụ khác đã tồn tại
                if (db.DichVus.Any(d => d.TenDV.ToLower() == tenDV.ToLower() && d.MaDV != maDV))
                {
                    MessageBox.Show("Tên dịch vụ này trùng với dịch vụ khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // TC_QLDV_08: Cập nhật dịch vụ thành công
                var dvSua = db.DichVus.Find(maDV);
                if (dvSua != null)
                {
                    dvSua.TenDV = tenDV;
                    dvSua.Gia = gia;

                    db.SaveChanges();
                    MessageBox.Show("Cập nhật dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadDataDichVu();
                    LamMoiForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi sửa dịch vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaDV.Text) || !int.TryParse(txtMaDV.Text, out int maDV))
                {
                    MessageBox.Show("Vui lòng chọn dịch vụ muốn xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // TC_QLDV_09: Dịch vụ đã có chi tiết sử dụng trong bảng ChiTietDichVu
                bool daDuocSuDung = db.ChiTietDichVus.Any(ct => ct.MaDV == maDV);
                if (daDuocSuDung)
                {
                    MessageBox.Show("Không thể xóa dịch vụ này vì đã có chi tiết sử dụng dịch vụ trong dữ liệu khách hàng!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận xóa
                var confirmResult = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa dịch vụ '{txtTenDV.Text}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmResult == DialogResult.Yes)
                {
                    var dvXoa = db.DichVus.Find(maDV);
                    if (dvXoa != null)
                    {
                        db.DichVus.Remove(dvXoa);
                        db.SaveChanges();

                        // TC_QLDV_10: Xóa thành công
                        MessageBox.Show("Xóa dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadDataDichVu();
                        LamMoiForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoiForm();
            LoadDataDichVu();
            if (dgvDichVu != null) dgvDichVu.ClearSelection();
        }

        #endregion
    }
}