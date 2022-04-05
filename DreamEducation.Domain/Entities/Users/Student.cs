using DreamEducation.Domain.Entities.Courses;
using System.Collections.Generic;

namespace DreamEducation.Domain.Entities.Users
{
    public class Student : User
    {
        public ICollection<Course> Courses { get; set; }
    }
}
