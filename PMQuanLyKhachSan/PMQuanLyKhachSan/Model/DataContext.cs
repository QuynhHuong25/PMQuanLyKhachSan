using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyKhachSan.Model
{
    public class DataContext : DbContext
    {
        public DbSet<ChucVu> ChucVus { get; set; }

        public DbSet<TaiKhoan> TaiKhoans { get; set; }

        public DbSet<NhanVien> NhanViens { get; set; }

        public DbSet<LichPhanCong> LichPhanCongs { get; set; }

        public DbSet<LoaiPhong> LoaiPhongs { get; set; }

        public DbSet<Phong> Phongs { get; set; }

        public DbSet<KhachHang> KhachHangs { get; set; }

        public DbSet<DatPhong> DatPhongs { get; set; }

        public DbSet<DichVu> DichVus { get; set; }

        public DbSet<ChiTietDichVu> ChiTietDichVus { get; set; }

        public DbSet<HoaDon> HoaDons { get; set; }

        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;AttachDbFilename=|DataDirectory|\PMQuanLyKhachSan.mdf;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //====================== CHUC VU ======================
            modelBuilder.Entity<ChucVu>().HasData(
            new ChucVu { MaChucVu = 1, TenChucVu = "Quản lý" },
            new ChucVu { MaChucVu = 2, TenChucVu = "Lễ tân" },
            new ChucVu { MaChucVu = 3, TenChucVu = "Lao công" },
            new ChucVu { MaChucVu = 4, TenChucVu = "Đầu bếp" },
            new ChucVu { MaChucVu = 5, TenChucVu = "Phục vụ nhà hàng" },
            new ChucVu { MaChucVu = 6, TenChucVu = "Bảo vệ" }
            );

            //====================== NHAN VIEN ======================
            modelBuilder.Entity<NhanVien>().HasData(
            new NhanVien { MaNV = 1, MaTK = 1, HoTen = "Lương Thị Quỳnh Hương", NgaySinh = new DateTime(2005, 10, 25), GioiTinh = false, SDT = "0976111111", Email = "huong@gmail.com", MaChucVu = 1 },
            new NhanVien { MaNV = 2, MaTK = 2, HoTen = "Trịnh Thị Hay", NgaySinh = new DateTime(2005, 10, 17), GioiTinh = false, SDT = "0982222222", Email = "hay@gmail.com", MaChucVu = 1 },
            new NhanVien { MaNV = 3, MaTK = 3, HoTen = "Nguyễn Thị Diễm", NgaySinh = new DateTime(2005, 12, 11), GioiTinh = false, SDT = "0983333333", Email = "diem@gmail.com", MaChucVu = 1 },
            new NhanVien { MaNV = 4, MaTK = 4, HoTen = "Nguyễn Văn An", NgaySinh = new DateTime(1999, 1, 15), GioiTinh = true, SDT = "0984444444", Email = "an@gmail.com", MaChucVu = 2 },
            new NhanVien { MaNV = 5, MaTK = 5, HoTen = "Phạm Thị Mai", NgaySinh = new DateTime(2000, 3, 8), GioiTinh = false, SDT = "0985555555", Email = "mai@gmail.com", MaChucVu = 2 },
            new NhanVien { MaNV = 6, MaTK = 6, HoTen = "Lê Văn Tùng", NgaySinh = new DateTime(1998, 7, 18), GioiTinh = true, SDT = "0986666666", Email = "tung@gmail.com", MaChucVu = 3 },
            new NhanVien { MaNV = 7, MaTK = 7, HoTen = "Đỗ Thị Lan", NgaySinh = new DateTime(1997, 11, 11), GioiTinh = false, SDT = "0987777777", Email = "lan@gmail.com", MaChucVu = 4 },
            new NhanVien { MaNV = 8, MaTK = 8, HoTen = "Hoàng Văn Bình", NgaySinh = new DateTime(1996, 6, 6), GioiTinh = true, SDT = "0988888888", Email = "binh@gmail.com", MaChucVu = 5 },
            new NhanVien { MaNV = 9, MaTK = 9, HoTen = "Ngô Thị Thu", NgaySinh = new DateTime(1995, 12, 5), GioiTinh = false, SDT = "0989999999", Email = "thu@gmail.com", MaChucVu = 6 },
            new NhanVien { MaNV = 10, MaTK = 10, HoTen = "Trần Văn Hùng", NgaySinh = new DateTime(1994, 4, 30), GioiTinh = true, SDT = "0971111111", Email = "hung@gmail.com", MaChucVu = 5 }
            );

            //====================== TAI KHOAN ======================
            modelBuilder.Entity<TaiKhoan>().HasData(
            new TaiKhoan { MaTK = 1, TenDangNhap = "huong", MatKhau = "123456", MaNV = 1 },
            new TaiKhoan { MaTK = 2, TenDangNhap = "hay", MatKhau = "123456", MaNV = 2 },
            new TaiKhoan { MaTK = 3, TenDangNhap = "diem", MatKhau = "123456", MaNV = 3 },
            new TaiKhoan { MaTK = 4, TenDangNhap = "an", MatKhau = "123456", MaNV = 4 },
            new TaiKhoan { MaTK = 5, TenDangNhap = "mai", MatKhau = "123456", MaNV = 5 }
            );

            //====================== LOAI PHONG ======================
            modelBuilder.Entity<LoaiPhong>().HasData(
            new LoaiPhong { MaLoai = 1, TenLoai = "Standard 1 giường đôi", Gia = 3000000, MoTa = "Phòng phù hợp cho 2 người lớn và 1 trẻ em" },
            new LoaiPhong { MaLoai = 2, TenLoai = "Standard 2 giường đơn", Gia = 3500000, MoTa = "Phòng phù hợp cho 2 người lớn" },
            new LoaiPhong { MaLoai = 3, TenLoai = "Deluxe 1 giường đôi", Gia = 5000000, MoTa = "Phòng phù hợp cho 2 người lớn và 2 trẻ em" },
            new LoaiPhong { MaLoai = 4, TenLoai = "Deluxe 2 giường đôi", Gia = 7000000, MoTa = "Phòng phù hợp cho 4 người lớn" },
            new LoaiPhong { MaLoai = 5, TenLoai = "Suite", Gia = 8000000, MoTa = "Phòng cao cấp phù hợp cho 2 người lớn và 2 trẻ em" }
            );

            //====================== PHONG ======================
            modelBuilder.Entity<Phong>().HasData(
            new Phong { MaPhong = 101, TenPhong = "P101", MaLoai = 1, TrangThai = "Trống" },
            new Phong { MaPhong = 102, TenPhong = "P102", MaLoai = 1, TrangThai = "Trống" },
            new Phong { MaPhong = 201, TenPhong = "P201", MaLoai = 2, TrangThai = "Trống" },
            new Phong { MaPhong = 202, TenPhong = "P202", MaLoai = 3, TrangThai = "Trống" },
            new Phong { MaPhong = 301, TenPhong = "P301", MaLoai = 4, TrangThai = "Trống" },
            new Phong { MaPhong = 302, TenPhong = "P302", MaLoai = 5, TrangThai = "Trống" },
            new Phong { MaPhong = 303, TenPhong = "P303", MaLoai = 2, TrangThai = "Trống" },
            new Phong { MaPhong = 304, TenPhong = "P304", MaLoai = 3, TrangThai = "Trống" },
            new Phong { MaPhong = 305, TenPhong = "P305", MaLoai = 4, TrangThai = "Trống" },
            new Phong { MaPhong = 306, TenPhong = "P306", MaLoai = 5, TrangThai = "Trống" }
            );

            //====================== KHACH HANG ======================
            modelBuilder.Entity<KhachHang>().HasData(
            new KhachHang { MaKH = 1, HoTen = "Nguyễn Minh Anh", NgaySinh = new DateTime(2001, 5, 12), GioiTinh = true, CCCD = "00111111", SDT = "0901111111", TenDangNhap = "anh", MatKhau = "123456789" },
            new KhachHang { MaKH = 2, HoTen = "Trần Quốc Bảo", NgaySinh = new DateTime(2000, 8, 20), GioiTinh = true, CCCD = "00222222", SDT = "0902222222", TenDangNhap = "bao", MatKhau = "123456789" },
            new KhachHang { MaKH = 3, HoTen = "Lê Thanh Bình", NgaySinh = new DateTime(2002, 3, 15), GioiTinh = true, CCCD = "00333333", SDT = "0903333333", TenDangNhap = "binh", MatKhau = "123456789" },
            new KhachHang { MaKH = 4, HoTen = "Phạm Gia Hân", NgaySinh = new DateTime(2003, 11, 5), GioiTinh = false, CCCD = "00444444", SDT = "0904444444", TenDangNhap = "han", MatKhau = "123456789" },
            new KhachHang { MaKH = 5, HoTen = "Đỗ Thu Hà", NgaySinh = new DateTime(2001, 9, 18), GioiTinh = false, CCCD = "00555555", SDT = "0905555555", TenDangNhap = "ha", MatKhau = "123456789" },
            new KhachHang { MaKH = 6, HoTen = "Nguyễn Văn Nam", NgaySinh = new DateTime(1999, 12, 10), GioiTinh = true, CCCD = "00666666", SDT = "0906666666", TenDangNhap = "nam", MatKhau = "123456789" },
            new KhachHang { MaKH = 7, HoTen = "Bùi Thị Ngọc", NgaySinh = new DateTime(2002, 1, 28), GioiTinh = false, CCCD = "00777777", SDT = "0907777777", TenDangNhap = "ngoc", MatKhau = "123456789" },
            new KhachHang { MaKH = 8, HoTen = "Hoàng Đức Long", NgaySinh = new DateTime(2000, 6, 14), GioiTinh = true, CCCD = "00888888", SDT = "0908888888", TenDangNhap = "long", MatKhau = "123456789" },
            new KhachHang { MaKH = 9, HoTen = "Nguyễn Thị Hồng", NgaySinh = new DateTime(2001, 4, 7), GioiTinh = false, CCCD = "00999999", SDT = "0909999999", TenDangNhap = "hong", MatKhau = "123456789" },
            new KhachHang { MaKH = 10, HoTen = "Vũ Mạnh Cường", NgaySinh = new DateTime(1998, 10, 25), GioiTinh = true, CCCD = "01010101", SDT = "0910101010", TenDangNhap = "cuong", MatKhau = "123456789" }
            );

            //====================== LỊCH PHÂN CÔNG ======================
            modelBuilder.Entity<LichPhanCong>().HasData(
            new LichPhanCong { MaLich = 1, MaNV = 1, NgayLam = new DateTime(2026, 7, 12), CaLam = "Sáng", GioBatDau = new TimeSpan(7, 0, 0), GioKetThuc = new TimeSpan(11, 30, 0), GhiChu = "" },
            new LichPhanCong { MaLich = 2, MaNV = 2, NgayLam = new DateTime(2026, 7, 12), CaLam = "Chiều", GioBatDau = new TimeSpan(13, 0, 0), GioKetThuc = new TimeSpan(17, 0, 0), GhiChu = "" },
            new LichPhanCong { MaLich = 3, MaNV = 3, NgayLam = new DateTime(2026, 7, 12), CaLam = "Tối", GioBatDau = new TimeSpan(17, 30, 0), GioKetThuc = new TimeSpan(22, 0, 0), GhiChu = "" },
            new LichPhanCong { MaLich = 4, MaNV = 4, NgayLam = new DateTime(2026, 7, 12), CaLam = "Sáng", GioBatDau = new TimeSpan(7, 0, 0), GioKetThuc = new TimeSpan(11, 30, 0), GhiChu = "" },
            new LichPhanCong { MaLich = 5, MaNV = 5, NgayLam = new DateTime(2026, 7, 12), CaLam = "Chiều", GioBatDau = new TimeSpan(13, 0, 0), GioKetThuc = new TimeSpan(17, 0, 0), GhiChu = "" },
            new LichPhanCong { MaLich = 6, MaNV = 6, NgayLam = new DateTime(2026, 7, 12), CaLam = "Tối", GioBatDau = new TimeSpan(17, 30, 0), GioKetThuc = new TimeSpan(22, 0, 0), GhiChu = "" },
            new LichPhanCong { MaLich = 7, MaNV = 7, NgayLam = new DateTime(2026, 7, 12), CaLam = "Sáng", GioBatDau = new TimeSpan(7, 0, 0), GioKetThuc = new TimeSpan(11, 30, 0), GhiChu = "" },
            new LichPhanCong { MaLich = 8, MaNV = 8, NgayLam = new DateTime(2026, 7, 12), CaLam = "Chiều", GioBatDau = new TimeSpan(13, 0, 0), GioKetThuc = new TimeSpan(17, 0, 0), GhiChu = "" },
            new LichPhanCong { MaLich = 9, MaNV = 9, NgayLam = new DateTime(2026, 7, 12), CaLam = "Tối", GioBatDau = new TimeSpan(17, 30, 0), GioKetThuc = new TimeSpan(22, 0, 0), GhiChu = "" },
            new LichPhanCong { MaLich = 10, MaNV = 10, NgayLam = new DateTime(2026, 7, 12), CaLam = "Sáng", GioBatDau = new TimeSpan(7, 0, 0), GioKetThuc = new TimeSpan(11, 30, 0), GhiChu = "" }
            );

            //====================== ĐẶT PHÒNG ======================
            modelBuilder.Entity<DatPhong>().HasData(
            new DatPhong { MaDatPhong = 1, MaKH = 1, MaPhong = 102, NgayDat = new DateTime(2026, 7, 1), NgayNhan = new DateTime(2026, 7, 12), NgayTra = new DateTime(2026, 7, 14), TrangThai = "Đã đặt" },
            new DatPhong { MaDatPhong = 2, MaKH = 2, MaPhong = 202, NgayDat = new DateTime(2026, 7, 2), NgayNhan = new DateTime(2026, 7, 12), NgayTra = new DateTime(2026, 7, 15), TrangThai = "Đang ở" },
            new DatPhong { MaDatPhong = 3, MaKH = 3, MaPhong = 301, NgayDat = new DateTime(2026, 7, 3), NgayNhan = new DateTime(2026, 7, 20), NgayTra = new DateTime(2026, 7, 22), TrangThai = "Đã thanh toán" },
            new DatPhong { MaDatPhong = 4, MaKH = 4, MaPhong = 303, NgayDat = new DateTime(2026, 7, 5), NgayNhan = new DateTime(2026, 7, 12), NgayTra = new DateTime(2026, 7, 13), TrangThai = "Đã hủy" },
            new DatPhong { MaDatPhong = 5, MaKH = 5, MaPhong = 305, NgayDat = new DateTime(2026, 7, 8), NgayNhan = new DateTime(2026, 7, 18), NgayTra = new DateTime(2026, 7, 20), TrangThai = "Đã trả phòng" }
            );

            //====================== DỊCH VỤ ======================
            modelBuilder.Entity<DichVu>().HasData(
            new DichVu { MaDV = 1, TenDV = "Ăn uống", Gia = 250000 },
            new DichVu { MaDV = 2, TenDV = "Giặt ủi", Gia = 50000 },
            new DichVu { MaDV = 3, TenDV = "Thuê xe máy", Gia = 200000 },
            new DichVu { MaDV = 4, TenDV = "Đưa đón sân bay", Gia = 300000 },
            new DichVu { MaDV = 5, TenDV = "Spa", Gia = 400000 }
            );

            //====================== CHI TIẾT DỊCH VỤ ======================
            modelBuilder.Entity<ChiTietDichVu>().HasData(
            new ChiTietDichVu { MaCTDV = 1, MaDatPhong = 1, MaDV = 1, SoLuong = 2, DonGia = 100000, ThanhTien = 200000 },
            new ChiTietDichVu { MaCTDV = 2, MaDatPhong = 1, MaDV = 2, SoLuong = 3, DonGia = 50000, ThanhTien = 150000 },
            new ChiTietDichVu { MaCTDV = 3, MaDatPhong = 2, MaDV = 4, SoLuong = 1, DonGia = 300000, ThanhTien = 300000 },
            new ChiTietDichVu { MaCTDV = 4, MaDatPhong = 2, MaDV = 5, SoLuong = 1, DonGia = 400000, ThanhTien = 400000 },
            new ChiTietDichVu { MaCTDV = 5, MaDatPhong = 4, MaDV = 3, SoLuong = 2, DonGia = 200000, ThanhTien = 400000 }
            );

            //====================== HÓA ĐƠN ======================
            modelBuilder.Entity<HoaDon>().HasData(
            new HoaDon { MaHD = 1, MaDatPhong = 1, TongTien = 1350000, NgayLap = new DateTime(2026, 7, 14), PhuongThucTT = "Tiền mặt" },
            new HoaDon { MaHD = 2, MaDatPhong = 2, TongTien = 2500000, NgayLap = new DateTime(2026, 7, 15), PhuongThucTT = "Chuyển khoản" },
            new HoaDon { MaHD = 3, MaDatPhong = 3, TongTien = 1200000, NgayLap = new DateTime(2026, 7, 22), PhuongThucTT = "Tiền mặt" },
            new HoaDon { MaHD = 4, MaDatPhong = 4, TongTien = 1300000, NgayLap = new DateTime(2026, 7, 13), PhuongThucTT = "Thẻ" },
            new HoaDon { MaHD = 5, MaDatPhong = 5, TongTien = 2400000, NgayLap = new DateTime(2026, 7, 20), PhuongThucTT = "Chuyển khoản" }
            );

            //====================== CHI TIẾT HÓA ĐƠN ======================
            modelBuilder.Entity<ChiTietHoaDon>().HasData(
            new ChiTietHoaDon { MaCTHD = 1, MaHD = 1, MaDatPhong = 1, MaCTDV = null, ThanhTien = 1000000 },
            new ChiTietHoaDon { MaCTHD = 2, MaHD = 1, MaDatPhong = null, MaCTDV = 1, ThanhTien = 200000 },
            new ChiTietHoaDon { MaCTHD = 3, MaHD = 1, MaDatPhong = null, MaCTDV = 2, ThanhTien = 150000 },

            new ChiTietHoaDon { MaCTHD = 4, MaHD = 2, MaDatPhong = 2, MaCTDV = null, ThanhTien = 1800000 },
            new ChiTietHoaDon { MaCTHD = 5, MaHD = 2, MaDatPhong = null, MaCTDV = 3, ThanhTien = 300000 },
            new ChiTietHoaDon { MaCTHD = 6, MaHD = 2, MaDatPhong = null, MaCTDV = 4, ThanhTien = 400000 },

            new ChiTietHoaDon { MaCTHD = 7, MaHD = 3, MaDatPhong = 3, MaCTDV = null, ThanhTien = 1200000 },

            new ChiTietHoaDon { MaCTHD = 8, MaHD = 4, MaDatPhong = 4, MaCTDV = null, ThanhTien = 900000 },
            new ChiTietHoaDon { MaCTHD = 9, MaHD = 4, MaDatPhong = null, MaCTDV = 5, ThanhTien = 400000 },

            new ChiTietHoaDon { MaCTHD = 10, MaHD = 5, MaDatPhong = 5, MaCTDV = null, ThanhTien = 2400000 }
            );
        }
    }
}
