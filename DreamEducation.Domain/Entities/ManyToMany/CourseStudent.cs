using DreamEducation.Domain.Entities.Courses;
using DreamEducation.Domain.Entities.Users;
using System;

namespace DreamEducation.Domain.Entities.ManyToMany
{
    public class CourseStudent
    {
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
