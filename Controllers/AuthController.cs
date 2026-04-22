using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

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
            string email,
            string password
        )
        {
            await _service.Register(email, password);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
    LoginRequest request
)
        {
            var token =
                await _service.Login(
                    request.Email,
                    request.Password
                );

            if (token == null)
                return Unauthorized();

            return Ok(new { token });
        }
    }
}
