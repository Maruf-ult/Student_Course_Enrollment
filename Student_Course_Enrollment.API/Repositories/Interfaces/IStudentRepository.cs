using Student_Course_Enrollment.API.Models;

namespace Student_Course_Enrollment.API.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(Guid id);
        Task<Student>CreateAsync(Student student);
        Task<Student?> UpdateAsync(Guid id, Student student);
        Task<Student?> DeleteAsync(Guid id);
    }
}
