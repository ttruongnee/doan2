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

        bool Create(Model_Truyen truyen);
        bool Update(Model_Truyen truyen);
        bool Delete(string idTruyen);
        bool uploadPoster(IFormFile file); // tự đặt tên
        IFormFile getPoster(string fileName);
        List<Model_Truyen> Search(string tenTruyen);
    }
}
