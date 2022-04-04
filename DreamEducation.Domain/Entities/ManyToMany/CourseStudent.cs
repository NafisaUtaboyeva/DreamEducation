using DreamEducation.Domain.Entities.Courses;
using DreamEducation.Domain.Entities.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamEducation.Domain.Entities.ManyToMany
{
    public class CourseStudent
    {
        public Guid CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }
        public Guid StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }
    }
}
