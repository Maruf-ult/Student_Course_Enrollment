using Student_Course_Enrollment.API.Models;

namespace Student_Course_Enrollment.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);

        Task<User> CreateAsync(User user);
    }
}