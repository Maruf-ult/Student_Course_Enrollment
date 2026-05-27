using System.ComponentModel.DataAnnotations;

namespace Student_Course_Enrollment.API.DTOs.CourseDtos
{
    public class UpdateCourseDto
    {
        [Required]
        [MaxLength(400)]
        public required string Title { get; set; }

        [Required]
        [MaxLength(600)]
        public required string Description { get; set; }
        
    }
}
