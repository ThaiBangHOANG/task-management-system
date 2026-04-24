using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TaskManagementSystem.API.Enums;

namespace TaskManagementSystem.API.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.Pending;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

    }
}