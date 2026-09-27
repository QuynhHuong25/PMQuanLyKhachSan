using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyKhachSan.Model
{
    public class ChiTietDichVu
    {
        [Key]
        public int MaCTDV { get; set; }

        [ForeignKey("DatPhong")]
        public int MaDatPhong { get; set; }

        [ForeignKey("DichVu")]
        public int MaDV { get; set; }

        public int SoLuong { get; set; }

        public decimal DonGia { get; set; }

        public decimal ThanhTien { get; set; }

        public DateTime? NgaySuDung { get; set; }

        public virtual DatPhong? DatPhong { get; set; }

        public virtual DichVu? DichVu { get; set; }

        public virtual ICollection<ChiTietHoaDon>? ChiTietHoaDons { get; set; }
    }
}
