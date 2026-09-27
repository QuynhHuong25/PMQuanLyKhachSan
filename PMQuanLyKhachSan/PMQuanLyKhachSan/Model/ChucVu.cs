using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyKhachSan.Model
{
    public class ChucVu
    {
        [Key]
        public int MaChucVu { get; set; }

        [Required]
        [StringLength(50)]
        public string TenChucVu { get; set; }

        public virtual ICollection<NhanVien>? NhanViens { get; set; }
    }
}
