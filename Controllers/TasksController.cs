using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.API.Models;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private static List<TaskItem> tasks = new List<TaskItem>
        {
            new TaskItem
            {
                Id = 1,
                Title = "Learn ASP.NET Core",
                Description = "Understand controller and endpoints",
                IsCompleted = false
            },
            new TaskItem
            {
                Id = 2,
                Title = "Build Task API",
                Description = "Create CRUD endpoints",
                IsCompleted = true
            }
        };

        [HttpGet]
        public ActionResult<IEnumerable<TaskItem>> GetTasks()
        {
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetTaskById(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public ActionResult<TaskItem> CreateTask(TaskItem newTask)
        {
            newTask.Id = tasks.Max(t => t.Id) + 1;
            tasks.Add(newTask);

            return CreatedAtAction(nameof(GetTaskById), new { id = newTask.Id }, newTask);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TaskItem updatedTask)
        {
            var existingTask = tasks.FirstOrDefault(t => t.Id == id);

            if (existingTask == null)
            {
                return NotFound();
            }

            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.IsCompleted = updatedTask.IsCompleted;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            tasks.Remove(task);

            return NoContent();
        }
    }
}