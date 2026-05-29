using Microsoft.EntityFrameworkCore;
using Student_Course_Enrollment.API.Data;
using Student_Course_Enrollment.API.Models;
using Student_Course_Enrollment.API.Repositories.Interfaces;

namespace Student_Course_Enrollment.API.Repositories.Implementations
{
    public class CourseRepository:ICourseRepository
    {
        private readonly StudentDbContext dbContext;

        public CourseRepository(StudentDbContext dbContext)
        {
            this.dbContext = dbContext;    
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await dbContext.Courses
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Student)
                .ToListAsync();
        }

        public async Task<Course?>GetByIdAsync(Guid id)
        {
            return await dbContext.Courses
               .Include(s => s.Enrollments)
               .ThenInclude(e => e.Student)
               .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course> CreateAsync(Course course)
        {
            await dbContext.Courses.AddAsync(course);
            await dbContext.SaveChangesAsync();

            return course;
        }

        public async Task<Course?>UpdateAsync(Guid id,Course course)
        {
            var existingCourse = await dbContext.Courses
                .FirstOrDefaultAsync(c => c.Id == id);
            if (existingCourse == null)
                return null;
            existingCourse.Title = course.Title;
            existingCourse.Description = course.Description;
            await dbContext.SaveChangesAsync();
            return existingCourse;
        }

        public async Task<Course?> DeleteAsync(Guid id)
        {
            var existingCourse = await dbContext.Courses
                .FirstOrDefaultAsync(c => c.Id == id);
            if (existingCourse == null)
                return null;
            dbContext.Courses.Remove(existingCourse);
            await dbContext.SaveChangesAsync();
            return existingCourse;
        }
    }
}
