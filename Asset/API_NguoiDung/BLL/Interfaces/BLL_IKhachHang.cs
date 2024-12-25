using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface BLL_IKhachHang
    {
        List<Model_KhachHang> GetAll();

        bool Update(Model_KhachHang KhachHang);
        List<Model_KhachHang> GetNameByUser(string user);
    }
}
