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

        [Route("Get-All")]
        [HttpGet]
        public List<Model_GiamGia> GetAll()
        {
            return bLL_IGiamGia.GetAll();
        }

        [Route("Create")]
        [HttpPost]
        public void Create([FromBody] Model_GiamGia GiamGia)
        {
            bLL_IGiamGia.Create(GiamGia);
        }

        [Route("Update")]
        [HttpPut]
        public void Update(Model_GiamGia GiamGia)
        {
            bLL_IGiamGia.Update(GiamGia);
        }

        [Route("Delete/{MaGiamGia}")]
        [HttpDelete]
        public void Delete(string MaGiamGia)
        {
            bLL_IGiamGia.Delete(MaGiamGia);
        }

        [Route("Search")]
        [HttpGet]
        public List<Model_GiamGia> Search(DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            return bLL_IGiamGia.Search(NgayBatDau, NgayKetThuc);
        }

    }
}
