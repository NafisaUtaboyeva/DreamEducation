using DreamEducation.Data.IRepositories;
using DreamEducation.Data.Repositories;
using DreamEducation.Service.Interfaces;
using DreamEducation.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DreamEducation.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IMentorService, MentorService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IChapterService, ChapterService>();
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IQuestionService, QuestionService>();
        }
    }
}
