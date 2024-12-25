using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Model_ChiTietGioHang
    {
        public string? MaCTGioHang { get; set; }
        public string? MaGioHang { get; set; }
        public string MaTruyen { get; set; }
        public string? TenTruyen { get; set; }
        public int SoLuong { get; set; }
        public double? DonGia { get; set; }
        public double? ThanhTien { get; set; }

    }
}
