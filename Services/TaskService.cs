using Microsoft.EntityFrameworkCore;
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
            return _context.Tasks.Where(t => t.UserId == userId).ToList();
        }

        public async Task<List<TaskItem>> GetAllByUser(int userId)
        {
            return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
        }

        public TaskItem? GetById(int id, int userId)
        {
            return _context.Tasks.FirstOrDefault(t => t.Id == id && t.UserId == userId);
        }

        public TaskItem Create(TaskItem newTask)
        {
            _context.Tasks.Add(newTask);
            newTask.CreatedAt = DateTime.UtcNow;
            newTask.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return newTask;
        }

        public bool Update(int id, TaskItem updatedTask, int userId)
        {
            var existingTask = _context.Tasks.FirstOrDefault(t => t.Id == id && t.UserId == userId);

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

        public bool Delete(int id, int userId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id && t.UserId == userId);

            if (task != null)
            {
                _context.Tasks.Remove(task);    
                _context.SaveChanges();
            }

            return true;
        }

        public bool MarkAsCompleted(int id, int userId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id && t.UserId == userId);

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
