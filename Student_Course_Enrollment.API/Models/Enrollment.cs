namespace Student_Course_Enrollment.API.Models
{
    public class Enrollment
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public Decimal Grade { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
