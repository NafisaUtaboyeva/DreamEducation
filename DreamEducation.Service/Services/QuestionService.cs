using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Tests;
using DreamEducation.Service.DTOs.Questions;
using DreamEducation.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DreamEducation.Service.Services
{
    public class QuestionService : IQuestionService
    {
        public Task<BaseResponse<Question>> CreateAsync(QuestionForCreationDto question)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Question, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<Question>>> GetAllAsync(PaginationParams @params, Expression<Func<Question, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Question>> GetAsync(Expression<Func<Question, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Question>> UpdateAsync(Guid id, QuestionForCreationDto question)
        {
            throw new NotImplementedException();
        }
    }
}
