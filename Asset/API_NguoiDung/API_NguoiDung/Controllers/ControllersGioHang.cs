using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API_NguoiDung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllersGioHang : ControllerBase
    {
        private readonly BLL_IGioHang bLL_IGioHang;

        public ControllersGioHang(BLL_IGioHang bLL_IGioHang)
        {
            this.bLL_IGioHang = bLL_IGioHang;
        }

        [Route("Create")]
        [HttpPost]
        public void Create([FromBody] Model_GioHang GioHang)
        {
            bLL_IGioHang.Create(GioHang);
        }


        [Route("Delete/{username}")]
        [HttpDelete]
        public void Delete(string username)
        {
            bLL_IGioHang.Delete(username);
        }


        [Route("GetGioHangByUser/{username}")]
        [HttpGet]
        public List<Model_GioHang> Search(string username)
        {
            return bLL_IGioHang.GetGioHangByUser(username);
        }

    }
}
