using Student_Course_Enrollment.API.Models;

namespace Student_Course_Enrollment.API.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
