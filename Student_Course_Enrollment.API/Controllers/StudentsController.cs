using Microsoft.AspNetCore.Mvc;
using Student_Course_Enrollment.API.DTOs.EnrollmentDtos;
using Student_Course_Enrollment.API.DTOs.StudentDtos;
using Student_Course_Enrollment.API.Models;
using Student_Course_Enrollment.API.Repositories.Interfaces;
using Student_Course_Enrollment.API.Services.Interfaces;

namespace Student_Course_Enrollment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllAsync();

            var dto = students.Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Enrollments = s.Enrollments.Select(e => new EnrollmentDto
                {
                    StudentId = e.StudentId,
                    CourseId = e.CourseId,
                    EnrollmentDate = e.EnrollmentDate,
                    Grade = e.Grade,
                    Student = s.Name,
                    Course = e.Course?.Title
                }).ToList()
            }).ToList();

            return Ok(dto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetStudentById([FromRoute] Guid id)
        {
            var student = await _studentService.GetByIdAsync(id);

            if (student == null)
                return NotFound();

            var dto = new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Enrollments = student.Enrollments.Select(e => new EnrollmentDto
                {
                    StudentId = e.StudentId,
                    CourseId = e.CourseId,
                    EnrollmentDate = e.EnrollmentDate,
                    Grade = e.Grade,
                    Student = student.Name,
                    Course = e.Course?.Title
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] AddStudentDto request)
        {
            var student = await _studentService.CreateAsync(request);

            var dto = new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Enrollments = new List<EnrollmentDto>()
            };

            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, dto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] Guid id, [FromBody] UpdateStudentDto request)
        {
          

            var student = await _studentService.UpdateAsync(id, request);

            if (student == null)
                return NotFound();

            var dto = new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Enrollments = new List<EnrollmentDto>()
            };

            return Ok(dto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] Guid id)
        {
            var student = await _studentService.DeleteAsync(id);

            if (student == null)
                return NotFound();

            return NoContent();
        }
    }
}