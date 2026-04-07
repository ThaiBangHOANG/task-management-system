using TaskManagementSystem.API.Models;

namespace TaskManagementSystem.API.Services
{
    public interface ITaskService
    {
        List<TaskItem> GetAll();
        TaskItem GetById(int id);
        TaskItem Create(TaskItem newTask);
        bool Update(int id, TaskItem updatedTask);
        bool Delete(int id);
        bool MarkAsCompleted(int id);

    }
}
