using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Entities.Chapters;
using DreamEducation.Domain.Entities.Mentors;
using DreamEducation.Domain.Entities.Tests;
using DreamEducation.Domain.Entities.Users;
using DreamEducation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamEducation.Domain.Entities.Courses
{
    public class Course : IAuditable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public Section Section { get; set; }
        public Degree Degree { get; set; }
        public long Views { get; set; } = 0;
        public string Image { get; set; }

        public Guid MentorId { get; set; }

        [ForeignKey(nameof(MentorId))]
        public Mentor Mentor { get; set; }

        public Guid TestId { get; set; }

        [ForeignKey(nameof(TestId))]
        public Test Test { get; set; }

        public virtual ICollection<Chapter> Chapters { get; set; }
        public virtual ICollection<Student> Students { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
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
