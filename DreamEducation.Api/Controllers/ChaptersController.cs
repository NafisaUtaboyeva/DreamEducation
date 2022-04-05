using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Chapters;
using DreamEducation.Domain.Enums;
using DreamEducation.Service.DTOs.Chapters;
using DreamEducation.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DreamEducation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChaptersController : ControllerBase
    {
        private readonly IChapterService chapterService;

        public ChaptersController(IChapterService chapterService)
        {
            this.chapterService = chapterService;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Chapter>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await chapterService.GetAllAsync(@params);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Chapter>>> Get(Guid id)
        {
            var result = await chapterService.GetAsync(p => p.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Chapter>>> Create([FromForm] ChapterForCreationDto chapter)
        {
            var result = await chapterService.CreateAsync(chapter);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Chapter>>> Update(Guid id, ChapterForCreationDto chapter)
        {
            var result = await chapterService.UpdateAsync(id, chapter);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<Chapter>>> Delete(Guid id)
        {
            var result = await chapterService.DeleteAsync(p => p.Id == id && p.State != ItemState.Deleted);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPost("Video/{id}")]
        public async Task<ActionResult<Chapter>> SetVideo(Guid id, IFormFile video)
        {
            var result = await chapterService.SetVideoAsync(p => p.Id == id, video);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete("Video/{id}")]
        public async Task<ActionResult<Chapter>> DeleteVideo(Guid id)
        {
            var result = await chapterService.DeleteVideoAsync(p => p.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPost("Lection/{id}")]
        public async Task<ActionResult<Chapter>> SetLection(Guid id, IFormFile document)
        {
            var result = await chapterService.SetLectionAsync(p => p.Id == id, document);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete("Lection/{id}")]
        public async Task<ActionResult<Chapter>> DeleteLection(Guid id)
        {
            var result = await chapterService.DeleteLectionAsync(p => p.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }
    }
}
