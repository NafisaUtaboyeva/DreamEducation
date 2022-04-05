using DreamEducation.Domain.Entities.Chapters;
using DreamEducation.Domain.Entities.Courses;
using DreamEducation.Domain.Entities.Mentors;
using DreamEducation.Domain.Entities.Tests;
using DreamEducation.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace DreamEducation.Data.Contexts
{
    public class DreamEduDbContext : DbContext
    {
        public DreamEduDbContext(DbContextOptions<DreamEduDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Mentor> Mentors { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Chapter> Chapters { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Question> Questions { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<CourseStudent>()
        //        .HasKey(cs => new { cs.CourseId, cs.StudentId });

        //    builder.Entity<CourseStudent>()
        //        .HasOne(cs => cs.Course)
        //        .WithMany(cs => cs.Students)
        //        .HasForeignKey(cs => cs.CourseId);

        //    builder.Entity<CourseStudent>()
        //        .HasOne(cs => cs.Student)
        //        .WithMany(cs => cs.Courses)
        //        .HasForeignKey(cs => cs.StudentId);
        //}

    }
}
