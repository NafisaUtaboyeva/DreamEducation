using AutoMapper;
using DreamEducation.Data.IRepositories;
using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Tests;
using DreamEducation.Domain.Enums;
using DreamEducation.Service.DTOs;
using DreamEducation.Service.Extensions;
using DreamEducation.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DreamEducation.Service.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<Test>> CreateAsync(TestForCreationDto testDto)
        {
            var response = new BaseResponse<Test>();

            Test mappedTest = mapper.Map<Test>(testDto);
            var result = await unitOfWork.Tests.CreateAsync(mappedTest);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Test, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var existTest = await unitOfWork.Tests.GetAsync(expression);
            if (existTest is null)
            {
                response.Error = new ErrorResponse(404, "Test not found");
                return response;
            }
            existTest.Delete();

            var result = await unitOfWork.Tests.UpdateAsync(existTest);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Test>>> GetAllAsync(PaginationParams @params, Expression<Func<Test, bool>> expression = null)
        {
            var response = new BaseResponse<IEnumerable<Test>>();

            var tests = await unitOfWork.Tests.GetAllAsync(expression);

            tests = tests.Where(p => p.State != ItemState.Deleted);

            response.Data = tests.ToPagedList(@params);

            return response;
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

        public async Task<BaseResponse<Test>> UpdateAsync(Guid id, TestForCreationDto test)
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
    }
}
