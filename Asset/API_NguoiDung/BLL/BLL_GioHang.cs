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
    public class BLL_GioHang : BLL_IGioHang
    {
        private DAL_IGioHang dAL_IGioHang;

        public BLL_GioHang(DAL_IGioHang dAL_IGioHang)
        {
            this.dAL_IGioHang = dAL_IGioHang;
        }
        public bool Create(Model_GioHang giohang)
        {
            return dAL_IGioHang.Create(giohang);
        }
        public bool Delete(string username)
        {
            return dAL_IGioHang.Delete(username);
        }
        public List<Model_GioHang> GetGioHangByUser(string username)
        {
            return dAL_IGioHang.GetGioHangByUser(username);
        }
    }
}
