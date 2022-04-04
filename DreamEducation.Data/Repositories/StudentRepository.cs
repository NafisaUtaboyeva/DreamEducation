using DreamEducation.Data.Contexts;
using DreamEducation.Data.IRepositories;
using DreamEducation.Domain.Entities.Users;
using Serilog;

namespace DreamEducation.Data.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(DreamEduDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }
    }
}
