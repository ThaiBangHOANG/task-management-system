using TaskManagementSystem.API.Models;

namespace TaskManagementSystem.API.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskItem> GetAll(int userId);
        TaskItem? GetById(int id, int userId);
        TaskItem Create(TaskItem newTask);
        bool Update(int id, TaskItem updatedTask, int userId);
        bool Delete(int id, int userId);
        bool MarkAsCompleted(int id, int userId);

    }
}
