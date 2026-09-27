using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMQuanLyKhachSan.Migrations
{
    public partial class PMQuanLyKhachSan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChucVus",
                columns: table => new
                {
                    MaChucVu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChucVu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucVus", x => x.MaChucVu);
                });

            migrationBuilder.CreateTable(
                name: "DichVus",
                columns: table => new
                {
                    MaDV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDV = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DichVus", x => x.MaDV);
                });

            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    MaKH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    CCCD = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.MaKH);
                });

            migrationBuilder.CreateTable(
                name: "LoaiPhongs",
                columns: table => new
                {
                    MaLoai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiPhongs", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "NhanViens",
                columns: table => new
                {
                    MaNV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaTK = table.Column<int>(type: "int", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MaChucVu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanViens", x => x.MaNV);
                    table.ForeignKey(
                        name: "FK_NhanViens_ChucVus_MaChucVu",
                        column: x => x.MaChucVu,
                        principalTable: "ChucVus",
                        principalColumn: "MaChucVu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phongs",
                columns: table => new
                {
                    MaPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaLoai = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phongs", x => x.MaPhong);
                    table.ForeignKey(
                        name: "FK_Phongs_LoaiPhongs_MaLoai",
                        column: x => x.MaLoai,
                        principalTable: "LoaiPhongs",
                        principalColumn: "MaLoai",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichPhanCongs",
                columns: table => new
                {
                    MaLich = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNV = table.Column<int>(type: "int", nullable: false),
                    NgayLam = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaLam = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GioBatDau = table.Column<TimeSpan>(type: "time", nullable: false),
                    GioKetThuc = table.Column<TimeSpan>(type: "time", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichPhanCongs", x => x.MaLich);
                    table.ForeignKey(
                        name: "FK_LichPhanCongs_NhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanViens",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoans",
                columns: table => new
                {
                    MaTK = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaNV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoans", x => x.MaTK);
                    table.ForeignKey(
                        name: "FK_TaiKhoans_NhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanViens",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatPhongs",
                columns: table => new
                {
                    MaDatPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKH = table.Column<int>(type: "int", nullable: false),
                    MaPhong = table.Column<int>(type: "int", nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayNhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatPhongs", x => x.MaDatPhong);
                    table.ForeignKey(
                        name: "FK_DatPhongs_KhachHangs_MaKH",
                        column: x => x.MaKH,
                        principalTable: "KhachHangs",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DatPhongs_Phongs_MaPhong",
                        column: x => x.MaPhong,
                        principalTable: "Phongs",
                        principalColumn: "MaPhong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDichVus",
                columns: table => new
                {
                    MaCTDV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDatPhong = table.Column<int>(type: "int", nullable: false),
                    MaDV = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThanhTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDichVus", x => x.MaCTDV);
                    table.ForeignKey(
                        name: "FK_ChiTietDichVus_DatPhongs_MaDatPhong",
                        column: x => x.MaDatPhong,
                        principalTable: "DatPhongs",
                        principalColumn: "MaDatPhong",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDichVus_DichVus_MaDV",
                        column: x => x.MaDV,
                        principalTable: "DichVus",
                        principalColumn: "MaDV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    MaHD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDatPhong = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhuongThucTT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.MaHD);
                    table.ForeignKey(
                        name: "FK_HoaDons_DatPhongs_MaDatPhong",
                        column: x => x.MaDatPhong,
                        principalTable: "DatPhongs",
                        principalColumn: "MaDatPhong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDons",
                columns: table => new
                {
                    MaCTHD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHD = table.Column<int>(type: "int", nullable: false),
                    MaDatPhong = table.Column<int>(type: "int", nullable: true),
                    MaCTDV = table.Column<int>(type: "int", nullable: true),
                    ThanhTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHoaDons", x => x.MaCTHD);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDons_ChiTietDichVus_MaCTDV",
                        column: x => x.MaCTDV,
                        principalTable: "ChiTietDichVus",
                        principalColumn: "MaCTDV");
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDons_DatPhongs_MaDatPhong",
                        column: x => x.MaDatPhong,
                        principalTable: "DatPhongs",
                        principalColumn: "MaDatPhong");
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDons_HoaDons_MaHD",
                        column: x => x.MaHD,
                        principalTable: "HoaDons",
                        principalColumn: "MaHD",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ChucVus",
                columns: new[] { "MaChucVu", "TenChucVu" },
                values: new object[,]
                {
                    { 1, "Quản lý" },
                    { 2, "Lễ tân" },
                    { 3, "Lao công" },
                    { 4, "Đầu bếp" },
                    { 5, "Phục vụ nhà hàng" },
                    { 6, "Bảo vệ" }
                });

            migrationBuilder.InsertData(
                table: "DichVus",
                columns: new[] { "MaDV", "Gia", "TenDV" },
                values: new object[,]
                {
                    { 1, 250000m, "Ăn uống" },
                    { 2, 50000m, "Giặt ủi" },
                    { 3, 200000m, "Thuê xe máy" },
                    { 4, 300000m, "Đưa đón sân bay" },
                    { 5, 400000m, "Spa" }
                });

            migrationBuilder.InsertData(
                table: "KhachHangs",
                columns: new[] { "MaKH", "CCCD", "GioiTinh", "HoTen", "MatKhau", "NgaySinh", "SDT", "TenDangNhap" },
                values: new object[,]
                {
                    { 1, "00111111", true, "Nguyễn Minh Anh", "123456789", new DateTime(2001, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "0901111111", "anh" },
                    { 2, "00222222", true, "Trần Quốc Bảo", "123456789", new DateTime(2000, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "0902222222", "bao" },
                    { 3, "00333333", true, "Lê Thanh Bình", "123456789", new DateTime(2002, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "0903333333", "binh" },
                    { 4, "00444444", false, "Phạm Gia Hân", "123456789", new DateTime(2003, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "0904444444", "han" },
                    { 5, "00555555", false, "Đỗ Thu Hà", "123456789", new DateTime(2001, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "0905555555", "ha" },
                    { 6, "00666666", true, "Nguyễn Văn Nam", "123456789", new DateTime(1999, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "0906666666", "nam" },
                    { 7, "00777777", false, "Bùi Thị Ngọc", "123456789", new DateTime(2002, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "0907777777", "ngoc" },
                    { 8, "00888888", true, "Hoàng Đức Long", "123456789", new DateTime(2000, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "0908888888", "long" },
                    { 9, "00999999", false, "Nguyễn Thị Hồng", "123456789", new DateTime(2001, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "0909999999", "hong" },
                    { 10, "01010101", true, "Vũ Mạnh Cường", "123456789", new DateTime(1998, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "0910101010", "cuong" }
                });

            migrationBuilder.InsertData(
                table: "LoaiPhongs",
                columns: new[] { "MaLoai", "Gia", "MoTa", "TenLoai" },
                values: new object[,]
                {
                    { 1, 3000000m, "Phòng phù hợp cho 2 người lớn và 1 trẻ em", "Standard 1 giường đôi" },
                    { 2, 3500000m, "Phòng phù hợp cho 2 người lớn", "Standard 2 giường đơn" },
                    { 3, 5000000m, "Phòng phù hợp cho 2 người lớn và 2 trẻ em", "Deluxe 1 giường đôi" },
                    { 4, 7000000m, "Phòng phù hợp cho 4 người lớn", "Deluxe 2 giường đôi" },
                    { 5, 8000000m, "Phòng cao cấp phù hợp cho 2 người lớn và 2 trẻ em", "Suite" }
                });

            migrationBuilder.InsertData(
                table: "NhanViens",
                columns: new[] { "MaNV", "Email", "GioiTinh", "HoTen", "MaChucVu", "MaTK", "NgaySinh", "SDT" },
                values: new object[,]
                {
                    { 1, "huong@gmail.com", false, "Lương Thị Quỳnh Hương", 1, 1, new DateTime(2005, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "0976111111" },
                    { 2, "hay@gmail.com", false, "Trịnh Thị Hay", 1, 2, new DateTime(2005, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "0982222222" },
                    { 3, "diem@gmail.com", false, "Nguyễn Thị Diễm", 1, 3, new DateTime(2005, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "0983333333" },
                    { 4, "an@gmail.com", true, "Nguyễn Văn An", 2, 4, new DateTime(1999, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "0984444444" },
                    { 5, "mai@gmail.com", false, "Phạm Thị Mai", 2, 5, new DateTime(2000, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "0985555555" },
                    { 6, "tung@gmail.com", true, "Lê Văn Tùng", 3, 6, new DateTime(1998, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "0986666666" },
                    { 7, "lan@gmail.com", false, "Đỗ Thị Lan", 4, 7, new DateTime(1997, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "0987777777" },
                    { 8, "binh@gmail.com", true, "Hoàng Văn Bình", 5, 8, new DateTime(1996, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "0988888888" },
                    { 9, "thu@gmail.com", false, "Ngô Thị Thu", 6, 9, new DateTime(1995, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "0989999999" },
                    { 10, "hung@gmail.com", true, "Trần Văn Hùng", 5, 10, new DateTime(1994, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "0971111111" }
                });

            migrationBuilder.InsertData(
                table: "Phongs",
                columns: new[] { "MaPhong", "MaLoai", "TenPhong", "TrangThai" },
                values: new object[,]
                {
                    { 101, 1, "P101", "Trống" },
                    { 102, 1, "P102", "Trống" },
                    { 201, 2, "P201", "Trống" },
                    { 202, 3, "P202", "Trống" },
                    { 301, 4, "P301", "Trống" },
                    { 302, 5, "P302", "Trống" },
                    { 303, 2, "P303", "Trống" },
                    { 304, 3, "P304", "Trống" },
                    { 305, 4, "P305", "Trống" },
                    { 306, 5, "P306", "Trống" }
                });

            migrationBuilder.InsertData(
                table: "DatPhongs",
                columns: new[] { "MaDatPhong", "MaKH", "MaPhong", "NgayDat", "NgayNhan", "NgayTra", "TrangThai" },
                values: new object[,]
                {
                    { 1, 1, 102, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đã đặt" },
                    { 2, 2, 202, new DateTime(2026, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang ở" },
                    { 3, 3, 301, new DateTime(2026, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đã thanh toán" },
                    { 4, 4, 303, new DateTime(2026, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang hủy" },
                    { 5, 5, 305, new DateTime(2026, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đã trả phòng" }
                });

            migrationBuilder.InsertData(
                table: "LichPhanCongs",
                columns: new[] { "MaLich", "CaLam", "GhiChu", "GioBatDau", "GioKetThuc", "MaNV", "NgayLam" },
                values: new object[,]
                {
                    { 1, "Sáng", "", new TimeSpan(0, 7, 0, 0, 0), new TimeSpan(0, 11, 30, 0, 0), 1, new DateTime(2026, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Chiều", "", new TimeSpan(0, 13, 0, 0, 0), new TimeSpan(0, 17, 0, 0, 0), 2, new DateTime(2026, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Tối", "", new TimeSpan(0, 17, 30, 0, 0), new TimeSpan(0, 22, 0, 0, 0), 3, new DateTime(2026, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Sáng", "", new TimeSpan(0, 7, 0, 0, 0), new TimeSpan(0, 11, 30, 0, 0), 4, new DateTime(2026, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Chiều", "", new TimeSpan(0, 13, 0, 0, 0), new TimeSpan(0, 17, 0, 0, 0), 5, new DateTime(2026, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Tối", "", new TimeSpan(0, 17, 30, 0, 0), new TimeSpan(0, 22, 0, 0, 0), 6, new DateTime(2026, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Sáng", "", new TimeSpan(0, 7, 0, 0, 0), new TimeSpan(0, 11, 30, 0, 0), 7, new DateTime(2026, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Chiều", "", new TimeSpan(0, 13, 0, 0, 0), new TimeSpan(0, 17, 0, 0, 0), 8, new DateTime(2026, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "Tối", "", new TimeSpan(0, 17, 30, 0, 0), new TimeSpan(0, 22, 0, 0, 0), 9, new DateTime(2026, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "Sáng", "", new TimeSpan(0, 7, 0, 0, 0), new TimeSpan(0, 11, 30, 0, 0), 10, new DateTime(2026, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "TaiKhoans",
                columns: new[] { "MaTK", "MaNV", "MatKhau", "TenDangNhap" },
                values: new object[,]
                {
                    { 1, 1, "123456", "huong" },
                    { 2, 2, "123456", "hay" },
                    { 3, 3, "123456", "diem" },
                    { 4, 4, "123456", "an" },
                    { 5, 5, "123456", "mai" }
                });

            migrationBuilder.InsertData(
                table: "ChiTietDichVus",
                columns: new[] { "MaCTDV", "DonGia", "MaDV", "MaDatPhong", "SoLuong", "ThanhTien" },
                values: new object[,]
                {
                    { 1, 100000m, 1, 1, 2, 200000m },
                    { 2, 50000m, 2, 1, 3, 150000m },
                    { 3, 300000m, 4, 2, 1, 300000m },
                    { 4, 400000m, 5, 2, 1, 400000m },
                    { 5, 200000m, 3, 4, 2, 400000m }
                });

            migrationBuilder.InsertData(
                table: "HoaDons",
                columns: new[] { "MaHD", "MaDatPhong", "NgayLap", "PhuongThucTT", "TongTien" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tiền mặt", 1350000m },
                    { 2, 2, new DateTime(2026, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chuyển khoản", 2500000m },
                    { 3, 3, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tiền mặt", 1200000m },
                    { 4, 4, new DateTime(2026, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thẻ", 1300000m },
                    { 5, 5, new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chuyển khoản", 2400000m }
                });

            migrationBuilder.InsertData(
                table: "ChiTietHoaDons",
                columns: new[] { "MaCTHD", "MaCTDV", "MaDatPhong", "MaHD", "ThanhTien" },
                values: new object[,]
                {
                    { 1, null, 1, 1, 1000000m },
                    { 2, 1, null, 1, 200000m },
                    { 3, 2, null, 1, 150000m },
                    { 4, null, 2, 2, 1800000m },
                    { 5, 3, null, 2, 300000m },
                    { 6, 4, null, 2, 400000m },
                    { 7, null, 3, 3, 1200000m },
                    { 8, null, 4, 4, 900000m },
                    { 9, 5, null, 4, 400000m },
                    { 10, null, 5, 5, 2400000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDichVus_MaDatPhong",
                table: "ChiTietDichVus",
                column: "MaDatPhong");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDichVus_MaDV",
                table: "ChiTietDichVus",
                column: "MaDV");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDons_MaCTDV",
                table: "ChiTietHoaDons",
                column: "MaCTDV");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDons_MaDatPhong",
                table: "ChiTietHoaDons",
                column: "MaDatPhong");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDons_MaHD",
                table: "ChiTietHoaDons",
                column: "MaHD");

            migrationBuilder.CreateIndex(
                name: "IX_DatPhongs_MaKH",
                table: "DatPhongs",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_DatPhongs_MaPhong",
                table: "DatPhongs",
                column: "MaPhong");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_MaDatPhong",
                table: "HoaDons",
                column: "MaDatPhong");

            migrationBuilder.CreateIndex(
                name: "IX_LichPhanCongs_MaNV",
                table: "LichPhanCongs",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_MaChucVu",
                table: "NhanViens",
                column: "MaChucVu");

            migrationBuilder.CreateIndex(
                name: "IX_Phongs_MaLoai",
                table: "Phongs",
                column: "MaLoai");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_MaNV",
                table: "TaiKhoans",
                column: "MaNV");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietHoaDons");

            migrationBuilder.DropTable(
                name: "LichPhanCongs");

            migrationBuilder.DropTable(
                name: "TaiKhoans");

            migrationBuilder.DropTable(
                name: "ChiTietDichVus");

            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DropTable(
                name: "NhanViens");

            migrationBuilder.DropTable(
                name: "DichVus");

            migrationBuilder.DropTable(
                name: "DatPhongs");

            migrationBuilder.DropTable(
                name: "ChucVus");

            migrationBuilder.DropTable(
                name: "KhachHangs");

            migrationBuilder.DropTable(
                name: "Phongs");

            migrationBuilder.DropTable(
                name: "LoaiPhongs");
        }
    }
}
