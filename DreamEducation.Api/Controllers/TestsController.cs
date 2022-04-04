using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Tests;
using DreamEducation.Domain.Enums;
using DreamEducation.Service.DTOs;
using DreamEducation.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DreamEducation.Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class TestsController : ControllerBase
    {
        private readonly ITestService testService;
        private readonly IWebHostEnvironment env;
        public TestsController(ITestService testService, IWebHostEnvironment env)
        {
            this.testService = testService;
            this.env = env;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Test>>> Create([FromForm] TestForCreationDto testDto)
        {
            var result = await testService.CreateAsync(testDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Test>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await testService.GetAllAsync(@params);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Test>>> Get([FromRoute] Guid id)
        {
            var result = await testService.GetAsync(p => p.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Test>>> Update(Guid id, TestForCreationDto studentDto)
        {
            var result = await testService.UpdateAsync(id, studentDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var result = await testService.DeleteAsync(p => p.Id == id && p.State != ItemState.Deleted);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }
    }
}
