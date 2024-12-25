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
        List<Model_GiamGia> ApplyMaGiamGia(string magiamgia);
    }
}
