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
        List<Model_TaiKhoan> GetAll();
        List<Model_TaiKhoanUpdateMatKhau> GetEmailNhanVien();
        List<Model_TaiKhoanUpdateMatKhau> GetEmailKhachHang();
        bool DangNhapAdmin(Model_TaiKhoan taikhoan);
        bool DangNhapUser(Model_TaiKhoan taikhoan);
        bool Create(Model_CreateTaiKhoan taikhoan);
        bool UpdateMatKhau(Model_TaiKhoanUpdateMatKhau taikhoan);
        bool Delete(string idTaiKhoan);
        List<Model_TaiKhoan> Search(string tenNguoiDung);
        List<Model_TaiKhoan> GetQuyenByID(string id);
    }
}
