using DreamEducation.Data.Contexts;
using DreamEducation.Data.IRepositories;
using DreamEducation.Domain.Entities.Tests;
using Serilog;

namespace DreamEducation.Data.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(DreamEduDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {

        }
    }
}
