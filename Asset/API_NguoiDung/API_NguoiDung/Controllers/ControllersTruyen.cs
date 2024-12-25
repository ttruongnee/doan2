using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllersTruyen : ControllerBase
    {
        private readonly BLL_ITruyen bLL_ITruyen;

        public ControllersTruyen(BLL_ITruyen bLL_ITruyen)
        {
            this.bLL_ITruyen = bLL_ITruyen;
        }

        [Route("Get-All")]
        [HttpGet]
        public List<Model_Truyen> GetAll()
        {
            return bLL_ITruyen.GetAll();
        }

        //[Route("UploadImage")]
        //[HttpPost]
        //public bool UploadImage(IFormFile img)
        //{
        //    return bLL_ITruyen.uploadPoster(img);
        //}

        [Route("get-poster/{fileName}")]
        [HttpGet]
        public IActionResult getPoster(string fileName)
        {
            IFormFile file = bLL_ITruyen.getPoster(fileName);
            if (file == null)
            { 
                return BadRequest(); 
            }
            return File(file.OpenReadStream(), file.ContentType, file.FileName);
        }

        [Route("SearchByID/{ID}")]
        [HttpGet]
        public List<Model_Truyen> Search(string ID)
        {
            return bLL_ITruyen.SearchByID(ID);
        }

        [Route("SearchByName/{Name}")]
        [HttpGet]
        public List<Model_Truyen> SearchByName(string Name)
        {
            return bLL_ITruyen.SearchByName(Name);
        }

        [Route("GetTruyenByTheLoai/{idorname}")]
        [HttpGet]
        public List<Model_Truyen> GetTruyenByTheLoai(string idorname)
        {
            return bLL_ITruyen.GetTruyenByTheLoai(idorname);
        }
    }
}
