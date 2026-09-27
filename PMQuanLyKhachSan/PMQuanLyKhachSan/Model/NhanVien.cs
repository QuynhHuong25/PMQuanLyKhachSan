using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyKhachSan.Model
{
    public class NhanVien
    {
        [Key]
        public int MaNV { get; set; }
        [ForeignKey("TaiKhoan")]
        public int MaTK { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Required]
        public DateTime NgaySinh { get; set; }

        public bool GioiTinh { get; set; }

        [StringLength(15)]
        public string SDT { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [ForeignKey("ChucVu")]
        public int MaChucVu { get; set; }

        public virtual ChucVu? ChucVu { get; set; }

        public virtual ICollection<TaiKhoan>? TaiKhoans { get; set; }

        public virtual ICollection<LichPhanCong>? LichPhanCongs { get; set; }
    }
}
