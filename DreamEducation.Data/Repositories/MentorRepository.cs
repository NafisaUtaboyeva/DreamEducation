using DreamEducation.Data.Contexts;
using DreamEducation.Data.IRepositories;
using DreamEducation.Domain.Entities.Mentors;
using Serilog;

namespace DreamEducation.Data.Repositories
{
    public class MentorRepository : GenericRepository<Mentor>, IMentorRepository
    {
        public MentorRepository(DreamEduDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }
    }
}
