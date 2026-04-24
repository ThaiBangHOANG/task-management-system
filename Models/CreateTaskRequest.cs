using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.API.Models
{
    public class CreateTaskRequest
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
