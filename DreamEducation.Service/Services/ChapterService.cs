using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Chapters;
using DreamEducation.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DreamEducation.Service.Services
{
    public class ChapterService : IChapterService
    {
        public Task<BaseResponse<Chapter>> CreateAsync(Chapter chapter)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Chapter, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<Chapter>>> GetAllAsync(PaginationParams @params, Expression<Func<Chapter, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Chapter>> GetAsync(Expression<Func<Chapter, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<string> SaveFileAsync(Stream file, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Chapter>> UpdateAsync(Guid id, Chapter chapter)
        {
            throw new NotImplementedException();
        }
    }
}
