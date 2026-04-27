using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.API.Models
{
    public class RegisterRequest
    {
        [Required]
        public string Username  { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }
    }
}
