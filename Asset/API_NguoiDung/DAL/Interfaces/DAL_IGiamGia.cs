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
        List<Model_GiamGia> ApplyMaGiamGia(string magiamgia);
    }
}
