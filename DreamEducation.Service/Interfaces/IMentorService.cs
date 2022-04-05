using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Mentors;
using DreamEducation.Service.DTOs.Mentors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DreamEducation.Service.Interfaces
{
    public interface IMentorService
    {
        Task<BaseResponse<Mentor>> CreateAsync(MentorForCreationDto mentor);
        Task<BaseResponse<Mentor>> GetAsync(Expression<Func<Mentor, bool>> expression);
        Task<BaseResponse<IEnumerable<Mentor>>> GetAllAsync(PaginationParams @params, Expression<Func<Mentor, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Mentor, bool>> expression);
        Task<BaseResponse<Mentor>> UpdateAsync(Guid id, MentorForCreationDto mentor);
        Task<BaseResponse<Mentor>> LoginAsync(MentorForLoginDto dto);
        Task<BaseResponse<Mentor>> SetImageAsync(Expression<Func<Mentor, bool>> expression, IFormFile image);
        Task<BaseResponse<Mentor>> DeleteImageAsync(Expression<Func<Mentor, bool>> expression);
    }
}
