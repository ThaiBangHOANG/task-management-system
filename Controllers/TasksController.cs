using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.API.Models;
using TaskManagementSystem.API.Services;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskItem>> GetTasks()
        {
            var tasks = _taskService.GetAll();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetTaskById(int id)
        {
            var task = _taskService.GetById(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public ActionResult<TaskItem> CreateTask(TaskItem newTask)
        {
            var createdTask = _taskService.Create(newTask);

            return CreatedAtAction(nameof(GetTaskById), new { id = newTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TaskItem updatedTask)
        {
            var updated = _taskService.Update(id, updatedTask);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var deleted = _taskService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPatch("{id}/complete")]
        public IActionResult MarkTaskAsCompleted(int id)
        {
            var marked = _taskService.MarkAsCompleted(id);
            if (!marked)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}