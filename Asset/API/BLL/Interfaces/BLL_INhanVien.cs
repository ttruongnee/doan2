﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface BLL_INhanVien
    {
        List<Model_NhanVien> GetAll();
        bool Update(Model_NhanVien NhanVien);
        List<Model_NhanVien> Search(string tenNhanVien);
        List<Model_NhanVien> GetNhanVienByUser(string user);
    }
}
