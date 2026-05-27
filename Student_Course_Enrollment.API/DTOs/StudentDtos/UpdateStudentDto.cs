using System.ComponentModel.DataAnnotations;

namespace Student_Course_Enrollment.API.DTOs.StudentDtos
{
    public class UpdateStudentDto
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
