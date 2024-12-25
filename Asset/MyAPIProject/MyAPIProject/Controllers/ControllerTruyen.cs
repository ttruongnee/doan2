using BLL;
using DAL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerTruyen : ControllerBase
    {
        private readonly BLLTruyen _bllTruyen;

        public ControllerTruyen(IConfiguration configuration)
        {
            string connectionString = configuration["ConnectionStrings:DefaultConnection"];
            var dalTruyen = new DALTruyen(connectionString);
            _bllTruyen = new BLLTruyen(dalTruyen); 
        }

        [HttpGet]
        public IActionResult GetAllTruyen()
        {
            List<DTOTruyen> truyens = _bllTruyen.GetAllTruyen();
            return Ok(truyens);
        }
    }
}
