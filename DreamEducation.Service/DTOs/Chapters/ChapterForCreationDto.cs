using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamEducation.Service.DTOs.Chapters
{
    public class ChapterForCreationDto
    {
        public string Title { get; set; }
        public IFormFile Video { get; set; }
        public IFormFile Lection { get; set; }
        public Guid CourseId { get; set; }
    }
}
