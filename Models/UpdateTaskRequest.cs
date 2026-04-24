
using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.API.Models
{
    public class UpdateTaskRequest
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public Enums.TaskStatus Status { get; set; }
    }
}
