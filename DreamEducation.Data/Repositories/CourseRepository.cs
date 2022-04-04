using DreamEducation.Data.Contexts;
using DreamEducation.Data.IRepositories;
using DreamEducation.Domain.Entities.Courses;
using Serilog;

namespace DreamEducation.Data.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(DreamEduDbContext dbContext, ILogger logger)
            : base(dbContext, logger)
        {
        }
    }
}
