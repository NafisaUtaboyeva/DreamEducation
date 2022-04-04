using DreamEducation.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamEducation.Service.DTOs.Courses
{
    public class CourseForCreationDto
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public Section Section { get; set; }
        public Degree Degree { get; set; }
        public IFormFile Image { get; set; }
        public Guid MentorId { get; set; }
        public Guid TestId { get; set; }
    }
}
