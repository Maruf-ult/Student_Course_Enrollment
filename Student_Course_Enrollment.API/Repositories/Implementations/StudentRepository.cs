using Microsoft.EntityFrameworkCore;
using Student_Course_Enrollment.API.Data;
using Student_Course_Enrollment.API.Models;
using Student_Course_Enrollment.API.Repositories.Interfaces;

namespace Student_Course_Enrollment.API.Repositories.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext dbContext;

        public StudentRepository(StudentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await dbContext.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(Guid id)
        {
            return await dbContext.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Student> CreateAsync(Student student)
        {
            await dbContext.Students.AddAsync(student);

            await dbContext.SaveChangesAsync();

            return student;
        }

        public async Task<Student?> UpdateAsync(Guid id, Student student)
        {
            var existingStudent = await dbContext.Students
                .FirstOrDefaultAsync(s => s.Id == id);

            if (existingStudent == null)
                return null;

            existingStudent.Name = student.Name;
            existingStudent.Email = student.Email;

            await dbContext.SaveChangesAsync();

            return existingStudent;
        }

        public async Task<Student?> DeleteAsync(Guid id)
        {
            var existingStudent = await dbContext.Students
                .FirstOrDefaultAsync(s => s.Id == id);

            if (existingStudent == null)
                return null;

            dbContext.Students.Remove(existingStudent);

            await dbContext.SaveChangesAsync();

            return existingStudent;
        }
    }
}