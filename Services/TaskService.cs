using TaskManagementSystem.API.Models;

namespace TaskManagementSystem.API.Services
{
    public class TaskService : ITaskService
    {
        private static List<TaskItem> tasks = new List<TaskItem>
        {
            new TaskItem
            {
                Id = 1,
                Title = "ASP.NET Core",
                Description = "Controller and endpoints",
                Status = "To Do",
                IsCompleted = false
            },
            new TaskItem
            {
                Id = 2,
                Title = "Build Task API",
                Description = "Create CRUD endpoints",
                Status = "Done",
                IsCompleted = true
            }
        };

        public List<TaskItem> GetAll()
        {
            return tasks;
        }

        public TaskItem? GetById(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public TaskItem Create(TaskItem newTask)
        {
            newTask.Id = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
            newTask.CreatedAt = DateTime.UtcNow;
            newTask.UpdatedAt = DateTime.UtcNow;

            tasks.Add(newTask);
            return newTask;
        }

        public bool Update(int id, TaskItem updatedTask)
        {
            var existingTask = tasks.FirstOrDefault(t => t.Id == id);

            if (existingTask == null)
            {
                return false;
            }

            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.Status = updatedTask.Status;
            existingTask.UpdatedAt = DateTime.UtcNow;

            return true;
        }

        public bool Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return false;
            }

            tasks.Remove(task);

            return true;
        }

        public bool MarkAsCompleted(int id)
        {
            var task = tasks.FirstOrDefault(task => task.Id == id);

            if (task == null)
            {
                return false;
            }

            task.IsCompleted = true;
            task.Status = "Done";
            task.UpdatedAt = DateTime.UtcNow;

            return true;
        }

    }
}
