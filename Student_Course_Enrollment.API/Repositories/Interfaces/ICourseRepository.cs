using Student_Course_Enrollment.API.Models;

namespace Student_Course_Enrollment.API.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(Guid id);
        Task<Course> CreateAsync(Course course);
        Task<Course?> UpdateAsync(Guid id, Course course);
        Task<Course?> DeleteAsync(Guid id);
    }
}
