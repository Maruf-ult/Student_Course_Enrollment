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
        public async Task<IActionResult> GetAllEnrollments()
        {
            var enrollments = await dbContext.Enrollments
                             .Include(e => e.Student)
                             .Include(e => e.Course)
                             .ToListAsync();

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
            var enrollment = await dbContext.Enrollments
                            .Include(e => e.Student)
                            .Include(e => e.Course)
                            .FirstOrDefaultAsync(e =>     
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
        public async Task<IActionResult> CreateEnrollment([FromBody] AddEnrollmentDto request)
        {
            var enrollment = new Enrollment
            {
                StudentId = request.StudentId,
                CourseId = request.CourseId,
                EnrollmentDate = request.EnrollmentDate,
                Grade = request.Grade.GetValueOrDefault()
            };
            await dbContext.Enrollments.AddAsync(enrollment);
            await dbContext.SaveChangesAsync();

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
        public async Task<IActionResult> UpdateEnrollment(Guid studentId, Guid courseId, [FromBody] AddEnrollmentDto request)
        {
            var enrollment = await dbContext.Enrollments.FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);
            if (enrollment == null)
            {
                return NotFound();
            }
            enrollment.EnrollmentDate = request.EnrollmentDate;
            enrollment.Grade = request.Grade.GetValueOrDefault();
            await dbContext.SaveChangesAsync();
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
            var enrollment = await dbContext.Enrollments.FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);
            if (enrollment == null)
            {
                return NotFound();
            }
            dbContext.Enrollments.Remove(enrollment);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
