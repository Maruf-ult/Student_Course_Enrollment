using Microsoft.EntityFrameworkCore;
using Student_Course_Enrollment.API.Data;
using Student_Course_Enrollment.API.Repositories.Implementation;
using Student_Course_Enrollment.API.Repositories.Implementations;
using Student_Course_Enrollment.API.Repositories.Interfaces;
using Student_Course_Enrollment.API.Services.Implementations;
using Student_Course_Enrollment.API.Services.Interfaces;
using Student_Course_Enrollment.API.Services.Repositories;

namespace Student_Course_Enrollment.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<IStudentRepository,StudentRepository>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IPasswordService, PasswordService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddScoped<IPasswordService, PasswordService>();

            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StudentDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("StudentCourseDbCon"));
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
