using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.API.DTOs.Auth
{
    public class RegisterRequest
    {
        [Required]
        public string Username  { get; set; } = null!;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = null!;
    }
}
