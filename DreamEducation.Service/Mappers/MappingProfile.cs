using AutoMapper;
using DreamEducation.Domain.Entities.Chapters;
using DreamEducation.Domain.Entities.Courses;
using DreamEducation.Domain.Entities.Mentors;
using DreamEducation.Domain.Entities.Tests;
using DreamEducation.Domain.Entities.Users;
using DreamEducation.Service.DTOs;
using DreamEducation.Service.DTOs.Chapters;
using DreamEducation.Service.DTOs.Courses;
using DreamEducation.Service.DTOs.Mentors;
using DreamEducation.Service.DTOs.Questions;
using DreamEducation.Service.DTOs.Students;

namespace DreamEducation.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentForCreationDto, Student>().ReverseMap();
            CreateMap<MentorForCreationDto, Mentor>().ReverseMap();
            CreateMap<CourseForCreationDto, Course>().ReverseMap();
            CreateMap<ChapterForCreationDto, Chapter>().ReverseMap();
            CreateMap<TestForCreationDto, Test>().ReverseMap();
            CreateMap<QuestionForCreationDto, Question>().ReverseMap();
        }
    }
}
