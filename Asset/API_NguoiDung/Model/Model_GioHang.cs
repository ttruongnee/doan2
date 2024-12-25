using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Model_GioHang
    {
        public string MaGioHang { get; set; }
        public string MaKhachHang { get; set; }
        public double TongTien { get; set; }
        public List<Model_ChiTietGioHang>? listjson_chitiet { get; set; }
    }
}
