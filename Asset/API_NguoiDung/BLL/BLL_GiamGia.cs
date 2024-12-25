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
        public List<Model_GiamGia> ApplyMaGiamGia(string magiamgia)
        {
            return dAL_IGiamGia.ApplyMaGiamGia(magiamgia);
        }

    }
}
