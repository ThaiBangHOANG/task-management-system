
using System.ComponentModel.DataAnnotations;
using TaskStatusEnum = TaskManagementSystem.API.Enums.TaskStatus;

namespace TaskManagementSystem.API.DTOs.Tasks
{
    public class UpdateTaskRequest
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public TaskStatusEnum Status { get; set; }
    }
}
