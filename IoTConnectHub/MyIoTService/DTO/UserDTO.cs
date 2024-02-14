using System.ComponentModel.DataAnnotations;

namespace MyIoTService.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(255, ErrorMessage = "Password cannot be longer than 255 characters.")]
        public string Password { get; set; } = string.Empty;
    }
}
