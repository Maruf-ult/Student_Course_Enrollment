using Student_Course_Enrollment.API.DTOs.CourseDtos;
using Student_Course_Enrollment.API.Models;
using Student_Course_Enrollment.API.Repositories.Interfaces;
using Student_Course_Enrollment.API.Services.Interfaces;

namespace Student_Course_Enrollment.API.Services.Implementations
{
    public class CourseService:ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _courseRepository.GetAllAsync();
        }

        public async Task<Course?> GetByIdAsync(Guid id)
        {
            return await _courseRepository.GetByIdAsync(id);
        }

        public async Task<Course> CreateAsync(AddCourseDto request)
        {
            var course = new Course
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description
            };
            return await _courseRepository.CreateAsync(course);
        }

        public async Task<Course?> UpdateAsync(Guid id, UpdateCourseDto request)
        {
            var updatedCourse = new Course
            {
                Title = request.Title,
                Description = request.Description
            };

            return await _courseRepository.UpdateAsync(id, updatedCourse);
        }

        public async Task<Course?> DeleteAsync(Guid id)
        {
            return await _courseRepository.DeleteAsync(id);

        }


    }
}
