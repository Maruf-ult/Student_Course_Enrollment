using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Course_Enrollment.API.Data;
using Student_Course_Enrollment.API.DTOs.CourseDtos;
using Student_Course_Enrollment.API.DTOs.EnrollmentDtos;
using Student_Course_Enrollment.API.Models;

namespace Student_Course_Enrollment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        public readonly StudentDbContext dbContext;
        public CoursesController(StudentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllCourses()
        {
            var courses = dbContext.Courses
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Student)
                .ToList();

            var dto = courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Enrollments = c.Enrollments.Select(e => new EnrollmentDto
                {
                    StudentId = e.StudentId,
                    CourseId = e.CourseId,
                    EnrollmentDate = e.EnrollmentDate,
                    Grade = e?.Grade,
                    Student = e?.Student.Name,
                    Course = e?.Course.Title
                }).ToList()
            }).ToList();

            return Ok(dto);
        }

        [HttpGet("{id:guid}")]

        public IActionResult GetCourseById([FromRoute] Guid id)
        {
            var course = dbContext.Courses
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Student)
                .FirstOrDefault(c => c.Id == id);

            if (course == null)
                return NotFound();

            var dto = new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Enrollments = course.Enrollments.Select(e => new EnrollmentDto
                {
                    StudentId = e.StudentId,
                    CourseId = e.CourseId,
                    EnrollmentDate = e.EnrollmentDate,
                    Grade = e?.Grade,
                    Student = e?.Student.Name,
                    Course = e?.Course.Title
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult AddCourse([FromBody] AddCourseDto request)
        {
            var course = new Course
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
            };

            dbContext.Courses.Add(course);
            dbContext.SaveChanges();

            var dto = new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Enrollments = new List<EnrollmentDto>()
            };

            return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, dto);

        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateCourse([FromRoute] Guid id, [FromBody] UpdateCourseDto request)
        {
            var course = dbContext.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
                return NotFound();
            course.Title = request.Title;
            course.Description = request.Description;
            dbContext.SaveChanges();
            var dto = new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Enrollments = new List<EnrollmentDto>()
            };
            return Ok(dto);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteCourse([FromRoute] Guid id)
        {
            var course = dbContext.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
                return NotFound();
            dbContext.Courses.Remove(course);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}
