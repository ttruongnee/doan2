using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllersNhanVien : ControllerBase
    {
        private readonly BLL_INhanVien bLL_INhanVien;

        public ControllersNhanVien(BLL_INhanVien bLL_INhanVien)
        {
            this.bLL_INhanVien = bLL_INhanVien;
        }

        [Route("Get-All")]
        [HttpGet]
        public List<Model_NhanVien> GetAll()
        {
            return bLL_INhanVien.GetAll();
        }

        [Route("Update")]
        [HttpPut]
        public void Update(Model_NhanVien NhanVien)
        {
            bLL_INhanVien.Update(NhanVien);
        }

        [Route("Search/{TenNhanVien}")]
        [HttpGet]
        public List<Model_NhanVien> Search(string TenNhanVien)
        {
            return bLL_INhanVien.Search(TenNhanVien);
        }

        [Route("GetNhanVienByUser/{user}")]
        [HttpGet]
        public List<Model_NhanVien> GetNhanVienByUser(string user)
        {
            return bLL_INhanVien.GetNhanVienByUser(user);
        }
    }
}
