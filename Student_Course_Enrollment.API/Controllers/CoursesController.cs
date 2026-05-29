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
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await dbContext.Courses
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Student)
                .ToListAsync();

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

        public async Task<IActionResult> GetCourseById([FromRoute] Guid id)
        {
            var course = await dbContext.Courses
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Student)
                .FirstOrDefaultAsync(c => c.Id == id);

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
        public async Task<IActionResult> AddCourse([FromBody] AddCourseDto request)
        {
            var course = new Course
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
            };

            await dbContext.Courses.AddAsync(course);
            await dbContext.SaveChangesAsync();

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
        public async Task<IActionResult> UpdateCourse([FromRoute] Guid id, [FromBody] UpdateCourseDto request)
        {
            var course = await dbContext.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
                return NotFound();
            course.Title = request.Title;
            course.Description = request.Description;
            await dbContext.SaveChangesAsync();
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
        public async Task<IActionResult> DeleteCourse([FromRoute] Guid id)
        {
            var course = await dbContext.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
                return NotFound();
            dbContext.Courses.Remove(course);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
