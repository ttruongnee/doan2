using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllersKhachHang : ControllerBase
    {
        private readonly BLL_IKhachHang bLL_IKhachHang;

        public ControllersKhachHang(BLL_IKhachHang bLL_IKhachHang)
        {
            this.bLL_IKhachHang = bLL_IKhachHang;
        }

        [Route("Get-All")]
        [HttpGet]
        public List<Model_KhachHang> GetAll()
        {
            return bLL_IKhachHang.GetAll();
        }

        [Route("Update")]
        [HttpPut]
        public void Update(Model_KhachHang KhachHang)
        {
            bLL_IKhachHang.Update(KhachHang);
        }

        [Route("Search/{TenKhachHang}")]
        [HttpGet]
        public List<Model_KhachHang> Search(string TenKhachHang)
        {
            return bLL_IKhachHang.Search(TenKhachHang);
        }
    }
}
