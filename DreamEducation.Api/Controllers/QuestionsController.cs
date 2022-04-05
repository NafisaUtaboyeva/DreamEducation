using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Tests;
using DreamEducation.Service.DTOs.Questions;
using DreamEducation.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DreamEducation.Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService questionService;
        public QuestionsController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Test>>> Create([FromForm] QuestionForCreationDto dto)
        {
            var result = await questionService.CreateAsync(dto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("testId={id}")]
        public async Task<ActionResult<BaseResponse<IEnumerable<Question>>>> GetAll([FromQuery] PaginationParams @params, Guid id)
        {
            var result = await questionService.GetAllAsync(@params, p => p.TestId == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Question>>> Get([FromRoute] Guid id)
        {
            var result = await questionService.GetAsync(p => p.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Test>>> Update(Guid id, QuestionForCreationDto dto)
        {
            var result = await questionService.UpdateAsync(id, dto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var result = await questionService.DeleteAsync(p => p.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }
    }
}
