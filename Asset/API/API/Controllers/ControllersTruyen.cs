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

        [Route("UploadImage")]
        [HttpPost]
        public bool UploadImage(IFormFile img)
        {
            return bLL_ITruyen.uploadPoster(img);
        }

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

        [Route("Create")]
        [HttpPost]
        public void Create([FromBody] Model_Truyen Truyen)
        {
            bLL_ITruyen.Create(Truyen);

        }

        [Route("Update")]
        [HttpPut]
        public void Update(Model_Truyen Truyen)
        {
            bLL_ITruyen.Update(Truyen);
        }

        [Route("Delete/{MaTruyen}")]
        [HttpDelete]
        public void Delete(string MaTruyen)
        {
            bLL_ITruyen.Delete(MaTruyen);
        }

        [Route("Search/{Search}")]
        [HttpGet]
        public List<Model_Truyen> Search(string Search)
        {
            return bLL_ITruyen.Search(Search);
        }

    }
}
