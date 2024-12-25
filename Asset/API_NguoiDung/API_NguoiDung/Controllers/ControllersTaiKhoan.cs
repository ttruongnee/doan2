using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllersTaiKhoan : ControllerBase
    {
        private readonly BLL_ITaiKhoan bLL_ITaiKhoan;

        private readonly EmailHelper _emailHelper = new EmailHelper();
        private static Dictionary<string, string> otpStore = new Dictionary<string, string>(); // Lưu OTP tạm thời

        [HttpPost("SendOTP")]
        public async Task<IActionResult> SendOTP([FromBody] string email)
        {

            // Tạo mã OTP
            var otp = new Random().Next(100000, 999999).ToString();

            // Lưu mã OTP tạm thời
            if (otpStore.ContainsKey(email))
            {
                otpStore[email] = otp;
            }
            else
            {
                otpStore.Add(email, otp);
            }

            // Gửi email
            string subject = "Mã OTP xác thực quên mật khẩu";
            string body = $"Mã OTP của bạn là: {otp}. Mã này sẽ hết hạn sau 10 phút.";
            await _emailHelper.SendEmailAsync(email, subject, body);

            return Ok("Mã OTP đã được gửi.");
        }



        [HttpPost("VerifyOTP")]
        public IActionResult VerifyOTP([FromBody] string otp)
        {
            // Tìm email với OTP trong store
            var email = otpStore.FirstOrDefault(x => x.Value == otp).Key;

            if (email != null)
            {
                otpStore.Remove(email); // Xoá OTP sau khi xác nhận
                return Ok("Xác nhận OTP thành công.");
            }

            return BadRequest("Mã OTP không chính xác hoặc đã hết hạn.");
        }


        public ControllersTaiKhoan(BLL_ITaiKhoan bLL_ITaiKhoan)
        {
            this.bLL_ITaiKhoan = bLL_ITaiKhoan;
        }

        [Route("Get-EmailKhachHang")]
        [HttpGet]
        public List<Model_TaiKhoanUpdateMatKhau> GetEmailKhachHang()
        {
            return bLL_ITaiKhoan.GetEmailKhachHang();
        }

        [Route("Get-MaKhachHang/{TaiKhoan}")]
        [HttpGet]
        public List<Model_TaiKhoan> GetMaKhachHang(string TaiKhoan)
        {
            return bLL_ITaiKhoan.GetMaKhachHang(TaiKhoan);
        }

        [Route("DangNhapUser")]
        [HttpPost]
        public bool DangNhapUser(Model_TaiKhoan taikhoan)
        {
            return bLL_ITaiKhoan.DangNhapUser(taikhoan);
        }

        [Route("Create")]
        [HttpPost]
        public void Create([FromBody] Model_CreateTaiKhoan TaiKhoan)
        {
            bLL_ITaiKhoan.Create(TaiKhoan);
        }


        [Route("UpdateMatKhau")]
        [HttpPut]
        public void Update(Model_TaiKhoanUpdateMatKhau TaiKhoan)
        {
            bLL_ITaiKhoan.UpdateMatKhau(TaiKhoan);
        }

    }
}
