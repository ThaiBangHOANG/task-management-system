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
        private readonly ILogger<TasksController> _logger;

        public TasksController(ITaskService taskService, ILogger<TasksController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskItem>> GetTasks()
        { 
            _logger.LogInformation("Retrieving all tasks");

            var tasks = _taskService.GetAll();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetTaskById(int id)
        {
            try
            {

                var task = _taskService.GetById(id);

                if (task == null)
                {
                    return NotFound();
                }

                return Ok(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving task with ID {TaskId}", id);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An error occurred while retrieving the task."
                );
            }
        }

        [HttpPost]
        public ActionResult<TaskItem> CreateTask(TaskItem newTask)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdTask = _taskService.Create(newTask);

                return CreatedAtAction(
                     nameof(GetTaskById),
                     new { id = newTask.Id },
                     createdTask
                 );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    ex.Message
                );
            }
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