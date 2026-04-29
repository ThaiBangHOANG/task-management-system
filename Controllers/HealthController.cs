using Microsoft.AspNetCore.Mvc;

namespace TaskManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Check()
        {
            return Ok(new
            {
                status = "Healthy",
                message = "API is running",
                time = DateTime.UtcNow
            });
        }
    }
}