using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Chapters;
using DreamEducation.Service.DTOs.Chapters;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DreamEducation.Service.Interfaces
{
    public interface IChapterService
    {
        Task<BaseResponse<Chapter>> CreateAsync(ChapterForCreationDto chapter);
        Task<BaseResponse<Chapter>> GetAsync(Expression<Func<Chapter, bool>> expression);
        Task<BaseResponse<IEnumerable<Chapter>>> GetAllAsync(PaginationParams @params, Expression<Func<Chapter, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Chapter, bool>> expression);
        Task<BaseResponse<Chapter>> UpdateAsync(Guid id, ChapterForCreationDto chapter);
        Task<BaseResponse<Chapter>> DeleteVideoAsync(Expression<Func<Chapter, bool>> expression);
        Task<BaseResponse<Chapter>> SetVideoAsync(Expression<Func<Chapter, bool>> expression, IFormFile video);
        Task<BaseResponse<Chapter>> SetLectionAsync(Expression<Func<Chapter, bool>> expression, IFormFile lection);
        Task<BaseResponse<Chapter>> DeleteLectionAsync(Expression<Func<Chapter, bool>> expression);
    }
}
