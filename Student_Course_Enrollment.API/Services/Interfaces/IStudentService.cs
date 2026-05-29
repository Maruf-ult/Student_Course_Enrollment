using Student_Course_Enrollment.API.DTOs.StudentDtos;
using Student_Course_Enrollment.API.Models;

namespace Student_Course_Enrollment.API.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(Guid id);
        Task<Student> CreateAsync(AddStudentDto request);
        Task<Student?> UpdateAsync(Guid id, UpdateStudentDto request);
        Task<Student?> DeleteAsync(Guid id);
    }
}
