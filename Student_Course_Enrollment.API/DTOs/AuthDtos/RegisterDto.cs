using System.ComponentModel.DataAnnotations;

namespace Student_Course_Enrollment.API.DTOs.AuthDtos
{
    public class RegisterDto
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string Role { get; set; }
    }
}