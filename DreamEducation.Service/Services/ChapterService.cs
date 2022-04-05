using AutoMapper;
using DreamEducation.Data.IRepositories;
using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Chapters;
using DreamEducation.Domain.Enums;
using DreamEducation.Service.DTOs.Chapters;
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
    public class ChapterService : IChapterService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public ChapterService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Chapter>> CreateAsync(ChapterForCreationDto dto)
        {
            var response = new BaseResponse<Chapter>();

            var existChapter = await unitOfWork.Courses.GetAsync(p => p.Title == dto.Title);

            if (existChapter is not null)
            {
                response.Error = new ErrorResponse(400, "This title is already taken!");
                return response;
            }

            var mappedChapter = mapper.Map<Chapter>(dto);
            mappedChapter.Lection = await FileExtensions.SaveFileAsync(dto.Lection.OpenReadStream(), dto.Lection.FileName, config, env);
            mappedChapter.Video = await FileExtensions.SaveFileAsync(dto.Video.OpenReadStream(), dto.Video.FileName, config, env);

            mappedChapter.Create();

            var result = await unitOfWork.Chapters.CreateAsync(mappedChapter);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Chapter, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var existChapter = await unitOfWork.Chapters.GetAsync(expression);

            if (existChapter is null)
            {
                response.Error = new ErrorResponse(404, "Chapter not found!");
                return response;
            }

            existChapter.Delete();

            await unitOfWork.Chapters.UpdateAsync(existChapter);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<Chapter>> DeleteVideoAsync(Expression<Func<Chapter, bool>> expression)
        {
            var response = new BaseResponse<Chapter>();

            var chapter = await unitOfWork.Chapters.GetAsync(expression);

            if (chapter is null || chapter.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Chapter not found!");
                return response;
            }
            chapter.Video = null;
            chapter.Update();

            await unitOfWork.Chapters.UpdateAsync(chapter);
            await unitOfWork.SaveChangesAsync();

            response.Data = chapter;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Chapter>>> GetAllAsync(PaginationParams @params, Expression<Func<Chapter, bool>> expression = null)
        {
            var response = new BaseResponse<IEnumerable<Chapter>>();

            var chapters = await unitOfWork.Chapters.GetAllAsync(expression);

            chapters = chapters.Where(p => p.State != ItemState.Deleted);

            response.Data = chapters.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Chapter>> GetAsync(Expression<Func<Chapter, bool>> expression)
        {
            var response = new BaseResponse<Chapter>();

            var chapter = await unitOfWork.Chapters.GetAsync(expression);

            if (chapter is null || chapter.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Chapter not found!");
                return response;
            }

            response.Data = chapter;

            return response;
        }

        public async Task<BaseResponse<Chapter>> SetVideoAsync(Expression<Func<Chapter, bool>> expression, IFormFile video)
        {
            var response = new BaseResponse<Chapter>();

            var chapter = await unitOfWork.Chapters.GetAsync(expression);

            if (chapter is null)
            {
                response.Error = new ErrorResponse(404, "Chapter not found!");
                return response;
            }

            chapter.Video = await FileExtensions.SaveFileAsync(video.OpenReadStream(), video.FileName, config, env);
            chapter.Update();

            await unitOfWork.Chapters.UpdateAsync(chapter);

            await unitOfWork.SaveChangesAsync();

            response.Data = chapter;

            return response;
        }

        public async Task<BaseResponse<Chapter>> UpdateAsync(Guid id, ChapterForCreationDto model)
        {
            var response = new BaseResponse<Chapter>();

            var chapter = await unitOfWork.Chapters.GetAsync(p => p.Id == id && p.State == ItemState.Deleted);
            if (chapter is null)
            {
                response.Error = new ErrorResponse(404, "Chapter not found!");
                return response;
            }

            chapter.Video = await FileExtensions.SaveFileAsync(model.Video.OpenReadStream(), model.Video.FileName, config, env);
            chapter.Lection = await FileExtensions.SaveFileAsync(model.Lection.OpenReadStream(), model.Lection.FileName, config, env);

            chapter.Update();

            var result = await unitOfWork.Chapters.UpdateAsync(chapter);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }
        public async Task<BaseResponse<Chapter>> DeleteLectionAsync(Expression<Func<Chapter, bool>> expression)
        {
            var response = new BaseResponse<Chapter>();

            var chapter = await unitOfWork.Chapters.GetAsync(expression);

            if (chapter is null || chapter.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Chapter not found!");
                return response;
            }
            chapter.Lection = null;
            chapter.Update();

            await unitOfWork.Chapters.UpdateAsync(chapter);
            await unitOfWork.SaveChangesAsync();

            response.Data = chapter;

            return response;
        }
        public async Task<BaseResponse<Chapter>> SetLectionAsync(Expression<Func<Chapter, bool>> expression, IFormFile lection)
        {
            var response = new BaseResponse<Chapter>();

            var chapter = await unitOfWork.Chapters.GetAsync(expression);

            if (chapter is null)
            {
                response.Error = new ErrorResponse(404, "Chapter not found!");
                return response;
            }

            chapter.Video = await FileExtensions.SaveFileAsync(lection.OpenReadStream(), lection.FileName, config, env);
            chapter.Update();

            await unitOfWork.Chapters.UpdateAsync(chapter);

            await unitOfWork.SaveChangesAsync();

            response.Data = chapter;

            return response;
        }
    }
}
