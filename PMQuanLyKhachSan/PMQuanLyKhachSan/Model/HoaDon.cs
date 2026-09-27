using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyKhachSan.Model
{
    public class HoaDon
    {
        [Key]
        public int MaHD { get; set; }

        [ForeignKey("DatPhong")]
        public int MaDatPhong { get; set; }

        public decimal? TongTien { get; set; }

        public DateTime? NgayLap { get; set; }

        [StringLength(50)]
        public string? PhuongThucTT { get; set; }

        public virtual DatPhong? DatPhong { get; set; }
    }
}
