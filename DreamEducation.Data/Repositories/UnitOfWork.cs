using DreamEducation.Data.Contexts;
using DreamEducation.Data.IRepositories;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System;
using System.Threading.Tasks;

namespace DreamEducation.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DreamEduDbContext context;
        private readonly ILogger logger;
        private readonly IConfiguration config;

        public IStudentRepository Students { get; private set; }
        public IMentorRepository Mentors { get; private set; }
        public ICourseRepository Courses { get; private set; }
        public IChapterRepository Chapters { get; private set; }
        public ITestRepository Tests { get; private set; }
        public IQuestionRepository Questions { get; private set; }

        public UnitOfWork(DreamEduDbContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
            this.logger = new LoggerConfiguration()
                .WriteTo.File
                (
                    path: "Logs/logs.txt",
                    outputTemplate: config.GetSection("Serilog:OutputTemplate").Value,
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Information
                ).CreateLogger();

            // Object initializing for repositories
            Students = new StudentRepository(context, logger);
            Mentors = new MentorRepository(context, logger);
            Courses = new CourseRepository(context, logger);
            Chapters = new ChapterRepository(context, logger);
            Tests = new TestRepository(context, logger);
            Questions = new QuestionRepository(context, logger);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
