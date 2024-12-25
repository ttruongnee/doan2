using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllersDonHang : ControllerBase
    {
        private readonly BLL_IDonHang bLL_IDonHang;

        public ControllersDonHang(BLL_IDonHang bLL_IDonHang)
        {
            this.bLL_IDonHang = bLL_IDonHang;
        }

        [Route("Get-All")]
        [HttpGet]
        public List<Model_DonHang> GetAll()
        {
            return bLL_IDonHang.GetAll();
        }

        [Route("Get-CTDonHangByID/{MaDonHang}")]
        [HttpGet]
        public List<Model_ChiTietDonHang> GetChiTietDonHangByID(string MaDonHang)
        {
            return bLL_IDonHang.GetChiTietDonHangByID(MaDonHang);
        }


        [Route("Update")]
        [HttpPut]
        public void Update(Model_DonHang DonHang)
        {
            bLL_IDonHang.Update(DonHang);
        }

        [Route("Delete/{MaDonHang}")]
        [HttpDelete]
        public void Delete(string MaDonHang)
        {
            bLL_IDonHang.Delete(MaDonHang);
        }

        [Route("Search/{search}")]
        [HttpGet]
        public List<Model_DonHang> Search(string search)
        {
            return bLL_IDonHang.Search(search);
        }
    }
}
