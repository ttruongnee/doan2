using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface DAL_ITaiKhoan
    {
        List<Model_TaiKhoanUpdateMatKhau> GetEmailKhachHang();
        List<Model_TaiKhoan> GetMaKhachHang(string TaiKhoan);
        bool DangNhapUser(Model_TaiKhoan taikhoan);
        bool Create(Model_CreateTaiKhoan taikhoan);
        bool UpdateMatKhau(Model_TaiKhoanUpdateMatKhau taikhoan);
    }
}
