using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface DAL_IDonHang
    {
        List<Model_DonHang> GetAll();
        List<Model_ChiTietDonHang> GetChiTietDonHangByID(string madonhang);
        bool Update(Model_DonHang donhang);
        bool Delete(string maDonHang);
        List<Model_DonHang> Search(string search);
    }
}
