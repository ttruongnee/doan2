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
        bool Create(Model_Truyen truyen);
        bool Update(Model_Truyen truyen);
        bool Delete(string idTruyen);
        List<Model_Truyen> Search(string tenTruyen);
    }
}
