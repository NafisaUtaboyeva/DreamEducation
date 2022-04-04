using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DreamEducation.Service.Interfaces
{
    public interface IQuestionService
    {
        Task<BaseResponse<Question>> CreateAsync(Question question);
        Task<BaseResponse<Question>> GetAsync(Expression<Func<Question, bool>> expression);
        Task<BaseResponse<IEnumerable<Question>>> GetAllAsync(PaginationParams @params, Expression<Func<Question, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Question, bool>> expression);
        Task<BaseResponse<Question>> UpdateAsync(Guid id, Question question);
    }
}
