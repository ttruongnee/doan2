using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Truyen : BLL_ITruyen
    {
        private DAL_ITruyen dAL_ITruyen;

        public BLL_Truyen(DAL_ITruyen dAL_ITruyen)
        {
            this.dAL_ITruyen = dAL_ITruyen;
        }

        public List<Model_Truyen> GetAll()
        {
            return dAL_ITruyen.GetAll();
        }

        public IFormFile getPoster(string fileName)
        {
            try
            {
                string path = Path.Combine(dAL_ITruyen.getFolderPath(), fileName);

                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("File không tồn tại.");
                }
                var memoryStream = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    stream.CopyTo(memoryStream);
                }
                memoryStream.Position = 0;
                IFormFile formFile = new FormFile(memoryStream, 0, memoryStream.Length, fileName, fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/octet-stream"
                };
                return formFile;
            }
            catch
            {
                return null;
            }
        }
        public List<Model_Truyen> SearchByID(string id)
        {
            return dAL_ITruyen.SearchByID(id);
        }

        public List<Model_Truyen> SearchByName(string name)
        {
            return dAL_ITruyen.SearchByName(name);
        }
        public List<Model_Truyen> GetTruyenByTheLoai(string idorname)
        {
            return dAL_ITruyen.GetTruyenByTheLoai(idorname);
        }

    }
}
