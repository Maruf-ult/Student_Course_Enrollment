using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Course_Enrollment.API.Data;
using Student_Course_Enrollment.API.DTOs.EnrollmentDtos;
using Student_Course_Enrollment.API.Models;

namespace Student_Course_Enrollment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        public readonly StudentDbContext dbContext;

        public EnrollmentsController(StudentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllEnrollments()
        {
            var enrollments = dbContext.Enrollments
                             .Include(e => e.Student)
                             .Include(e => e.Course)
                             .ToList();

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
        public IActionResult GetEnrollmentByStudentAndCourse(Guid studentId, Guid courseId)
        {
            var enrollment = dbContext.Enrollments
                            .Include(e => e.Student)
                            .Include(e => e.Course)
                            .FirstOrDefault(e =>     
                             e.StudentId == studentId &&
                             e.CourseId == courseId);

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
        public IActionResult CreateEnrollment([FromBody] AddEnrollmentDto request)
        {
            var enrollment = new Enrollment
            {
                StudentId = request.StudentId,
                CourseId = request.CourseId,
                EnrollmentDate = request.EnrollmentDate,
                Grade = request.Grade.GetValueOrDefault()
            };
            dbContext.Enrollments.Add(enrollment);
            dbContext.SaveChanges();

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
        public IActionResult UpdateEnrollment(Guid studentId, Guid courseId, [FromBody] AddEnrollmentDto request)
        {
            var enrollment = dbContext.Enrollments.FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId);
            if (enrollment == null)
            {
                return NotFound();
            }
            enrollment.EnrollmentDate = request.EnrollmentDate;
            enrollment.Grade = request.Grade.GetValueOrDefault();
            dbContext.SaveChanges();
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
        public IActionResult DeleteEnrollment(Guid studentId, Guid courseId)
        {
            var enrollment = dbContext.Enrollments.FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId);
            if (enrollment == null)
            {
                return NotFound();
            }
            dbContext.Enrollments.Remove(enrollment);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}
