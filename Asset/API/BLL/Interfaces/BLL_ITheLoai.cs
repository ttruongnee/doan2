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

        bool Create(Model_TheLoai theloai);
        bool Update(Model_TheLoai theloai);
        bool Delete(string idTheLoai);
        List<Model_TheLoai> Search(string tenTheLoai);
    }
}
