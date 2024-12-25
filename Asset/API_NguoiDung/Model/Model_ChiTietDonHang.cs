using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Model_ChiTietDonHang
    {
        public string? MaCTDonHang {  get; set; }
        public string? MaDonHang {  get; set; }  
        public string MaTruyen {  get; set; }  
        public string? TenTruyen {  get; set; }  
        public int SoLuong {  get; set; }  
        public double? DonGia {  get; set; }  
        public double? ThanhTien {  get; set; }  
    }
}
