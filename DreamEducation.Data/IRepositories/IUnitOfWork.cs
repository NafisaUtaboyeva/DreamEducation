using System;
using System.Threading.Tasks;

namespace DreamEducation.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepository Courses { get; }
        IStudentRepository Students { get; }
        IMentorRepository Mentors { get; }
        IChapterRepository Chapters { get; }
        ITestRepository Tests { get; }
        IQuestionRepository Questions { get; }
        Task SaveChangesAsync();
    }
}
