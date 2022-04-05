using AutoMapper;
using DreamEducation.Data.IRepositories;
using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Tests;
using DreamEducation.Domain.Enums;
using DreamEducation.Service.DTOs.Questions;
using DreamEducation.Service.Extensions;
using DreamEducation.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DreamEducation.Service.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public QuestionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<Question>> CreateAsync(QuestionForCreationDto dto)
        {
            var response = new BaseResponse<Question>();

            var question = mapper.Map<Question>(dto);

            var result = await unitOfWork.Questions.CreateAsync(question);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Question, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var question = await unitOfWork.Questions.GetAsync(expression);
            if (question is null || question.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Question not found!");
                return response;
            }
            question.Delete();

            await unitOfWork.Questions.UpdateAsync(question);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Question>>> GetAllAsync(PaginationParams @params, Expression<Func<Question, bool>> expression = null)
        {
            var response = new BaseResponse<IEnumerable<Question>>();

            var questions = await unitOfWork.Questions.GetAllAsync(expression);

            questions = questions.Where(st => st.State != ItemState.Deleted);

            response.Data = questions.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Question>> GetAsync(Expression<Func<Question, bool>> expression)
        {
            var response = new BaseResponse<Question>();

            var question = await unitOfWork.Questions.GetAsync(expression);
            if (question is null || question.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Question not found!");
                return response;
            }

            response.Data = question;

            return response;
        }

        public async Task<BaseResponse<Question>> UpdateAsync(Guid id, QuestionForCreationDto dto)
        {
            var response = new BaseResponse<Question>();

            var question = await unitOfWork.Questions.GetAsync(p => p.Id == id);
            if (question is null || question.State != ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Question not found!");
                return response;
            }

            question = mapper.Map<QuestionForCreationDto, Question>(dto, question);

            question.Update();

            var result = await unitOfWork.Questions.UpdateAsync(question);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }
    }
}
