using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyKhachSan.Model
{
    public class LoaiPhong
    {
        [Key]
        public int MaLoai { get; set; }

        [Required]
        [StringLength(50)]
        public string TenLoai { get; set; }

        public decimal Gia { get; set; }

        [StringLength(300)]
        public string? MoTa { get; set; }

        public virtual ICollection<Phong>? Phongs { get; set; }
    }
}
