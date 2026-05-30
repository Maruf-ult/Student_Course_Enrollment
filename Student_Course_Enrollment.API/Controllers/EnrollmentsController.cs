using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Course_Enrollment.API.Data;
using Student_Course_Enrollment.API.DTOs.EnrollmentDtos;
using Student_Course_Enrollment.API.Models;
using Student_Course_Enrollment.API.Services.Interfaces;

namespace Student_Course_Enrollment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEnrollments()
        {
            var enrollments = await _enrollmentService.GetAllAsync();

            var dto = enrollments.Select(e => new EnrollmentDto
            {
                StudentId = e.StudentId,
                CourseId = e.CourseId,
                EnrollmentDate = e.EnrollmentDate,
                Grade = e?.Grade,
                Student = e?.Student.Name,
                Course = e?.Course.Title
            });

            return Ok(dto);
            
        }

        [HttpGet("{studentId:guid}/{courseId:guid}")]
        public async Task<IActionResult> GetEnrollmentByStudentAndCourse(Guid studentId, Guid courseId)
        {
            var enrollment = await _enrollmentService.GetByStudentAndCourseAsync(studentId, courseId);

            if (enrollment == null)
            {
                return NotFound();
            }

            var dto = new EnrollmentDto
            {
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId,
                EnrollmentDate = enrollment.EnrollmentDate,
                Grade = enrollment?.Grade,
                Student = enrollment?.Student.Name,
                Course = enrollment?.Course.Title
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnrollment([FromBody] AddEnrollmentDto request)
        {
            
            var enrollment = await _enrollmentService.CreateAsync(request);

            var dto = new EnrollmentDto
            {
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId,
                EnrollmentDate = enrollment.EnrollmentDate,
                Grade = enrollment?.Grade,
                Student = enrollment?.Student?.Name,
                Course = enrollment?.Course?.Title
            };


            return CreatedAtAction(nameof(GetEnrollmentByStudentAndCourse), new { studentId = enrollment?.StudentId, courseId = enrollment?.CourseId }, dto);
        }

        [HttpPut("{studentId:guid}/{courseId:guid}")]
        public async Task<IActionResult> UpdateEnrollment(Guid studentId, Guid courseId, [FromBody] UpdateEnrollmentDto request)
        {
            var enrollment = await _enrollmentService.UpdateAsync(studentId,courseId,request);
            if (enrollment == null)
            {
                return NotFound();
            }

            var dto = new EnrollmentDto
            {
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId,
                EnrollmentDate = enrollment.EnrollmentDate,
                Grade = enrollment?.Grade,
                Student = enrollment?.Student?.Name,
                Course = enrollment?.Course?.Title
            };
            return Ok(dto);
        }

        [HttpDelete("{studentId:guid}/{courseId:guid}")]
        public async Task<IActionResult> DeleteEnrollment(Guid studentId, Guid courseId)
        {
            var enrollment = await _enrollmentService.DeleteAsync(studentId, courseId);
            if (enrollment == null)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}
