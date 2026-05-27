using Student_Course_Enrollment.API.DTOs.EnrollmentDtos;

namespace Student_Course_Enrollment.API.DTOs.StudentDtos
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public required string Email { get; set;  }
        public List<EnrollmentDto> Enrollments { get; set; } = new List<EnrollmentDto>();
    }
}
