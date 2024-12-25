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
    public class BLL_DonHang : BLL_IDonHang
    {
        private DAL_IDonHang dAL_IDonHang;

        public BLL_DonHang(DAL_IDonHang dAL_IDonHang)
        {
            this.dAL_IDonHang = dAL_IDonHang;
        }

        public bool Delete(string maDonHang)
        {
            return dAL_IDonHang.Delete(maDonHang);
        }

        public List<Model_DonHang> GetAll()
        {
            return dAL_IDonHang.GetAll();
        }

        public List<Model_ChiTietDonHang> GetChiTietDonHangByID(string madonhang)
        {
            return dAL_IDonHang.GetChiTietDonHangByID(madonhang);
        }

        public List<Model_DonHang> Search(string search)
        {
            return dAL_IDonHang.Search(search);
        }

        public bool Update(Model_DonHang donhang)
        {
            return dAL_IDonHang.Update(donhang);
        }
    }
}
