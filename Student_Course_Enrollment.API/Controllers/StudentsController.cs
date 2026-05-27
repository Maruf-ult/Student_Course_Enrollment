using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Course_Enrollment.API.Data;
using Student_Course_Enrollment.API.DTOs.EnrollmentDtos;
using Student_Course_Enrollment.API.DTOs.StudentDtos;
using Student_Course_Enrollment.API.Models;

namespace Student_Course_Enrollment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        public readonly StudentDbContext dbContext;

        public StudentsController(StudentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllStudent()
        {
            var students = dbContext.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .ToList();

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
        public IActionResult GetStudentById([FromRoute] Guid id)
        {
            var student = dbContext.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .FirstOrDefault(s => s.Id == id);

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
        public IActionResult CreateStudent([FromBody] AddStudentDto request)
        {
            var student = new Student
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
            };
            dbContext.Students.Add(student);
            dbContext.SaveChanges();

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
        public IActionResult UpdateStudent([FromRoute] Guid id, [FromBody] UpdateStudentDto request)
        {
            var student = dbContext.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            student.Name = request.Name;
            student.Email = request.Email;
            dbContext.SaveChanges();

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
        public IActionResult DeleteStudent([FromRoute] Guid id)
        {
            var student = dbContext.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound();

            dbContext.Students.Remove(student);
            dbContext.SaveChanges();
            return NoContent();
        }


    }
}
