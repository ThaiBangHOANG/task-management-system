
namespace TaskManagementSystem.API.Models
{
    public class UpdateTaskRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.Pending;
    }
}
