using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.API.Models;
using TaskManagementSystem.API.Services;
using System.Security.Claims;

namespace TaskManagementSystem.Controllers
{
    [Authorize]
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

        private int GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                throw new Exception("User ID not found in token.");
            }

            return int.Parse(userId);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskItem>> GetTasks()
        { 
            _logger.LogInformation("Retrieving all tasks");
            var userId = GetUserId();

            var tasks = _taskService.GetAll((userId));
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetTaskById(int id)
        {
            try
            {
                var userId = GetUserId();

                var task = _taskService.GetById(id, userId);

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
        public ActionResult<TaskItem> CreateTask(CreateTaskRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userId = GetUserId();

                var newTask = new TaskItem
                    {
                    Title = request.Title,
                    Description = request.Description,
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsCompleted = false,
                    UserId = userId
                };

                var createdTask = _taskService.Create(newTask);

                return CreatedAtAction(
                     nameof(GetTaskById),
                     new { id = createdTask.Id },
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
        public IActionResult UpdateTask(int id, UpdateTaskRequest request, int userId)
        {
            var updated = _taskService.Update(id, request, userId);

            if (!updated)
            {
                return NotFound();
            }

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var userId = GetUserId();

            var deleted = _taskService.Delete(id, userId);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPatch("{id}/complete")]
        public IActionResult MarkTaskAsCompleted(int id)
        {
            var userId = GetUserId();

            var marked = _taskService.MarkAsCompleted(id, userId);

            if (!marked)
            {
                return NotFound();
            }
            return Ok(marked);
        }
    }
}