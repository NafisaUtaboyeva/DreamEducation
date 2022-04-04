using DreamEducation.Domain.Entities.ManyToMany;
using System.Collections.Generic;

namespace DreamEducation.Domain.Entities.Users
{
    public class Student : User
    {
        public ICollection<CourseStudent> Courses { get; set; }
    }
}
