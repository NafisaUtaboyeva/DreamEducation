using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Courses;
using DreamEducation.Domain.Enums;
using DreamEducation.Service.DTOs.Courses;
using DreamEducation.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DreamEducation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Course>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await courseService.GetAllAsync(@params);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Get(Guid id)
        {
            var result = await courseService.GetAsync(p => p.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Course>>> Create([FromForm] CourseForCreationDto course)
        {
            var result = await courseService.CreateAsync(course);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Update(Guid id, CourseForCreationDto course)
        {
            var result = await courseService.UpdateAsync(id, course);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Delete(Guid id)
        {
            var result = await courseService.DeleteAsync(p => p.Id == id && p.State != ItemState.Deleted);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPost("Registeration/{courseId}&{studentId}")]
        public async Task<ActionResult<BaseResponse<Course>>> Register(Guid courseId, Guid userId)
        {
            var result = await courseService.RegisterStudentForCourse(courseId, userId);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }
    }
}
