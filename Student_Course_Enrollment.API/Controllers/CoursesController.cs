using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Course_Enrollment.API.Data;
using Student_Course_Enrollment.API.DTOs.CourseDtos;
using Student_Course_Enrollment.API.DTOs.EnrollmentDtos;
using Student_Course_Enrollment.API.Models;
using Student_Course_Enrollment.API.Services.Interfaces;

namespace Student_Course_Enrollment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllAsync();

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
            var course = await _courseService.GetByIdAsync(id);

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
            var course = await _courseService.CreateAsync(request);

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
            var course = await _courseService.UpdateAsync(id, request);

            if (course == null)
                return NotFound();

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
            var course = await _courseService.DeleteAsync(id);
            
            if (course == null)
                return NotFound();

            return NoContent();
        }
    }
}
