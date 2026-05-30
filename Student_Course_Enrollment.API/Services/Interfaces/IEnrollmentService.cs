using Student_Course_Enrollment.API.DTOs.EnrollmentDtos;
using Student_Course_Enrollment.API.Models;

namespace Student_Course_Enrollment.API.Services.Interfaces
{
    public interface IEnrollmentService
    {
        Task<List<Enrollment>> GetAllAsync();
        Task<Enrollment?> GetByStudentAndCourseAsync(Guid studentId, Guid courseId);
        Task<Enrollment> CreateAsync(AddEnrollmentDto enrollment);
        Task<Enrollment?> UpdateAsync(Guid studentId, Guid courseId, UpdateEnrollmentDto enrollment);
        Task<Enrollment?> DeleteAsync(Guid studentId, Guid courseId);
    }
}
