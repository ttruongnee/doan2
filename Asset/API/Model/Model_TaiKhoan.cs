using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Model_TaiKhoan
    {
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string? Quyen { get; set; }
        public string? TenNguoiDung { get; set; }
    }
    public class Model_TaiKhoanUpdateMatKhau
    {
        public string? MatKhau { get; set; }
        public string Email { get; set; }
    }

    public class Model_CreateTaiKhoan
    {
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string Quyen { get; set; }
        public string TenNguoiDung {  get; set; }
        public string GioiTinh {  get; set; }
        public string SoDienThoai {  get; set; }
        public string Email {  get; set; }
        public string? QueQuan {  get; set; }
    }
}
