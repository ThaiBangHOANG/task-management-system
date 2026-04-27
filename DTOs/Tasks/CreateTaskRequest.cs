using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.API.DTOs.Tasks
{
    public class CreateTaskRequest
    {
        [Required(ErrorMessage ="Title is required")]
        [MaxLength(100, ErrorMessage ="Max length of the title is 100")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Max length of the description is 500")]
        public string Description { get; set; } = string.Empty;
    }
}
