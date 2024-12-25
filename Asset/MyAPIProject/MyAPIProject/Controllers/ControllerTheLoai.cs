using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL;
using DTO;
using DAL;

namespace MyAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerTheLoai : ControllerBase
    {
        private readonly BLLTheLoai _bllTheLoai;

        public ControllerTheLoai(IConfiguration configuration)
        {
            string connectionString = configuration["ConnectionStrings:DefaultConnection"];
            var dalTheLoai = new DALTheLoai(connectionString);
            _bllTheLoai = new BLLTheLoai(dalTheLoai); 
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllTheLoai()
        {
            var theLoais = _bllTheLoai.GetAllTheLoai();
            return Ok(theLoais);
        }

        [HttpPost("Add")]
        public IActionResult AddTheLoai([FromBody] DTOTheLoai theLoai)
        {
            if (theLoai == null)
            {
                return BadRequest();
            }

            _bllTheLoai.AddTheLoai(theLoai);
            return Ok();
        }

        [HttpPut("Update/{maTheLoai}")]
        public IActionResult UpdateTheLoai(string maTheLoai, [FromBody] DTOTheLoai theLoai)
        {
            if (theLoai == null || theLoai.MaTheLoai != maTheLoai)
            {
                return BadRequest();
            }

            var existingTheLoai = _bllTheLoai.GetTheLoaiById(maTheLoai);
            if (existingTheLoai == null)
            {
                return NotFound("TheLoai not found.");
            }

            _bllTheLoai.UpdateTheLoai(theLoai);
            return Ok();
        }

        [HttpDelete("Delete/{maTheLoai}")]
        public IActionResult DeleteTheLoai(string maTheLoai)
        {
            var existingTheLoai = _bllTheLoai.GetTheLoaiById(maTheLoai);
            if (existingTheLoai == null)
            {
                return NotFound("TheLoai not found.");
            }

            _bllTheLoai.DeleteTheLoai(maTheLoai);
            return Ok();
        }

        [HttpGet("Find/{maTheLoai}")]
        public IActionResult GetTheLoaiById(string maTheLoai)
        {
            var theLoai = _bllTheLoai.GetTheLoaiById(maTheLoai);
            if (theLoai == null)
            {
                return NotFound("TheLoai not found.");
            }

            return Ok(theLoai);
        }

    }
}
