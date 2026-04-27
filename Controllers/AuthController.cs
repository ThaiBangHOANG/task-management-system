using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.API.Models;

namespace TaskManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;

        public AuthController(AuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterRequest request
        )
        {
            await _service.Register(
                request.Username,
                request.Password
            );

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token =
                await _service.Login(request);

            if (token == null)
                return Unauthorized();

            return Ok(new { token });
        }
    }
}