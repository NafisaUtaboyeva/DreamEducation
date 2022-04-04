using DreamEducation.Data.Contexts;
using DreamEducation.Data.IRepositories;
using DreamEducation.Domain.Entities.Tests;
using Serilog;

namespace DreamEducation.Data.Repositories
{
    public class TestRepository : GenericRepository<Test>, ITestRepository
    {
        public TestRepository(DreamEduDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }
    }
}
