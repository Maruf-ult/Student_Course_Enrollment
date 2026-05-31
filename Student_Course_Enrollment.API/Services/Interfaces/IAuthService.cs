using Student_Course_Enrollment.API.DTOs.AuthDtos;

namespace Student_Course_Enrollment.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto request);

        Task<string?> LoginAsync(LoginDto request);
    }
}