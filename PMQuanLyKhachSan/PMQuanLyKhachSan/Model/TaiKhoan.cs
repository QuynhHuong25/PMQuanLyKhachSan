using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyKhachSan.Model
{
    public class TaiKhoan
    {
        [Key]
        public int? MaTK { get; set; }

        [Required]
        [StringLength(50)]
        public string? TenDangNhap { get; set; }

        [Required]
        [StringLength(100)]
        public string? MatKhau { get; set; }

        [ForeignKey("NhanVien")]
        public int MaNV { get; set; }

        public virtual NhanVien? NhanVien { get; set; }
    }
}
