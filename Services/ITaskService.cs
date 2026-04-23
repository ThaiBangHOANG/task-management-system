using TaskManagementSystem.API.Models;

namespace TaskManagementSystem.API.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskItem> GetAll(int userId);
        TaskItem? GetById(int id);
        TaskItem Create(TaskItem newTask);
        bool Update(TaskItem updatedTask);
        bool Delete(int id);
        bool MarkAsCompleted(int id);

    }
}
