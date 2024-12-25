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


        [Route("Search/{TenTheLoai}")]
        [HttpGet]
        public List<Model_TheLoai> Search(string TenTheLoai)
        {
            return bLL_ITheLoai.Search(TenTheLoai);
        }

    }
}
