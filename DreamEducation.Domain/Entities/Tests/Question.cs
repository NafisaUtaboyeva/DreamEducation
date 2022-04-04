using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamEducation.Domain.Entities.Tests
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string VariantA { get; set; }
        public string VariantB { get; set; }
        public string VariantC { get; set; }
        public string VariantD { get; set; }
        public string Answer { get; internal set; }
        public Guid TestId { get; set; }

        [ForeignKey(nameof(TestId))]
        public Test Test { get; set; }
    }
}
