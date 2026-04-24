using TaskManagementSystem.API.Models;
using TaskStatusEnum = TaskManagementSystem.API.Enums.TaskStatus;

namespace TaskManagementSystem.API.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskItem> GetAllTask(int userId, int page, int pageSize, string? search, TaskStatusEnum? status, bool? isCompleted);
        TaskItem? GetTaskById(int id, int userId);
        TaskItem CreateTask(TaskItem newTask);
        bool UpdateTask(int id, UpdateTaskRequest request, int userId);
        bool DeleteTask(int id, int userId);
        bool MarkTaskAsCompleted(int id, int userId);

    }
}
