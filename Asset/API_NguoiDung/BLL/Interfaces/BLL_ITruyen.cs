using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface BLL_ITruyen
    {
        List<Model_Truyen> GetAll();

        //bool uploadPoster(IFormFile file); 
        IFormFile getPoster(string fileName);
        List<Model_Truyen> SearchByID(string id);
        List<Model_Truyen> SearchByName(string name);
        List<Model_Truyen> GetTruyenByTheLoai(string idorname);
    }
}
