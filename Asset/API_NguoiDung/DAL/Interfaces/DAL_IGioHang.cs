using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface DAL_IGioHang
    {
        bool Create(Model_GioHang giohang);
        bool Delete(string username);
        List<Model_GioHang> GetGioHangByUser(string username);
    }
}
