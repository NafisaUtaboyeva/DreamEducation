using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Entities.Courses;
using DreamEducation.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamEducation.Domain.Entities.Chapters
{
    public class Chapter : IAuditable
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Video { get; set; }
        public string Lection { get; set; }

        public Guid CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }


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
