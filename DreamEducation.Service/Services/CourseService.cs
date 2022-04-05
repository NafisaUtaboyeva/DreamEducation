using AutoMapper;
using DreamEducation.Data.IRepositories;
using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Courses;
using DreamEducation.Domain.Enums;
using DreamEducation.Service.DTOs.Courses;
using DreamEducation.Service.Extensions;
using DreamEducation.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DreamEducation.Service.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Course>> CreateAsync(CourseForCreationDto dto)
        {
            var response = new BaseResponse<Course>();

            var existCourse = await unitOfWork.Courses.GetAsync(p => p.Name == dto.Name);

            if (existCourse is not null)
            {
                response.Error = new ErrorResponse(400, "This name is already taken!");
                return response;
            }

            var mappedCourse = mapper.Map<Course>(dto);
            mappedCourse.Image = await FileExtensions.SaveFileAsync(dto.Image.OpenReadStream(), dto.Image.FileName, config, env);

            mappedCourse.Create();

            var result = await unitOfWork.Courses.CreateAsync(mappedCourse);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Course, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var existCourse = await unitOfWork.Courses.GetAsync(expression);

            if (existCourse is null)
            {
                response.Error = new ErrorResponse(404, "Course not found!");
                return response;
            }

            existCourse.Delete();

            await unitOfWork.Courses.UpdateAsync(existCourse);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<Course>> DeleteImageAsync(Expression<Func<Course, bool>> expression)
        {
            var response = new BaseResponse<Course>();

            var course = await unitOfWork.Courses.GetAsync(expression);

            if (course is null || course.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Course not found!");
                return response;
            }
            course.Image = null;
            course.Update();

            await unitOfWork.Courses.UpdateAsync(course);
            await unitOfWork.SaveChangesAsync();

            response.Data = course;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Course>>> GetAllAsync(PaginationParams @params, Expression<Func<Course, bool>> expression = null)
        {
            var response = new BaseResponse<IEnumerable<Course>>();

            var courses = await unitOfWork.Courses.GetAllAsync(expression);

            courses = courses.Where(p => p.State != ItemState.Deleted);

            response.Data = courses.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Course>> GetAsync(Expression<Func<Course, bool>> expression)
        {
            var response = new BaseResponse<Course>();

            var course = await unitOfWork.Courses.GetAsync(expression);

            if (course is null || course.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Course not found!");
                return response;
            }

            response.Data = course;

            return response;
        }

        public async Task<BaseResponse<Course>> RegisterStudentForCourse(Guid courseId, Guid userId)
        {
            var response = new BaseResponse<Course>();

            var user = await unitOfWork.Students.GetAsync(p => p.Id == userId && p.State != ItemState.Deleted);
            if (user is null)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }

            var course = await unitOfWork.Courses.GetAsync(p => p.Id == courseId && p.State != ItemState.Deleted);
            if (course is null)
            {
                response.Error = new ErrorResponse(404, "Course not found!");
                return response;
            }

            user.Courses.Add(course);
            user.Update();
            await unitOfWork.Students.UpdateAsync(user);

            course.Students.Add(user);
            course.Update();
            await unitOfWork.Courses.UpdateAsync(course);

            await unitOfWork.SaveChangesAsync();

            response.Data = course;
            return response;
        }

        public async Task<BaseResponse<Course>> SetImageAsync(Expression<Func<Course, bool>> expression, IFormFile image)
        {
            var response = new BaseResponse<Course>();

            var course = await unitOfWork.Courses.GetAsync(expression);

            if (course is null)
            {
                response.Error = new ErrorResponse(404, "Course not found!");
                return response;
            }

            course.Image = await FileExtensions.SaveFileAsync(image.OpenReadStream(), image.FileName, config, env);
            course.Update();

            await unitOfWork.Courses.UpdateAsync(course);

            await unitOfWork.SaveChangesAsync();

            response.Data = course;

            return response;
        }

        public async Task<BaseResponse<Course>> UpdateAsync(Guid id, CourseForCreationDto model)
        {
            var response = new BaseResponse<Course>();

            var course = await unitOfWork.Courses.GetAsync(p => p.Id == id && p.State == ItemState.Deleted);
            if (course is null)
            {
                response.Error = new ErrorResponse(404, "Course not found!");
                return response;
            }

            course.Image = await FileExtensions.SaveFileAsync(model.Image.OpenReadStream(), model.Image.FileName, config, env);

            course.Update();

            var result = await unitOfWork.Courses.UpdateAsync(course);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }
    }
}
