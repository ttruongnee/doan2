using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface BLL_IDonHang
    {
        List<Model_DonHang> GetAll();
        List<Model_ChiTietDonHang> GetChiTietDonHangByID(string madonhang);
        bool Update(Model_DonHang donhang);
        bool Delete(string idDonHang);
        List<Model_DonHang> Search(string search);
    }
}
