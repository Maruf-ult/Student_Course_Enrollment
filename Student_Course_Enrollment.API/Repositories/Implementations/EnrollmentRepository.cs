using Microsoft.EntityFrameworkCore;
using Student_Course_Enrollment.API.Data;
using Student_Course_Enrollment.API.Models;
using Student_Course_Enrollment.API.Repositories.Interfaces;

namespace Student_Course_Enrollment.API.Repositories.Implementations
{
    public class EnrollmentRepository:IEnrollmentRepository
    {
        private readonly StudentDbContext dbContext;

        public EnrollmentRepository(StudentDbContext dbContext)
        {
            this.dbContext = dbContext;   
        }

        public async Task<List<Enrollment>> GetAllAsync()
        {
            return await dbContext.Enrollments
                             .Include(e => e.Student)
                             .Include(e => e.Course)
                             .ToListAsync();
        }

        public async Task<Enrollment?> GetByStudentAndCourseAsync(Guid studentId, Guid courseId)
        {
            return await dbContext.Enrollments
                            .Include(e => e.Student)
                            .Include(e => e.Course)
                            .FirstOrDefaultAsync(e =>
                             e.StudentId == studentId &&
                             e.CourseId == courseId);
        }
        public async Task<Enrollment> CreateAsync(Enrollment enrollment)
        {
            dbContext.Enrollments.Add(enrollment);
            await dbContext.SaveChangesAsync();
            return enrollment;
        }

        public async Task<Enrollment?> UpdateAsync(Guid studentId, Guid courseId, Enrollment enrollment)
        {
            var existingEnrollment = await GetByStudentAndCourseAsync(studentId, courseId);
            if (existingEnrollment == null)
            {
                return null;
            }
            existingEnrollment.EnrollmentDate = enrollment.EnrollmentDate;
            existingEnrollment.Grade = enrollment.Grade;
            await dbContext.SaveChangesAsync();
            return existingEnrollment;
        }

        public async Task<Enrollment?> DeleteAsync(Guid studentId, Guid courseId)
        {
            var existingEnrollment = await GetByStudentAndCourseAsync(studentId, courseId);
            if (existingEnrollment == null)
            {
                return null;
            }
            dbContext.Enrollments.Remove(existingEnrollment);
            await dbContext.SaveChangesAsync();
            return existingEnrollment;
        }



    }
}
