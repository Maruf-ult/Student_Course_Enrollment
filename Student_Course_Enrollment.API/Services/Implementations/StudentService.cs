using Student_Course_Enrollment.API.DTOs.StudentDtos;
using Student_Course_Enrollment.API.Models;
using Student_Course_Enrollment.API.Repositories.Interfaces;
using Student_Course_Enrollment.API.Services.Interfaces;

namespace Student_Course_Enrollment.API.Services.Repositories
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _studentRepository.GetAllAsync();
        }

        public async Task<Student?> GetByIdAsync(Guid id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task<Student> CreateAsync(AddStudentDto request)
        {
            var student = new Student
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email
            };

            return await _studentRepository.CreateAsync(student);
        }

        public async Task<Student?> UpdateAsync(Guid id, UpdateStudentDto request)
        {
            var updatedStudent = new Student
            {
                Name = request.Name,
                Email = request.Email
            };

            return await _studentRepository.UpdateAsync(id, updatedStudent);
        }

        public async Task<Student?> DeleteAsync(Guid id)
        {
            return await _studentRepository.DeleteAsync(id);
        }
    }
}