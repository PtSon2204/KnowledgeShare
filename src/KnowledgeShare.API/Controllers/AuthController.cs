using System.Security.Claims;
using KnowledgeShare.API.Services.Interface;
using KnowledgeShare.ViewModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeShare.API.Controllers
{      
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;
        public AuthController(ILoginService loginService, IRegisterService registerService)
        {
            _loginService = loginService;
            _registerService = registerService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginVm request)
        {
            var token = await _loginService.LoginAsync(request.Email, request.Password);

            if (token == null)
            {
                return Unauthorized(new { message = "Email hoặc mật khẩu không chính xác" });
            }

            return Ok(new
            {
                token = token,
                message = "Đăng nhập thành công"
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterVm request)
        {
            var result = await _registerService.RegisterAsync(request);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "Đăng ký thành công!" });
        }

        [HttpGet("profile")]
        public IActionResult Profile()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var userId = User.FindFirst("UserId")?.Value;
            var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value);

            return Ok(new
            {
                userId,
                email,
                roles
            });
        }

    }
}
