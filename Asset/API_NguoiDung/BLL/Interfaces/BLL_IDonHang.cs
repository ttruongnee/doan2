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

        bool Create(Model_DonHang donhang);
        bool Update(Model_DonHang donhang);
        bool Delete(string idDonHang);
        List<Model_DonHang> Search(string search);
    }
}
