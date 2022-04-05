﻿using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Users;
using DreamEducation.Service.DTOs.Students;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DreamEducation.Service.Interfaces
{
    public interface IStudentService
    {
        Task<BaseResponse<Student>> CreateAsync(StudentForCreationDto studentDto);
        Task<BaseResponse<Student>> GetAsync(Expression<Func<Student, bool>> expression);
        Task<BaseResponse<IEnumerable<Student>>> GetAllAsync(PaginationParams @params, Expression<Func<Student, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Student, bool>> expression);
        Task<BaseResponse<Student>> UpdateAsync(Guid id, StudentForCreationDto studentDto);
        Task<BaseResponse<Student>> LoginAsync(StudentForLoginDto dto);
        Task<BaseResponse<Student>> SetImageAsync(Expression<Func<Student, bool>> expression, IFormFile image);
        Task<BaseResponse<Student>> DeleteImageAsync(Expression<Func<Student, bool>> expression);
    }
}
