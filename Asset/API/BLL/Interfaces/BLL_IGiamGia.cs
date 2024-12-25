using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface BLL_IGiamGia
    {
        List<Model_GiamGia> GetAll();

        bool Create(Model_GiamGia GiamGia);
        bool Update(Model_GiamGia GiamGia);
        bool Delete(string idGiamGia);
        List<Model_GiamGia> Search(DateTime? NgayBatDau, DateTime? NgayKetThuc);
    }
}
