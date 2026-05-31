using Student_Course_Enrollment.API.DTOs.AuthDtos;
using Student_Course_Enrollment.API.Models;
using Student_Course_Enrollment.API.Repositories.Interfaces;
using Student_Course_Enrollment.API.Services.Interfaces;

namespace Student_Course_Enrollment.API.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public AuthService(
            IUserRepository userRepository,
            IPasswordService passwordService,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public async Task<bool> RegisterAsync(RegisterDto request)
        {
            var existingUser =
                await _userRepository.GetByEmailAsync(request.Email);

            if (existingUser != null)
            {
                return false;
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Role = request.Role,
                PasswordHash =_passwordService.HashPassword(request.Password)
            };

            await _userRepository.CreateAsync(user);

            return true;
        }

        public async Task<string?> LoginAsync(LoginDto request)
        {
            var user =
                await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                return null;
            }

            var isValidPassword =
                _passwordService.VerifyPassword(
                    request.Password,
                    user.PasswordHash);

            if (!isValidPassword)
            {
                return null;
            }

            return _tokenService.CreateToken(user);
        }
    }
}