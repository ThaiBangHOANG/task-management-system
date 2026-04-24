using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.API.Models;
using TaskManagementSystem.Data;
using TaskStatusEnum = TaskManagementSystem.API.Enums.TaskStatus;

namespace TaskManagementSystem.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TaskItem> GetAllTask(
            int userId, 
            int page, 
            int pageSize, 
            string? search,
            TaskStatusEnum? status,
            bool? isCompleted
        )
        {
            var query = _context.Tasks
         .Where(t => t.UserId == userId)
         .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t =>
                    t.Title.Contains(search) ||
                    t.Description.Contains(search)
                );
            }

            if (status.HasValue)
            {
                query = query.Where(t =>
                    t.Status == status.Value
                );
            }

            if (isCompleted.HasValue)
            {
                query = query.Where(t =>
                    t.IsCompleted == isCompleted.Value
                );
            }

            return query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public async Task<List<TaskItem>> GetAllByUser(int userId)
        {
            return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
        }

        public TaskItem? GetTaskById(int id, int userId)
        {
            return _context.Tasks.FirstOrDefault(t => t.Id == id && t.UserId == userId);
        }

        public TaskItem CreateTask(TaskItem newTask)
        {
            _context.Tasks.Add(newTask);
            newTask.CreatedAt = DateTime.UtcNow;
            newTask.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return newTask;
        }

        public bool UpdateTask(int id, UpdateTaskRequest request, int userId)
        {
            var existingTask = _context.Tasks.FirstOrDefault(t => t.Id == id && t.UserId == userId);

            if (existingTask == null)
            {
                return false;
            }

            existingTask.Title = request.Title;
            existingTask.Description = request.Description;
            existingTask.Status = request.Status;

            existingTask.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return true;
        }

        public bool DeleteTask(int id, int userId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id && t.UserId == userId);

            if (task != null)
            {
                _context.Tasks.Remove(task);    
                _context.SaveChanges();
            }

            return true;
        }

        public bool MarkTaskAsCompleted(int id, int userId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id && t.UserId == userId);

            if (task == null)
            {
                return false;
            }

            task.IsCompleted = true;
            task.Status = Enums.TaskStatus.Pending;

            task.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return true;
        }

    }
}
