﻿using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Courses;
using DreamEducation.Service.DTOs.Courses;
using DreamEducation.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DreamEducation.Service.Services
{
    public class CourseService : ICourseService
    {
        public Task<BaseResponse<Course>> CreateAsync(CourseForCreationDto course)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Course, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<Course>>> GetAllAsync(PaginationParams @params, Expression<Func<Course, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Course>> GetAsync(Expression<Func<Course, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Course>> UpdateAsync(Guid id, CourseForCreationDto course)
        {
            throw new NotImplementedException();
        }
    }
}
