using Microsoft.EntityFrameworkCore;
using PhanMemQuanLyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PMQuanLyKhachSan
{
    public partial class frmQLDatPhong : Form
    {
        public DataContext db = new DataContext();

        public static event Action TrangThaiPhongChanged;

        private NhanVien? nhanVienHienTai;
        private int maDatPhong;

        private readonly Color colGreenLight = Color.FromArgb(232, 245, 233);
        private readonly Color colGreenDark = Color.FromArgb(165, 214, 167);

        private readonly Color colRedLight = Color.FromArgb(255, 235, 238);
        private readonly Color colRedDark = Color.FromArgb(239, 154, 154);

        private readonly Color colBlueLight = Color.FromArgb(227, 242, 253);
        private readonly Color colBlueDark = Color.FromArgb(144, 202, 249);

        private readonly Color colYellowLight = Color.FromArgb(255, 248, 225);
        private readonly Color colYellowDark = Color.FromArgb(255, 224, 130);

        private readonly Color colOrangeLight = Color.FromArgb(255, 243, 224);
        private readonly Color colOrangeDark = Color.FromArgb(255, 204, 128);

        public frmQLDatPhong()
        {
            InitializeComponent();
        }

        public frmQLDatPhong(int maDP)
        {
            InitializeComponent();
            this.maDatPhong = maDP;
        }

        public frmQLDatPhong(NhanVien nv)
        {
            InitializeComponent();
            this.nhanVienHienTai = nv;
        }

        private void frmQLDatPhong_Load(object sender, EventArgs e)
        {
            dgvLichSu.CellFormatting += dgvLichSu_CellFormatting;
            dgvLichSu.CellPainting += dgvLichSu_CellPainting; 
            LoadDGV();
        }

        private void LoadDGV()
        {
            FormatGridStyles();

            db.Dispose();
            db = new DataContext();

            var listDatPhongAll = db.DatPhongs
                .AsNoTracking()
                .Include(x => x.Phong)
                .ThenInclude(p => p!.LoaiPhong)
                .ToList();

            CapNhatThongKe(listDatPhongAll);

            var listDeHienThi = maDatPhong > 0
                ? listDatPhongAll.Where(x => x.MaDatPhong == maDatPhong).ToList()
                : listDatPhongAll;

            HienThiDanhSach(listDeHienThi);
        }

        // ================= TÍNH TIỀN PHÒNG =================
        private decimal CalculateTienPhong(DatPhong? dp)
        {
            if (dp == null) return 0m;

            var phong = db.Phongs.AsNoTracking().Include(p => p.LoaiPhong).FirstOrDefault(p => p.MaPhong == dp.MaPhong);
            decimal giaPhong = phong?.LoaiPhong?.Gia ?? 0m;

            DateTime nn = dp.NgayNhan != default ? dp.NgayNhan : DateTime.Today;
            DateTime nt = dp.NgayTra != default ? dp.NgayTra : DateTime.Today;

            int soDem = (nt - nn).Days;
            if (soDem <= 0) soDem = 1;

            return giaPhong * soDem;
        }

        // ================= 1. CẬP NHẬT THỐNG KÊ =================
        private void CapNhatThongKe(List<DatPhong> listDP)
        {
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);

            int checkInTodayDaTT = listDP.Count(x => x.NgayNhan.Date == today && x.TrangThai == "Đã thanh toán");
            int checkInTodayChoTT = listDP.Count(x => x.NgayNhan.Date == today && (x.TrangThai == "Đã đặt" || x.TrangThai == "Chờ thanh toán" || x.TrangThai == "Chờ nhận"));

            int checkInTomorrowDaTT = listDP.Count(x => x.NgayNhan.Date == tomorrow && x.TrangThai == "Đã thanh toán");
            int checkInTomorrowChoTT = listDP.Count(x => x.NgayNhan.Date == tomorrow && (x.TrangThai == "Đã đặt" || x.TrangThai == "Chờ thanh toán" || x.TrangThai == "Chờ nhận"));

            int khachDuoi24h = listDP
                .Where(x => x.NgayNhan.Date == today && (x.TrangThai == "Đã đặt" || x.TrangThai == "Chờ thanh toán"))
                .Select(x => x.MaKH)
                .Distinct()
                .Count();

            int phongCanChuanBi = listDP.Count(x => x.NgayNhan.Date == tomorrow && x.TrangThai != "Đã hủy" && x.TrangThai != "Đã trả phòng");
            int sapCheckOut = listDP.Count(x => x.NgayTra.Date == today && x.TrangThai == "Đang thuê");

            if (lblDaTTHN != null) lblDaTTHN.Text = checkInTodayDaTT.ToString();
            if (lblChuaTTHN != null) lblChuaTTHN.Text = checkInTodayChoTT.ToString();

            if (lblDaTTNM != null) lblDaTTNM.Text = checkInTomorrowDaTT.ToString();
            if (lblChuaTTNM != null) lblChuaTTNM.Text = checkInTomorrowChoTT.ToString();

            if (lblDenHanTT != null) lblDenHanTT.Text = khachDuoi24h.ToString();
            if (lblCanCB != null) lblCanCB.Text = phongCanChuanBi.ToString();
            if (lblSapOut != null) lblSapOut.Text = sapCheckOut.ToString();
        }

        // ================= 2. HIỂN THỊ DANH SÁCH =================
        private void HienThiDanhSach(List<DatPhong> listDatPhong)
        {
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);

            var maDatPhongIds = listDatPhong.Select(x => x.MaDatPhong).ToList();

            var dsChiTietDV = db.ChiTietDichVus
                .AsNoTracking()
                .Include(x => x.DichVu)
                .Where(ct => maDatPhongIds.Contains(ct.MaDatPhong))
                .ToList();

            var dsHoaDon = db.HoaDons
                .AsNoTracking()
                .Where(hd => maDatPhongIds.Contains(hd.MaDatPhong))
                .ToList();

            var listSorted = listDatPhong.OrderBy(x =>
            {
                string tt = x.TrangThai ?? "";
                if (tt == "Đã hủy" || tt == "Đã trả phòng") return 10;

                DateTime nn = x.NgayNhan != default ? x.NgayNhan.Date : DateTime.MinValue;
                DateTime nt = x.NgayTra != default ? x.NgayTra.Date : DateTime.MinValue;

                if (nn == today && (tt == "Đã thanh toán" || tt == "Đã đặt" || tt == "Chờ thanh toán" || tt == "Chờ nhận"))
                {
                    bool daTT = (tt == "Đã thanh toán");
                    return daTT ? 2 : 1;
                }

                if (nt == today && tt == "Đang thuê") return 3;

                if (nn == tomorrow && (tt == "Đã thanh toán" || tt == "Đã đặt" || tt == "Chờ thanh toán" || tt == "Chờ nhận"))
                {
                    bool daTT = (tt == "Đã thanh toán");
                    return daTT ? 5 : 4;
                }

                return 6;
            }).ThenByDescending(x => x.NgayDat).ToList();

            var dsHienThi = listSorted.Select(x =>
            {
                DateTime nn = x.NgayNhan != default ? x.NgayNhan : DateTime.Today;
                DateTime nt = x.NgayTra != default ? x.NgayTra : DateTime.Today;

                int soDem = (nt - nn).Days;
                if (soDem <= 0) soDem = 1;

                decimal giaPhong = x.Phong?.LoaiPhong?.Gia ?? 0m;
                decimal tienPhong = giaPhong * soDem;

                var listDV = dsChiTietDV.Where(ct => ct.MaDatPhong == x.MaDatPhong).ToList();
                string tenDichVu = listDV.Count > 0
                    ? string.Join(", ", listDV.Select(ct => ct.DichVu?.TenDV ?? "").Where(t => !string.IsNullOrEmpty(t)))
                    : "Không có";

                decimal tienDichVu = listDV.Sum(ct => (decimal?)ct.ThanhTien) ?? 0m;
                decimal tongTien = tienPhong + tienDichVu;

                var hoaDon = dsHoaDon.FirstOrDefault(hd => hd.MaDatPhong == x.MaDatPhong);
                string pttt = "";

                if (x.TrangThai != "Đã đặt" && x.TrangThai != "Đã hủy" && hoaDon != null)
                {
                    pttt = hoaDon.PhuongThucTT ?? "";
                }

                return new
                {
                    MaDatPhong = x.MaDatPhong,
                    Phong = x.Phong?.TenPhong ?? "N/A",
                    LoaiPhong = x.Phong?.LoaiPhong?.TenLoai ?? "N/A",
                    NgayDat = x.NgayDat,
                    NgayNhan = nn,
                    NgayTra = nt,
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
            dgvLichSu.ClearSelection();
        }

        private void dgvLichSu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvLichSu.Rows.Count) return;

            var row = dgvLichSu.Rows[e.RowIndex];
            if (row.Cells["NgayNhan"]?.Value == null || row.Cells["NgayTra"]?.Value == null) return;

            string trangThai = row.Cells["TrangThai"]?.Value?.ToString() ?? "";
            bool isEvenRow = (e.RowIndex % 2 == 0);

            Color targetColor = Color.White;

            if (trangThai == "Đã hủy" || trangThai == "Đã trả phòng")
            {
                targetColor = isEvenRow ? Color.White : Color.FromArgb(248, 249, 250);
                row.DefaultCellStyle.ForeColor = (trangThai == "Đã hủy") ? Color.Gray : Color.DimGray;
            }
            else
            {
                DateTime ngayNhan = Convert.ToDateTime(row.Cells["NgayNhan"].Value).Date;
                DateTime ngayTra = Convert.ToDateTime(row.Cells["NgayTra"].Value).Date;

                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(1);

                if (ngayNhan == today && (trangThai == "Đã thanh toán" || trangThai == "Đã đặt" || trangThai == "Chờ thanh toán" || trangThai == "Chờ nhận"))
                {
                    targetColor = (trangThai == "Đã thanh toán")
                        ? (isEvenRow ? colGreenLight : colGreenDark)
                        : (isEvenRow ? colRedLight : colRedDark);
                }
                else if (ngayTra == today && trangThai == "Đang thuê")
                {
                    targetColor = isEvenRow ? colBlueLight : colBlueDark;
                }
                else if (ngayNhan == tomorrow && (trangThai == "Đã thanh toán" || trangThai == "Đã đặt" || trangThai == "Chờ thanh toán" || trangThai == "Chờ nhận"))
                {
                    targetColor = (trangThai == "Đã thanh toán")
                        ? (isEvenRow ? colYellowLight : colYellowDark)
                        : (isEvenRow ? colOrangeLight : colOrangeDark);
                }
                else
                {
                    targetColor = isEvenRow ? Color.White : Color.FromArgb(245, 245, 245);
                }

                row.DefaultCellStyle.ForeColor = Color.Black;
            }

            row.DefaultCellStyle.BackColor = targetColor;
            row.DefaultCellStyle.SelectionBackColor = targetColor;
            row.DefaultCellStyle.SelectionForeColor = row.DefaultCellStyle.ForeColor;
        }

        private void dgvLichSu_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string colName = dgvLichSu.Columns[e.ColumnIndex].Name;

                if (colName == "btnThanhToan" || colName == "btnNhanPhong" || colName == "btnTraPhong" || colName == "btnHuy")
                {
                    string tt = dgvLichSu.Rows[e.RowIndex].Cells["TrangThai"]?.Value?.ToString() ?? "";

                    bool showButton = false;
                    if (colName == "btnThanhToan" && (tt == "Đã đặt" || tt == "Chờ thanh toán")) showButton = true;
                    if (colName == "btnNhanPhong" && (tt == "Đã thanh toán" || tt == "Chờ nhận")) showButton = true;
                    if (colName == "btnTraPhong" && tt == "Đang thuê") showButton = true;
                    if (colName == "btnHuy" && (tt == "Chờ nhận" || tt == "Đã thanh toán" || tt == "Đã đặt" || tt == "Chờ thanh toán")) showButton = true;

                    if (!showButton)
                    {
                        e.PaintBackground(e.CellBounds, true);
                        e.Handled = true;
                    }
                }
            }
        }

        private void FormatGridStyles()
        {
            dgvLichSu.RowHeadersVisible = false;
            dgvLichSu.EnableHeadersVisualStyles = false;

            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.FromArgb(212, 175, 55),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            dgvLichSu.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvLichSu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvLichSu.ColumnHeadersHeight = 40;

            dgvLichSu.GridColor = Color.LightGray;
            dgvLichSu.RowsDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgvLichSu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
            dgvLichSu.Columns["PhuongThucTT"].HeaderText = "Phương thức TT";

            if (dgvLichSu.Columns["btnThanhToan"] == null)
            {
                DataGridViewButtonColumn colTT = new DataGridViewButtonColumn
                {
                    Name = "btnThanhToan",
                    HeaderText = "Thanh toán",
                    Text = "Thanh toán",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Flat
                };
                dgvLichSu.Columns.Add(colTT);
            }

            if (dgvLichSu.Columns["btnNhanPhong"] == null)
            {
                DataGridViewButtonColumn colNhan = new DataGridViewButtonColumn
                {
                    Name = "btnNhanPhong",
                    HeaderText = "Nhận phòng",
                    Text = "Nhận phòng",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Flat
                };
                dgvLichSu.Columns.Add(colNhan);
            }

            if (dgvLichSu.Columns["btnTraPhong"] == null)
            {
                DataGridViewButtonColumn colTra = new DataGridViewButtonColumn
                {
                    Name = "btnTraPhong",
                    HeaderText = "Trả phòng",
                    Text = "Trả phòng",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Flat
                };
                dgvLichSu.Columns.Add(colTra);
            }

            if (dgvLichSu.Columns["btnHuy"] == null)
            {
                DataGridViewButtonColumn colHuy = new DataGridViewButtonColumn
                {
                    Name = "btnHuy",
                    HeaderText = "Hủy đặt",
                    Text = "Hủy phòng",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Flat
                };
                dgvLichSu.Columns.Add(colHuy);
            }

            foreach (DataGridViewColumn col in dgvLichSu.Columns)
            {
                col.HeaderCell.Style.BackColor = Color.FromArgb(15, 23, 42);
                col.HeaderCell.Style.ForeColor = Color.FromArgb(212, 175, 55);
            }

            dgvLichSu.Columns["NgayDat"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvLichSu.Columns["NgayNhan"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvLichSu.Columns["NgayTra"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvLichSu.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            dgvLichSu.Columns["TongTien"].DefaultCellStyle.Format = "N0";

            dgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLichSu.Columns["MaDatPhong"].FillWeight = 45;
            dgvLichSu.Columns["Phong"].FillWeight = 60;
            dgvLichSu.Columns["LoaiPhong"].FillWeight = 85;
            dgvLichSu.Columns["NgayDat"].FillWeight = 70;
            dgvLichSu.Columns["NgayNhan"].FillWeight = 70;
            dgvLichSu.Columns["NgayTra"].FillWeight = 70;
            dgvLichSu.Columns["SoDem"].FillWeight = 40;
            dgvLichSu.Columns["DonGia"].FillWeight = 70;
            dgvLichSu.Columns["DichVu"].FillWeight = 100;
            dgvLichSu.Columns["TongTien"].FillWeight = 80;
            dgvLichSu.Columns["TrangThai"].FillWeight = 75;
            dgvLichSu.Columns["PhuongThucTT"].FillWeight = 80;

            dgvLichSu.Columns["btnThanhToan"].FillWeight = 70;
            dgvLichSu.Columns["btnNhanPhong"].FillWeight = 70;
            dgvLichSu.Columns["btnTraPhong"].FillWeight = 70;
            dgvLichSu.Columns["btnHuy"].FillWeight = 65;
        }

        private void dgvLichSu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvLichSu.Rows.Count) return;

            string colName = dgvLichSu.Columns[e.ColumnIndex].Name;
            var cellValue = dgvLichSu.Rows[e.RowIndex].Cells["MaDatPhong"]?.Value;
            var trangThaiVal = dgvLichSu.Rows[e.RowIndex].Cells["TrangThai"]?.Value?.ToString() ?? "";

            if (cellValue == null || !int.TryParse(cellValue.ToString(), out int maDP)) return;

            if (colName == "btnThanhToan")
            {
                if (trangThaiVal == "Đã đặt" || trangThaiVal == "Chờ thanh toán")
                {
                    DialogResult result = MessageBox.Show($"Xác nhận đã thu tiền/tiền cọc cho mã đặt {maDP}?", "Xác nhận Thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        var dp = db.DatPhongs.FirstOrDefault(x => x.MaDatPhong == maDP);
                        if (dp != null)
                        {
                            dp.TrangThai = "Đã thanh toán";
                            db.SaveChanges();

                            MessageBox.Show("Xác nhận thanh toán thành công! Giờ bạn có thể bấm 'Nhận phòng'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDGV();

                            TrangThaiPhongChanged?.Invoke();
                        }
                    }
                }
                return;
            }

            if (colName == "btnNhanPhong")
            {
                if (trangThaiVal == "Đã thanh toán" || trangThaiVal == "Chờ nhận")
                {
                    DialogResult result = MessageBox.Show($"Xác nhận làm thủ tục NHẬN PHÒNG cho mã đặt {maDP}?", "Xác nhận Check-in", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        var dp = db.DatPhongs.FirstOrDefault(x => x.MaDatPhong == maDP);
                        if (dp != null)
                        {
                            dp.TrangThai = "Đang thuê";

                            var phong = db.Phongs.FirstOrDefault(p => p.MaPhong == dp.MaPhong);
                            if (phong != null)
                            {
                                phong.TrangThai = "Đang thuê";
                            }

                            db.SaveChanges();
                            MessageBox.Show("Khách đã nhận phòng thành công! Trạng thái phòng chuyển sang 'Đang thuê'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDGV();

                            TrangThaiPhongChanged?.Invoke();
                        }
                    }
                }
                return;
            }

            if (colName == "btnTraPhong")
            {
                if (trangThaiVal == "Đang thuê")
                {
                    var dp = db.DatPhongs.FirstOrDefault(x => x.MaDatPhong == maDP);
                    var hoaDon = db.HoaDons.AsNoTracking().FirstOrDefault(x => x.MaDatPhong == maDP);

                    decimal tienPhong = CalculateTienPhong(dp);
                    decimal tienDV = db.ChiTietDichVus.AsNoTracking().Where(x => x.MaDatPhong == maDP).Sum(x => (decimal?)x.ThanhTien) ?? 0m;
                    decimal tongTienHienTai = tienPhong + tienDV;

                    decimal tienDaTra = hoaDon?.TongTien ?? 0m;
                    decimal conNo = tongTienHienTai - tienDaTra;

                    if (conNo > 0)
                    {
                        MessageBox.Show($"Khách còn {conNo:N0} đ chưa thanh toán (tiền phát sinh/dịch vụ). Vui lòng thu tiền trước khi trả phòng!",
                                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        frmHoaDonKhach frm = new frmHoaDonKhach(maDP);
                        frm.ShowDialog();

                        LoadDGV();
                        return;
                    }

                    DialogResult result = MessageBox.Show($"Xác nhận TRẢ PHÒNG cho mã đặt {maDP}?", "Xác nhận Check-out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        if (dp != null)
                        {
                            dp.TrangThai = "Đã trả phòng";

                            var phong = db.Phongs.FirstOrDefault(p => p.MaPhong == dp.MaPhong);
                            if (phong != null)
                            {
                                phong.TrangThai = "Trống";
                            }

                            db.SaveChanges();
                            MessageBox.Show("Đã trả phòng thành công! Phòng đã chuyển sang trạng thái TRỐNG.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDGV();

                            TrangThaiPhongChanged?.Invoke();
                        }
                    }
                }
                return;
            }

            if (colName == "btnHuy")
            {
                if (trangThaiVal == "Chờ nhận" || trangThaiVal == "Đã thanh toán" || trangThaiVal == "Đã đặt" || trangThaiVal == "Chờ thanh toán")
                {
                    DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn HỦY đơn đặt phòng mã {maDP}?", "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        var dp = db.DatPhongs.FirstOrDefault(x => x.MaDatPhong == maDP);
                        if (dp != null)
                        {
                            dp.TrangThai = "Đã hủy";

                            var phong = db.Phongs.FirstOrDefault(p => p.MaPhong == dp.MaPhong);
                            if (phong != null)
                            {
                                phong.TrangThai = "Trống";
                            }

                            db.SaveChanges();
                            MessageBox.Show("Đã hủy đơn đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDGV();

                            TrangThaiPhongChanged?.Invoke();
                        }
                    }
                }
                return;
            }

            frmHoaDonKhach frmDetail = new frmHoaDonKhach(maDP);

            if (trangThaiVal == "Đã thanh toán" || trangThaiVal == "Đang thuê" || trangThaiVal == "Đang ở" || trangThaiVal == "Đã trả phòng" || trangThaiVal == "Đã hủy")
            {
                Control[] btns = frmDetail.Controls.Find("btnThanhToan", true);
                if (btns.Length > 0 && btns[0] is Button btn)
                {
                    btn.Enabled = false;
                }
            }

            frmDetail.StartPosition = FormStartPosition.CenterScreen;
            frmDetail.ShowDialog();
        }
    }
}