using Student_Course_Enrollment.API.Models;

namespace Student_Course_Enrollment.API.Repositories.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<List<Enrollment>> GetAllAsync();
        Task<Enrollment?> GetByStudentAndCourseAsync(Guid studentId, Guid courseId);
        Task<Enrollment> CreateAsync(Enrollment enrollment);
        Task<Enrollment?> UpdateAsync(Guid studentId, Guid courseId, Enrollment enrollment);
        Task<Enrollment?> DeleteAsync(Guid studentId, Guid courseId);
    }
}
