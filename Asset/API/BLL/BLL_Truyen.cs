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


        public bool Create(Model_Truyen Truyen)
        {
            return dAL_ITruyen.Create(Truyen);
        }

        public bool Delete(string idTruyen)
        {
            return dAL_ITruyen.Delete(idTruyen);
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

        public List<Model_Truyen> Search(string tenTruyen)
        {
            return dAL_ITruyen.Search(tenTruyen);
        }

        public bool Update(Model_Truyen Truyen)
        {
            return dAL_ITruyen.Update(Truyen);
        }

        public bool uploadPoster(IFormFile file)
        {
            try
            {
                string path = Path.Combine(dAL_ITruyen.getFolderPath());
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Kiểm tra định dạng file
                string[] allowedExtensions = { ".jpg", ".png" };
                if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                {
                    throw new Exception("Chỉ hỗ trợ định dạng JPG, PNG.");
                }

                string filePath = Path.Combine(path, file.FileName);
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fs);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
