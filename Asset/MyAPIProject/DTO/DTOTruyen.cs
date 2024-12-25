using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOTruyen
    {
        public string MaTruyen { get; set; }
        public string TenTruyen { get; set; }
        public string TacGia { get; set; }
        public string MaTheLoai { get; set; }
        public int SoLuong { get; set; }
        public double GiaBan { get; set; }
        public string MoTa { get; set; }
        public string AnhTruyenURL { get; set; }
        public string DanhSachBinhLuanJSON { get; set; }
    }
}
