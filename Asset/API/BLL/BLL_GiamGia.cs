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
    public class BLL_GiamGia : BLL_IGiamGia
    {
        private DAL_IGiamGia dAL_IGiamGia;

        public BLL_GiamGia(DAL_IGiamGia dAL_IGiamGia)
        {
            this.dAL_IGiamGia = dAL_IGiamGia;
        }

        public bool Create(Model_GiamGia GiamGia)
        {
            return dAL_IGiamGia.Create(GiamGia);
        }

        public bool Delete(string idGiamGia)
        {
            return dAL_IGiamGia.Delete(idGiamGia);
        }

        public List<Model_GiamGia> GetAll()
        {
            return dAL_IGiamGia.GetAll();
        }

        public List<Model_GiamGia> Search(DateTime? NgayBatDau, DateTime? NgayKetThuc)
        {
            return dAL_IGiamGia.Search(NgayBatDau, NgayKetThuc);
        }

        public bool Update(Model_GiamGia GiamGia)
        {
            return dAL_IGiamGia.Update(GiamGia);
        }
    }
}
