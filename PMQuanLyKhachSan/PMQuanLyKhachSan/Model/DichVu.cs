using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyKhachSan.Model
{
    public class DichVu
    {
        [Key]
        public int MaDV { get; set; }

        [Required]
        [StringLength(100)]
        public string TenDV { get; set; }

        public decimal Gia { get; set; }

        public virtual ICollection<ChiTietDichVu>? ChiTietDichVus { get; set; }
    }
}
