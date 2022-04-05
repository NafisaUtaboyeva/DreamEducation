using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamEducation.Domain.Entities.Tests
{
    public class Question : IAuditable
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string VariantA { get; set; }
        public string VariantB { get; set; }
        public string VariantC { get; set; }
        public string VariantD { get; set; }
        public string Answer { get; set; }
        public Guid TestId { get; set; }
        public ItemState State { get; set; }

        [ForeignKey(nameof(TestId))]
        public Test Test { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? UpdatedBy { get; set; }

        public void Create()
        {
            CreatedAt = DateTime.Now;
            State = ItemState.Created;
        }

        public void Delete()
        {
            DeletedAt = DateTime.Now;
            State = ItemState.Deleted;
        }

        public void Update()
        {
            UpdatedAt = DateTime.Now;
            State = ItemState.Updated;
        }
    }
}
