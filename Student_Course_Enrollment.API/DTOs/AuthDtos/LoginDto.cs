using System.ComponentModel.DataAnnotations;

namespace Student_Course_Enrollment.API.DTOs.AuthDtos
{
    public class LoginDto
    {
        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}