using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Mentors;
using DreamEducation.Service.DTOs.Mentors;
using DreamEducation.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DreamEducation.Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class MentorsController : ControllerBase
    {
        private readonly IMentorService mentorService;
        public MentorsController(IMentorService mentorService, IWebHostEnvironment env)
        {
            this.mentorService = mentorService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Mentor>>> Create([FromForm] MentorForCreationDto dto)
        {
            var result = await mentorService.CreateAsync(dto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Mentor>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await mentorService.GetAllAsync(@params);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Mentor>>> Get([FromRoute] Guid id)
        {
            var result = await mentorService.GetAsync(p => p.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Mentor>>> Update(Guid id, [FromForm] MentorForCreationDto dto)
        {
            var result = await mentorService.UpdateAsync(id, dto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var result = await mentorService.DeleteAsync(p => p.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<Mentor>> Login(MentorForLoginDto dto)
        {
            var result = await mentorService.LoginAsync(dto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPost("Image/{id}")]
        public async Task<ActionResult<Mentor>> SetImage(Guid id, IFormFile image)
        {
            var result = await mentorService.SetImageAsync(p => p.Id == id, image);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete("Image/{id}")]
        public async Task<ActionResult<Mentor>> DeleteImage(Guid id)
        {
            var result = await mentorService.DeleteImageAsync(p => p.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }
    }
}
