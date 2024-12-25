using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Model
{
    public class Model_DonHang
    {
        public string MaDonHang {  get; set; }
        public string? MaKhachHangDatHang {  get; set; }
        public string? TenKhachHang {  get; set; }
        public string? MaNhanVienXuLyDonHang {  get; set; }
        public string? TenNhanVien {  get; set; }
        public DateTime? NgayDatHang {  get; set; }
        public string? DiaChiGiaoHang { get; set; }
        public string TrangThai {  get; set; }
        public string? PhuongThucThanhToan {  get; set; }
        public string? MaGiamGia {  get; set; }
        public double? TongTien {  get; set; }

        public List<Model_ChiTietDonHang>? listjson_chitiet { get; set; }


    }
}
