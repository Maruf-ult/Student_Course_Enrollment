
using Student_Course_Enrollment.API.DTOs.EnrollmentDtos;

namespace Student_Course_Enrollment.API.DTOs.CourseDtos
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public List<EnrollmentDto> Enrollments { get; set; } = new List<EnrollmentDto>();
    }
}
