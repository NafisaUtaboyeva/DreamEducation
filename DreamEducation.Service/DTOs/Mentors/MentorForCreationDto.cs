using Microsoft.AspNetCore.Http;

namespace DreamEducation.Service.DTOs.Mentors
{
    public class MentorForCreationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IFormFile Image { get; set; }
    }
}
