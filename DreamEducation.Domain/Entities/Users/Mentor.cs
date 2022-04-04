using DreamEducation.Domain.Entities.Courses;
using DreamEducation.Domain.Entities.Users;
using System.Collections.Generic;

namespace DreamEducation.Domain.Entities.Mentors
{
    public class Mentor : User
    {
        public virtual ICollection<Course> Courses { get; set; }
    }
}
