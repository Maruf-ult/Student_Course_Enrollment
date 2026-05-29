using Student_Course_Enrollment.API.DTOs.CourseDtos;
using Student_Course_Enrollment.API.Models;

namespace Student_Course_Enrollment.API.Services.Interfaces
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(Guid id);
        Task<Course> CreateAsync(AddCourseDto request);
        Task<Course?> UpdateAsync(Guid id, UpdateCourseDto request);
        Task<Course?> DeleteAsync(Guid id);
    }
}
