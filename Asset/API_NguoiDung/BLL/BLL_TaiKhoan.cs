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

        public bool DangNhapUser(Model_TaiKhoan taikhoan)
        {
            return dAL_ITaiKhoan.DangNhapUser(taikhoan);
        }

        public List<Model_TaiKhoanUpdateMatKhau> GetEmailKhachHang()
        {
            return dAL_ITaiKhoan.GetEmailKhachHang();
        }

        public List<Model_TaiKhoan> GetMaKhachHang(string TaiKhoan)
        {
            return dAL_ITaiKhoan.GetMaKhachHang(TaiKhoan);
        }


        public bool UpdateMatKhau(Model_TaiKhoanUpdateMatKhau taikhoan)
        {
            return dAL_ITaiKhoan.UpdateMatKhau(taikhoan);
        }


    }
}
