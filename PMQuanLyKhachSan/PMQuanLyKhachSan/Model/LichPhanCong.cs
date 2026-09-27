using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyKhachSan.Model
{
    public class LichPhanCong
    {
        [Key]
        public int MaLich { get; set; }

        [ForeignKey("NhanVien")]
        public int? MaNV { get; set; }

        [Required]
        public DateTime NgayLam { get; set; }

        [Required]
        [StringLength(20)]
        public string CaLam { get; set; }

        public TimeSpan? GioBatDau { get; set; }

        public TimeSpan? GioKetThuc { get; set; }

        [StringLength(200)]
        public string? GhiChu { get; set; }

        public virtual NhanVien? NhanVien { get; set; }
    }
}
