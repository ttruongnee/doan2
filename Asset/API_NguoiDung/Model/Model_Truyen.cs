using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Model_Truyen
    {
        public string? MaTruyen { get; set; }
        public string AnhTruyen { get; set; }
        public string TenTruyen { get; set; }
        public string ISBN { get; set; }
        public string TacGia { get; set; }
        public string DoiTuong { get; set; }
        public int SoTrang { get; set; }
        public string DinhDang { get; set; }
        public string TrongLuong { get; set; }
        public string MaTheLoai { get; set; }
        public string? TenTheLoai { get; set; }
        public int SoLuong { get; set; }
        public double GiaGoc { get; set; }
        public float PhanTramGiamGia { get; set; }
        public double GiaBan { get; set; }
    }
}
