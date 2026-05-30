using Student_Course_Enrollment.API.DTOs.EnrollmentDtos;
using Student_Course_Enrollment.API.Models;
using Student_Course_Enrollment.API.Repositories.Interfaces;
using Student_Course_Enrollment.API.Services.Interfaces;

namespace Student_Course_Enrollment.API.Services.Implementations
{
    public class EnrollmentService:IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;    
        }

        public async Task<List<Enrollment>> GetAllAsync()
        {
            return await _enrollmentRepository.GetAllAsync();
        }

        public async Task<Enrollment?> GetByStudentAndCourseAsync(Guid studentId, Guid courseId)
        {
            return await _enrollmentRepository.GetByStudentAndCourseAsync(studentId, courseId);
        }

        public async Task<Enrollment> CreateAsync(AddEnrollmentDto enrollment)
        {
            var newEnrollment = new Enrollment
            {
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId,
                EnrollmentDate = enrollment.EnrollmentDate,
                Grade = enrollment.Grade ?? 0
            };

            return await _enrollmentRepository.CreateAsync(newEnrollment);
        }

        public async Task<Enrollment?> UpdateAsync(Guid studentId, Guid courseId, UpdateEnrollmentDto enrollment)
        {
            var updatedEnrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrollmentDate = enrollment.EnrollmentDate,
                Grade = enrollment.Grade ?? 0
            };
            return await _enrollmentRepository.UpdateAsync(studentId, courseId, updatedEnrollment);
        }

        public async Task<Enrollment?> DeleteAsync(Guid studentId, Guid courseId)
        {
            return await _enrollmentRepository.DeleteAsync(studentId, courseId);
        }

    }
}
