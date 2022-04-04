﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamEducation.Service.DTOs.Questions
{
    public class QuestionForCreationDto
    {
        public string Text { get; set; }
        public string VariantA { get; set; }
        public string VariantB { get; set; }
        public string VariantC { get; set; }
        public string VariantD { get; set; }
        public string Answer { get;  set; }
        public Guid TestId { get; set; }
    }
}
