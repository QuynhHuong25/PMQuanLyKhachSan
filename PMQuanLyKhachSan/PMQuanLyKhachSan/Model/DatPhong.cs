using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyKhachSan.Model
{
    public class DatPhong
    {
        [Key]
        public int MaDatPhong { get; set; }

        [ForeignKey("KhachHang")]
        public int MaKH { get; set; }

        [ForeignKey("Phong")]
        public int MaPhong { get; set; }
        [Required]
        public DateTime NgayDat { get; set; }

        [Required]
        public DateTime NgayNhan { get; set; }

        [Required]
        public DateTime NgayTra { get; set; }

        [StringLength(30)]
        public string TrangThai { get; set; }

        public virtual KhachHang? KhachHang { get; set; }

        public virtual Phong? Phong { get; set; }

        public virtual ICollection<ChiTietDichVu>? ChiTietDichVus { get; set; }

        public virtual ICollection<HoaDon>? HoaDons { get; set; }
    }
}
