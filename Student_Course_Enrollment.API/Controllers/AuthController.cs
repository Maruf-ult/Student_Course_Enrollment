using Microsoft.AspNetCore.Mvc;
using Student_Course_Enrollment.API.DTOs.AuthDtos;
using Student_Course_Enrollment.API.Services.Interfaces;

namespace Student_Course_Enrollment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterDto request)
        {
            var result =
                await _authService.RegisterAsync(request);

            if (!result)
            {
                return BadRequest("Email already exists.");
            }

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginDto request)
        {
            var token =
                await _authService.LoginAsync(request);

            if (token == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            return Ok(new
            {
                Token = token
            });
        }
    }
}