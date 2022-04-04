using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Mentors;
using DreamEducation.Service.DTOs.Mentors;
using DreamEducation.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DreamEducation.Service.Services
{
    public class MentorService : IMentorService
    {
        public Task<BaseResponse<Mentor>> CreateAsync(MentorForCreationDto mentor)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Mentor, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<Mentor>>> GetAllAsync(PaginationParams @params, Expression<Func<Mentor, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Mentor>> GetAsync(Expression<Func<Mentor, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<string> SaveFileAsync(Stream file, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Mentor>> UpdateAsync(Guid id, MentorForCreationDto mentor)
        {
            throw new NotImplementedException();
        }
    }
}
