

namespace Student_Course_Enrollment.API.DTOs.EnrollmentDtos
{
    public class AddEnrollmentDto
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public Decimal? Grade { get; set; }
        
    }
}
