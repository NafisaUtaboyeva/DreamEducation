using DreamEducation.Data.IRepositories;
using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Tests;
using DreamEducation.Domain.Enums;
using DreamEducation.Service.Extensions;
using DreamEducation.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DreamEducation.Service.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork unitOfWork;

        public QuestionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Question>> CreateAsync(Question question)
        {
            var response = new BaseResponse<Question>();


            await unitOfWork.SaveChangesAsync();

          
            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Test, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            // check for exist student
            var existTest = await unitOfWork.Tests.GetAsync(expression);
            if (existTest is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }
            existTest.Delete();

            var result = await unitOfWork.Tests.UpdateAsync(existTest);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Question, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<IEnumerable<Test>>> GetAllAsync(PaginationParams @params, Expression<Func<Test, bool>> expression = null)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            var response = new BaseResponse<IEnumerable<Test>>();

            var tests = await unitOfWork.Tests.GetAllAsync(expression);

            response.Data = tests.ToPagedList(@params);

            return response;
        }

        public Task<BaseResponse<IEnumerable<Question>>> GetAllAsync(PaginationParams @params, Expression<Func<Question, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<Test>> GetAsync(Expression<Func<Test, bool>> expression)
        {
            var response = new BaseResponse<Test>();

            var test = await unitOfWork.Tests.GetAsync(expression);
            if (test is null)
            {
                response.Error = new ErrorResponse(404, "Test not found");
                return response;
            }

            response.Data = test;

            return response;
        }

        public Task<BaseResponse<Question>> GetAsync(Expression<Func<Question, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<Test>> UpdateAsync(Guid id, Test test)
        {
            var response = new BaseResponse<Test>();

            // check for exist student
            var test1 = await unitOfWork.Tests.GetAsync(p => p.Id == id && p.State != ItemState.Deleted);
            if (test1 is null)
            {
                response.Error = new ErrorResponse(404, "Test not found");
                return response;
            }


            test1.Title = test.Title;
            test1.AmountOfQuestions = test.AmountOfQuestions;
            test1.Update();

            var result = await unitOfWork.Tests.UpdateAsync(test1);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public Task<BaseResponse<Question>> UpdateAsync(Guid id, Question question)
        {
            throw new NotImplementedException();
        }
    }
}
