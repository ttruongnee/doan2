using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_NhanVien : BLL_INhanVien
    {
        private DAL_INhanVien dAL_INhanVien;

        public BLL_NhanVien(DAL_INhanVien dAL_INhanVien)
        {
            this.dAL_INhanVien = dAL_INhanVien;
        }

        public List<Model_NhanVien> GetAll()
        {
            return dAL_INhanVien.GetAll();
        }

        public List<Model_NhanVien> Search(string tenNhanVien)
        {
            return dAL_INhanVien.Search(tenNhanVien);
        }
        
        public List<Model_NhanVien> GetNhanVienByUser(string user)
        {
            return dAL_INhanVien.GetNhanVienByUser(user);
        }

        public bool Update(Model_NhanVien NhanVien)
        {
            return dAL_INhanVien.Update(NhanVien);
        }
    }
}
