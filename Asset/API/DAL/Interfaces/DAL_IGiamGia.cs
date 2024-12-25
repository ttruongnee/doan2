using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface DAL_IGiamGia
    {
        List<Model_GiamGia> GetAll();

        bool Create(Model_GiamGia GiamGia);
        bool Update(Model_GiamGia GiamGia);
        bool Delete(string idGiamGia);
        List<Model_GiamGia> Search(DateTime? NgayBatDau, DateTime? NgayKetThuc);
    }
}
