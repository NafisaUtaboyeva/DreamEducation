using DreamEducation.Data.Contexts;
using DreamEducation.Data.IRepositories;
using DreamEducation.Domain.Entities.Chapters;
using Serilog;

namespace DreamEducation.Data.Repositories
{
    public class ChapterRepository : GenericRepository<Chapter>, IChapterRepository
    {
        public ChapterRepository(DreamEduDbContext dbContext, ILogger logger)
            : base(dbContext, logger)
        {
        }
    }
}
