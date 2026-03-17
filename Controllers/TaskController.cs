using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.API.Models;

namespace TaskManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<TaskItem>> GetTasks()
        {
            var tasks = new List<TaskItem>
            {
                new TaskItem
                {
                    Id = 1,
                    Title = "Learn .NET Web API",
                    Description = "Create first API endpoint",
                    Status = "In Progress"
                }
            };

            return Ok(tasks);
        }
    }
}