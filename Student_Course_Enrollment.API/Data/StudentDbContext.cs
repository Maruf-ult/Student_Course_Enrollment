using Microsoft.EntityFrameworkCore;
using Student_Course_Enrollment.API.Models;

namespace Student_Course_Enrollment.API.Data
{
    public class StudentDbContext:DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course>Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<User>Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
            .HasKey(e => new 
            {
             e.StudentId,
             e.CourseId 
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
