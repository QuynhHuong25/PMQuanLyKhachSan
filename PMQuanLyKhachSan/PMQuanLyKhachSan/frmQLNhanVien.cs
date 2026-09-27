using Microsoft.EntityFrameworkCore;
using PhanMemQuanLyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMQuanLyKhachSan
{
    public partial class frmQLNhanVien : Form
    {
        private readonly DataContext db = new DataContext();
        private NhanVien NhanVien;

        public frmQLNhanVien()
        {
            InitializeComponent();
        }

        public frmQLNhanVien(NhanVien nv)
        {
            InitializeComponent();
            NhanVien = nv;
        }

        private void frmQLNhanVien_Load(object sender, EventArgs e)
        {
            txtMaNV.ReadOnly = true;
            if (dgvNhanVien != null)
            {
                dgvNhanVien.ReadOnly = true;
                dgvNhanVien.AllowUserToAddRows = false;
                dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvNhanVien.MultiSelect = false;
            }

            LoadChucVuCombobox();
            LoadDataNhanVien();
            LamMoiForm();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            db.Dispose();
            base.OnFormClosed(e);
        }

        private void LoadChucVuCombobox()
        {
            try
            {
                var listChucVu = db.ChucVus
                    .Where(cv => !cv.TenChucVu.ToLower().Contains("quản lý")
                              && !cv.TenChucVu.ToLower().Contains("quan ly")
                              && !cv.TenChucVu.ToLower().Contains("lễ tân")
                              && !cv.TenChucVu.ToLower().Contains("le tan"))
                    .ToList();

                cboChucVu.DataSource = listChucVu;
                cboChucVu.DisplayMember = "TenChucVu";
                cboChucVu.ValueMember = "MaChucVu";
                cboChucVu.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải chức vụ: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataNhanVien()
        {
            dgvNhanVien.EnableHeadersVisualStyles = false;
            dgvNhanVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 55);
            dgvNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gold;
            dgvNhanVien.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvNhanVien.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvNhanVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvNhanVien.ColumnHeadersHeight = 35;
            dgvNhanVien.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
            dgvNhanVien.GridColor = Color.Gray;

            dgvNhanVien.RowsDefaultCellStyle.BackColor = Color.White;
            dgvNhanVien.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvNhanVien.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dgvNhanVien.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            try
            {
                var rawList = (from nv in db.NhanViens.Include(x => x.ChucVu)
                               join tk in db.TaiKhoans on nv.MaNV equals tk.MaNV into tkGroup
                               from tk in tkGroup.DefaultIfEmpty()
                               select new
                               {
                                   nv.MaNV,
                                   nv.HoTen,
                                   nv.NgaySinh,
                                   Giới_Tính = nv.GioiTinh ? "Nam" : "Nữ",
                                   nv.SDT,
                                   nv.Email,
                                   nv.MaChucVu,
                                   TenChucVu = nv.ChucVu != null ? nv.ChucVu.TenChucVu : "",
                                   TenDangNhap = tk != null ? tk.TenDangNhap : "",
                                   MatKhau = tk != null ? tk.MatKhau : ""
                               }).ToList();

                if (dgvNhanVien != null)
                {
                    dgvNhanVien.DataSource = rawList;

                    dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
                    dgvNhanVien.Columns["HoTen"].HeaderText = "Họ tên";
                    dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                    dgvNhanVien.Columns["Giới_Tính"].HeaderText = "Giới tính";
                    dgvNhanVien.Columns["SDT"].HeaderText = "SĐT";
                    dgvNhanVien.Columns["Email"].HeaderText = "Email";
                    dgvNhanVien.Columns["TenChucVu"].HeaderText = "Chức vụ";
                    dgvNhanVien.Columns["TenDangNhap"].HeaderText = "Tên đăng nhập";
                    dgvNhanVien.Columns["MatKhau"].HeaderText = "Mật khẩu";

                    if (dgvNhanVien.Columns["MaChucVu"] != null)
                        dgvNhanVien.Columns["MaChucVu"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách nhân viên: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LamMoiForm()
        {
            txtMaNV.Clear();
            txtHoTen.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            dtpNgaySinh.Value = DateTime.Now;

            rdNam.Checked = true;
            rdNu.Checked = false;

            cboChucVu.SelectedIndex = -1;
        }

        private bool ValidateInput()
        {
            string hoTen = txtHoTen.Text.Trim();
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                MessageBox.Show("Vui lòng nhập Họ tên nhân viên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            string patternHoTen = @"^[a-zA-Zàáảãạăắằẳẵặâấầẩẫậèéẻẽẹêếềểễệđìíỉĩịòóỏõọôốồổỗộơớờởỡợùúủũụưứừửữựỳýỷỹỵ\s]+$";
            if (!Regex.IsMatch(hoTen, patternHoTen))
            {
                MessageBox.Show("Họ tên chỉ được chứa chữ cái và không được chứa chữ số hoặc ký tự đặc biệt!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (dtpNgaySinh.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Ngày sinh không được vượt quá ngày hiện tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaySinh.Focus();
                return false;
            }

            int tuoi = DateTime.Now.Year - dtpNgaySinh.Value.Year;
            if (dtpNgaySinh.Value.Date > DateTime.Now.AddYears(-tuoi)) tuoi--;
            if (tuoi < 18)
            {
                MessageBox.Show("Nhân viên phải từ 18 tuổi trở lên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaySinh.Focus();
                return false;
            }

            string sdt = txtSDT.Text.Trim();
            if (string.IsNullOrWhiteSpace(sdt))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }
            if (!Regex.IsMatch(sdt, @"^0[0-9]{9}$"))
            {
                MessageBox.Show("Số điện thoại phải bao gồm đúng 10 chữ số và bắt đầu bằng số 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            string email = txtEmail.Text.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập Email!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Định dạng Email không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (cboChucVu.SelectedIndex == -1 || cboChucVu.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn chức vụ cho nhân viên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboChucVu.Focus();
                return false;
            }

            return true;
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                string sdt = txtSDT.Text.Trim();
                string email = txtEmail.Text.Trim();

                bool isDuplicate = db.NhanViens.Any(nv => nv.SDT == sdt || nv.Email == email);
                if (isDuplicate)
                {
                    MessageBox.Show("Số điện thoại hoặc Email này đã tồn tại trong hệ thống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var nvmoi = new NhanVien
                {
                    HoTen = txtHoTen.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value,
                    GioiTinh = rdNam.Checked,
                    SDT = sdt,
                    Email = email,
                    MaChucVu = Convert.ToInt32(cboChucVu.SelectedValue)
                };

                db.NhanViens.Add(nvmoi);
                db.SaveChanges();

                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataNhanVien();
                LamMoiForm();
                if (dgvNhanVien != null) dgvNhanVien.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaNV.Text, out int maNV))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa từ bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput()) return;

            if (MessageBox.Show("Bạn có thực sự muốn cập nhật thông tin nhân viên này không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                var nvSua = db.NhanViens.Find(maNV);
                if (nvSua != null)
                {
                    string sdt = txtSDT.Text.Trim();
                    string email = txtEmail.Text.Trim();

                    bool isDuplicate = db.NhanViens.Any(nv => nv.MaNV != maNV && (nv.SDT == sdt || nv.Email == email));
                    if (isDuplicate)
                    {
                        MessageBox.Show("Số điện thoại hoặc Email này đã thuộc về nhân viên khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    nvSua.HoTen = txtHoTen.Text.Trim();
                    nvSua.NgaySinh = dtpNgaySinh.Value;
                    nvSua.GioiTinh = rdNam.Checked;
                    nvSua.SDT = sdt;
                    nvSua.Email = email;
                    nvSua.MaChucVu = Convert.ToInt32(cboChucVu.SelectedValue);

                    db.SaveChanges();
                    MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataNhanVien();
                    LamMoiForm();
                    if (dgvNhanVien != null) dgvNhanVien.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi sửa dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtMaNV.Text, out int maNV))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên muốn xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    var nvXoa = db.NhanViens.Find(maNV);
                    if (nvXoa != null)
                    {
                        db.NhanViens.Remove(nvXoa);
                        db.SaveChanges();
                        MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataNhanVien();
                        LamMoiForm();
                        if (dgvNhanVien != null) dgvNhanVien.ClearSelection();
                    }
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Không thể xóa nhân viên này vì đang có dữ liệu liên quan liên kết với các bảng khác!", "Lỗi ràng buộc hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btLamMoi_Click(object sender, EventArgs e)
        {
            LamMoiForm();
            LoadDataNhanVien();
            if (dgvNhanVien != null) dgvNhanVien.ClearSelection();
        }

        private void btTim_Click(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtHoTen.Text.Trim();
                string sdt = txtSDT.Text.Trim();

                if (string.IsNullOrEmpty(tuKhoa) && string.IsNullOrEmpty(sdt))
                {
                    MessageBox.Show("Vui lòng nhập SĐT hoặc Họ tên để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtHoTen.Focus();
                    return;
                }

                var rawResult = (from nv in db.NhanViens.Include(x => x.ChucVu)
                                 join tk in db.TaiKhoans on nv.MaNV equals tk.MaNV into tkGroup
                                 from tk in tkGroup.DefaultIfEmpty()
                                 where (string.IsNullOrEmpty(tuKhoa) || nv.HoTen.ToLower().Contains(tuKhoa.ToLower()))
                                    && (string.IsNullOrEmpty(sdt) || nv.SDT.Contains(sdt))
                                 select new
                                 {
                                     nv.MaNV,
                                     nv.HoTen,
                                     nv.NgaySinh,
                                     Giới_Tính = nv.GioiTinh ? "Nam" : "Nữ",
                                     nv.SDT,
                                     nv.Email,
                                     nv.MaChucVu,
                                     TenChucVu = nv.ChucVu != null ? nv.ChucVu.TenChucVu : "",
                                     TenDangNhap = tk != null ? tk.TenDangNhap : "",
                                     MatKhau = tk != null ? tk.MatKhau : ""
                                 }).ToList();

                if (dgvNhanVien != null)
                {
                    dgvNhanVien.DataSource = rawResult;

                    if (rawResult.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy nhân viên nào phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            Control parentContainer = this.Parent;

            if (parentContainer != null)
            {
                frmTrangChu frm = new frmTrangChu(NhanVien);
                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Dock = DockStyle.Fill;

                parentContainer.Controls.Clear();
                parentContainer.Controls.Add(frm);
                parentContainer.Tag = frm;
                frm.BringToFront();
                frm.Show();

                this.Close();
            }
            else
            {
                frmTrangChu frm = new frmTrangChu(NhanVien);
                frm.Show();
                this.Close();
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvNhanVien != null)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

                txtMaNV.Text = row.Cells["MaNV"].Value?.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value?.ToString();
                txtSDT.Text = row.Cells["SDT"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();

                if (row.Cells["NgaySinh"].Value != null && row.Cells["NgaySinh"].Value != DBNull.Value)
                {
                    dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                }

                if (row.Cells["Giới_Tính"].Value != null)
                {
                    string genderStr = row.Cells["Giới_Tính"].Value.ToString();
                    if (genderStr == "Nam")
                    {
                        rdNam.Checked = true;
                    }
                    else
                    {
                        rdNu.Checked = true;
                    }
                }

                if (row.Cells["MaChucVu"].Value != null && row.Cells["MaChucVu"].Value != DBNull.Value)
                {
                    cboChucVu.SelectedValue = Convert.ToInt32(row.Cells["MaChucVu"].Value);
                }
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLichPhanCong_Click(object sender, EventArgs e)
        {
            // Sửa biến 'nhanVienDangNhap' thành 'NhanVien' trùng tên biến lưu trữ trong Form
            OpenChildForm(new frmQLLichPhanCong(NhanVien));
        }

        private void OpenChildForm(Form childForm)
        {
            Control parentContainer = this.Parent;

            if (parentContainer != null)
            {
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;

                parentContainer.Controls.Clear();
                parentContainer.Controls.Add(childForm);
                parentContainer.Tag = childForm;
                childForm.BringToFront();
                childForm.Show();

                this.Close();
            }
            else
            {
                childForm.Show();
                this.Close();
            }
        }
    }
}