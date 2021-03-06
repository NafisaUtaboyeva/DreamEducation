using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Courses;
using DreamEducation.Service.DTOs.Courses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DreamEducation.Service.Interfaces
{
    public interface ICourseService
    {
        Task<BaseResponse<Course>> CreateAsync(CourseForCreationDto course);
        Task<BaseResponse<Course>> GetAsync(Expression<Func<Course, bool>> expression);
        Task<BaseResponse<IEnumerable<Course>>> GetAllAsync(PaginationParams @params, Expression<Func<Course, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Course, bool>> expression);
        Task<BaseResponse<Course>> UpdateAsync(Guid id, CourseForCreationDto course);
        Task<BaseResponse<Course>> SetImageAsync(Expression<Func<Course, bool>> expression, IFormFile image);
        Task<BaseResponse<Course>> DeleteImageAsync(Expression<Func<Course, bool>> expression);
        Task<BaseResponse<Course>> RegisterStudentForCourse(Guid courseId, Guid userId);
    }
}
