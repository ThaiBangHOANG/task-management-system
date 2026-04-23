using TaskManagementSystem.API.Models;
using TaskManagementSystem.Data;

namespace TaskManagementSystem.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TaskItem> GetAll(int userId)
        {
            return _context.Tasks.Where(t => t.Id == userId).ToList();
        }

        public TaskItem? GetById(int id)
        {
            return _context.Tasks.Find(id);
        }

        public TaskItem Create(TaskItem newTask)
        {
            _context.Tasks.Add(newTask);
            newTask.CreatedAt = DateTime.UtcNow;
            newTask.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return newTask;
        }

        public bool Update(TaskItem updatedTask)
        {
            var existingTask = _context.Tasks.Find(updatedTask.Id);

            if (existingTask == null)
            {
                return false;
            }

            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.Status = updatedTask.Status;
            existingTask.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task != null)
            {
                _context.Tasks.Remove(task);    
                _context.SaveChanges();
            }

            return true;
        }

        public bool MarkAsCompleted(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return false;
            }

            task.IsCompleted = true;
            task.Status = "Done";
            task.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return true;
        }

    }
}
