using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface DAL_IKhachHang
    {
        List<Model_KhachHang> GetAll();
        bool Update(Model_KhachHang KhachHang);
        List<Model_KhachHang> Search(string tenKhachHang);
    }
}
