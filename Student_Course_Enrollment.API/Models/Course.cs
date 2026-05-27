

namespace Student_Course_Enrollment.API.Models
{
    public class Course
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }
        public required string Description { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
