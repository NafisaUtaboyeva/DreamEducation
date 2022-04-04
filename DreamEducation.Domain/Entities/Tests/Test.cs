using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Enums;
using System;
using System.Collections.Generic;

namespace DreamEducation.Domain.Entities.Tests
{
    public class Test : IAuditable
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int AmountOfQuestions { get; set; }
        public virtual ICollection<Question> Questions { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public ItemState State { get; set; }

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
