using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface BLL_ITheLoai
    {
        List<Model_TheLoai> GetAll();

        List<Model_TheLoai> Search(string tenTheLoai);
    }
}
