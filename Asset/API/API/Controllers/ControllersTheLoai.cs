using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllersTheLoai : ControllerBase
    {
        private readonly BLL_ITheLoai bLL_ITheLoai;

        public ControllersTheLoai(BLL_ITheLoai bLL_ITheLoai)
        {
            this.bLL_ITheLoai = bLL_ITheLoai;
        }

        [Route("Get-All")]
        [HttpGet]
        public List<Model_TheLoai> GetAll()
        {
            return bLL_ITheLoai.GetAll();
        }

        [Route("Create")]
        [HttpPost]
        public void Create([FromBody] Model_TheLoai TheLoai)
        {
            bLL_ITheLoai.Create(TheLoai);
        }

        [Route("Update")]
        [HttpPut]
        public void Update(Model_TheLoai TheLoai)
        {
            bLL_ITheLoai.Update(TheLoai);
        }

        [Route("Delete/{MaTheLoai}")]
        [HttpDelete]
        public void Delete(string MaTheLoai)
        {
            bLL_ITheLoai.Delete(MaTheLoai);
        }


        [Route("Search/{TenTheLoai}")]
        [HttpGet]
        public List<Model_TheLoai> Search(string TenTheLoai)
        {
            return bLL_ITheLoai.Search(TenTheLoai);
        }

    }
}
