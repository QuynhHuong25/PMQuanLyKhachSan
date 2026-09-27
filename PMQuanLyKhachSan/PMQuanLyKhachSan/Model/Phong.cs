using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyKhachSan.Model
{
    public class Phong
    {
        [Key]
        public int MaPhong { get; set; }

        [Required]
        [StringLength(50)]
        public string TenPhong { get; set; }

        [ForeignKey("LoaiPhong")]
        public int MaLoai { get; set; }

        [Required]
        [StringLength(30)]
        public string TrangThai { get; set; }

        public virtual LoaiPhong? LoaiPhong { get; set; }

        public virtual ICollection<DatPhong>? DatPhongs { get; set; }
    }
}
