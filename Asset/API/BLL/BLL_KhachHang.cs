using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_KhachHang : BLL_IKhachHang
    {
        private DAL_IKhachHang dAL_IKhachHang;

        public BLL_KhachHang(DAL_IKhachHang dAL_IKhachHang)
        {
            this.dAL_IKhachHang = dAL_IKhachHang;
        }

        public List<Model_KhachHang> GetAll()
        {
            return dAL_IKhachHang.GetAll();
        }

        public List<Model_KhachHang> Search(string tenKhachHang)
        {
            return dAL_IKhachHang.Search(tenKhachHang);
        }

        public bool Update(Model_KhachHang KhachHang)
        {
            return dAL_IKhachHang.Update(KhachHang);
        }
    }
}
