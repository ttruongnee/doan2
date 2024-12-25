using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_TaiKhoan : BLL_ITaiKhoan
    {
        private DAL_ITaiKhoan dAL_ITaiKhoan;

        public BLL_TaiKhoan(DAL_ITaiKhoan dAL_ITaiKhoan)
        {
            this.dAL_ITaiKhoan = dAL_ITaiKhoan;
        }

        public bool Create(Model_CreateTaiKhoan TaiKhoan)
        {
            return dAL_ITaiKhoan.Create(TaiKhoan);
        }

        public bool DangNhapAdmin(Model_TaiKhoan taikhoan)
        {
            return dAL_ITaiKhoan.DangNhapAdmin(taikhoan);
        }

        public bool DangNhapUser(Model_TaiKhoan taikhoan)
        {
            return dAL_ITaiKhoan.DangNhapUser(taikhoan);
        }

        public bool Delete(string maTaiKhoan)
        {
            return dAL_ITaiKhoan.Delete(maTaiKhoan);
        }

        public List<Model_TaiKhoan> GetAll()
        {
            return dAL_ITaiKhoan.GetAll();
        }

        public List<Model_TaiKhoanUpdateMatKhau> GetEmailNhanVien()
        {
            return dAL_ITaiKhoan.GetEmailNhanVien();
        }
        
        public List<Model_TaiKhoanUpdateMatKhau> GetEmailKhachHang()
        {
            return dAL_ITaiKhoan.GetEmailKhachHang();
        }

        public List<Model_TaiKhoan> Search(string tenNguoiDung)
        {
            return dAL_ITaiKhoan.Search(tenNguoiDung);
        }

        public List<Model_TaiKhoan> GetQuyenByID(string id)
        {
            return dAL_ITaiKhoan.GetQuyenByID(id);
        }

        public bool UpdateMatKhau(Model_TaiKhoanUpdateMatKhau taikhoan)
        {
            return dAL_ITaiKhoan.UpdateMatKhau(taikhoan);
        }


    }
}
