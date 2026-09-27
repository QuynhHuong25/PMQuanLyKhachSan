using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyKhachSan.Model
{
    public class KhachHang
    {
        [Key]
        public int MaKH { get; set; }
        [StringLength(100)]
        public string? HoTen { get; set; }

        public DateTime? NgaySinh { get; set; }
        public bool? GioiTinh { get; set; }


        [StringLength(12)]
        public string? CCCD { get; set; }

        [StringLength(15)]
        public string? SDT { get; set; }
        [StringLength(50)]
        public string? TenDangNhap { get; set; }

        [StringLength(100)]
        public string? MatKhau { get; set; }

        public virtual ICollection<DatPhong>? DatPhongs { get; set; }
    }
}
