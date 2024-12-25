using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface DAL_ITruyen
    {
        List<Model_Truyen> GetAll();
        string getFolderPath();
        List<Model_Truyen> SearchByID(string id);
        List<Model_Truyen> SearchByName(string name);
        List<Model_Truyen> GetTruyenByTheLoai(string idorname);
    }
}
