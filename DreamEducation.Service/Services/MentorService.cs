using AutoMapper;
using DreamEducation.Data.IRepositories;
using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Mentors;
using DreamEducation.Domain.Enums;
using DreamEducation.Service.DTOs.Mentors;
using DreamEducation.Service.Extensions;
using DreamEducation.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DreamEducation.Service.Services
{
    public class MentorService : IMentorService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public MentorService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Mentor>> CreateAsync(MentorForCreationDto mentorDto)
        {
            var response = new BaseResponse<Mentor>();

            var existMentor = await unitOfWork.Mentors.GetAsync(p => p.Username == mentorDto.Username || p.Email == mentorDto.Email);
            if (existMentor is not null)
            {
                response.Error = new ErrorResponse(400, "User is exist!");
                return response;
            }

            var mappedMentor = mapper.Map<Mentor>(mentorDto);
            mappedMentor.Password = mentorDto.Password.ToHash256();
            mappedMentor.Image = await FileExtensions.SaveFileAsync(mentorDto.Image.OpenReadStream(), mentorDto.Image.FileName, config, env);

            var result = await unitOfWork.Mentors.CreateAsync(mappedMentor);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Mentor, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var existMentor = await unitOfWork.Mentors.GetAsync(expression);
            if (existMentor is null || existMentor.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }
            existMentor.Delete();

            await unitOfWork.Mentors.UpdateAsync(existMentor);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<Mentor>> DeleteImageAsync(Expression<Func<Mentor, bool>> expression)
        {
            var response = new BaseResponse<Mentor>();

            var user = await unitOfWork.Mentors.GetAsync(expression);

            if (user is null || user.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }
            user.Image = null;
            user.Update();

            await unitOfWork.Mentors.UpdateAsync(user);
            await unitOfWork.SaveChangesAsync();

            response.Data = user;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Mentor>>> GetAllAsync(PaginationParams @params, Expression<Func<Mentor, bool>> expression = null)
        {
            var response = new BaseResponse<IEnumerable<Mentor>>();

            var mentors = await unitOfWork.Mentors.GetAllAsync(expression);

            mentors = mentors.Where(st => st.State != ItemState.Deleted);

            response.Data = mentors.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Mentor>> GetAsync(Expression<Func<Mentor, bool>> expression)
        {
            var response = new BaseResponse<Mentor>();

            var mentor = await unitOfWork.Mentors.GetAsync(expression);
            if (mentor is null || mentor.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }

            response.Data = mentor;

            return response;
        }

        public async Task<BaseResponse<Mentor>> LoginAsync(MentorForLoginDto dto)
        {
            var response = new BaseResponse<Mentor>();

            var password = dto.Password.ToHash256();

            var user = await unitOfWork.Mentors.GetAsync(p => p.Username == dto.Username && p.Password == password);

            if (user is null)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }

            response.Data = user;

            return response;
        }

        public async Task<BaseResponse<Mentor>> SetImageAsync(Expression<Func<Mentor, bool>> expression, IFormFile image)
        {
            var response = new BaseResponse<Mentor>();

            var user = await unitOfWork.Mentors.GetAsync(expression);

            if (user is null)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }

            user.Image = await FileExtensions.SaveFileAsync(image.OpenReadStream(), image.FileName, config, env);

            user.Update();
            await unitOfWork.Mentors.UpdateAsync(user);

            await unitOfWork.SaveChangesAsync();

            response.Data = user;

            return response;
        }

        public async Task<BaseResponse<Mentor>> UpdateAsync(Guid id, MentorForCreationDto mentorDto)
        {
            var response = new BaseResponse<Mentor>();

            var mentor = await unitOfWork.Mentors.GetAsync(p => p.Id == id && p.State != ItemState.Deleted);
            if (mentor is null)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }

            mentor = mapper.Map<Mentor>(mentorDto);
            mentor.Id = id;

            mentor.Update();

            mentor.Password = mentor.Password.ToHash256();
            mentor.Image = await FileExtensions.SaveFileAsync(mentorDto.Image.OpenReadStream(), mentorDto.Image.FileName, config, env);

            var result = await unitOfWork.Mentors.UpdateAsync(mentor);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }
    }
}
