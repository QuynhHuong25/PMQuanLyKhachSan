using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyKhachSan.Model
{
    public class ChiTietHoaDon
    {
        [Key]
        public int MaCTHD { get; set; }

        [ForeignKey("HoaDon")]
        public int MaHD { get; set; }

        // Nếu là tiền phòng thì có giá trị
        [ForeignKey("DatPhong")]
        public int? MaDatPhong { get; set; }

        // Nếu là tiền dịch vụ thì có giá trị
        [ForeignKey("ChiTietDichVu")]
        public int? MaCTDV { get; set; }

        public decimal ThanhTien { get; set; }

        public virtual HoaDon? HoaDon { get; set; }

        public virtual DatPhong? DatPhong { get; set; }

        public virtual ChiTietDichVu? ChiTietDichVu { get; set; }
    }
}
