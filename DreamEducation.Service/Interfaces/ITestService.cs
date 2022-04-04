using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Tests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DreamEducation.Service.Interfaces
{
    public interface ITestService
    {
        Task<BaseResponse<Test>> CreateAsync(Test test);
        Task<BaseResponse<Test>> GetAsync(Expression<Func<Test, bool>> expression);
        Task<BaseResponse<IEnumerable<Test>>> GetAllAsync(PaginationParams @params, Expression<Func<Test, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Test, bool>> expression);
        Task<BaseResponse<Test>> UpdateAsync(Guid id, Test test);
    }
}
