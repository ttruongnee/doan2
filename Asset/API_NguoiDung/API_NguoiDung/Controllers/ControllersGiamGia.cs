using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllersGiamGia : ControllerBase
    {
        private readonly BLL_IGiamGia bLL_IGiamGia;

        public ControllersGiamGia(BLL_IGiamGia bLL_IGiamGia)
        {
            this.bLL_IGiamGia = bLL_IGiamGia;
        }

        [Route("ApplyMaGiamGia/{MaGiamGia}")]
        [HttpGet]
        public List<Model_GiamGia> ApplyMaGiamGia(string MaGiamGia)
        {
            return bLL_IGiamGia.ApplyMaGiamGia(MaGiamGia);
        }
    }
}
