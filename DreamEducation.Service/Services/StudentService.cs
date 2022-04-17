using AutoMapper;
using DreamEducation.Data.IRepositories;
using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Configurations;
using DreamEducation.Domain.Entities.Users;
using DreamEducation.Domain.Enums;
using DreamEducation.Service.DTOs.Students;
using DreamEducation.Service.Extensions;
using DreamEducation.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DreamEducation.Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Student>> CreateAsync(StudentForCreationDto studentDto)
        {
            var response = new BaseResponse<Student>();

            var existStudent = await unitOfWork.Students.GetAsync(p => p.Username == studentDto.Username || p.Email == studentDto.Email);
            if (existStudent is not null)
            {
                response.Error = new ErrorResponse(400, "User is exist!");
                return response;
            }

            var mappedStudent = mapper.Map<Student>(studentDto);
            mappedStudent.Password = studentDto.Password.ToHash256();
            mappedStudent.Image = await FileExtensions.SaveFileAsync(studentDto.Image.OpenReadStream(), studentDto.Image.FileName, config, env);

            var result = await unitOfWork.Students.CreateAsync(mappedStudent);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Student, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var existStudent = await unitOfWork.Students.GetAsync(expression);
            if (existStudent is null || existStudent.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }
            existStudent.Delete();

            await unitOfWork.Students.UpdateAsync(existStudent);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<Student>> DeleteImageAsync(Expression<Func<Student, bool>> expression)
        {
            var response = new BaseResponse<Student>();

            var student = await unitOfWork.Students.GetAsync(expression);

            if (student is null || student.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }
            student.Image = null;
            student.Update();

            await unitOfWork.Students.UpdateAsync(student);
            await unitOfWork.SaveChangesAsync();

            response.Data = student;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Student>>> GetAllAsync(PaginationParams @params, Expression<Func<Student, bool>> expression = null)
        {
            var response = new BaseResponse<IEnumerable<Student>>();

            var students = await unitOfWork.Students.GetAllAsync(expression);

            students = students.Where(st => st.State != ItemState.Deleted);

            response.Data = students.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Student>> GetAsync(Expression<Func<Student, bool>> expression)
        {
            var response = new BaseResponse<Student>();

            var student = await unitOfWork.Students.GetAsync(expression);
            if (student is null || student.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }

            response.Data = student;

            return response;
        }

        public async Task<BaseResponse<Student>> LoginAsync(StudentForLoginDto dto)
        {
            var response = new BaseResponse<Student>();

            var password = dto.Password.ToHash256();

            var user = await unitOfWork.Students.GetAsync(p => p.Username == dto.Username && p.Password == password);

            if (user is null)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }

            response.Data = user;

            return response;
        }

        public async Task<BaseResponse<Student>> SetImageAsync(Expression<Func<Student, bool>> expression, IFormFile image)
        {
            var response = new BaseResponse<Student>();

            var user = await unitOfWork.Students.GetAsync(expression);

            if (user is null)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }

            user.Image = await FileExtensions.SaveFileAsync(image.OpenReadStream(), image.FileName, config, env);

            user.Update();
            await unitOfWork.Students.UpdateAsync(user);

            await unitOfWork.SaveChangesAsync();

            response.Data = user;

            return response;
        }

        public async Task<BaseResponse<Student>> UpdateAsync(Guid id, StudentForCreationDto studentDto)
        {
            var response = new BaseResponse<Student>();

            var student = await unitOfWork.Students.GetAsync(p => p.Id == id && p.State != ItemState.Deleted);
            if (student is null)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }

            student.FirstName = studentDto.FirstName;
            student.LastName = studentDto.LastName;
            student.Phone = studentDto.Phone;
            student.Email = studentDto.Email;
            student.Username = studentDto.Username;
            student.Password = studentDto.Password;
            student.Image = await FileExtensions.SaveFileAsync(studentDto.Image.OpenReadStream(), studentDto.Image.FileName, config, env);
            student.Update();

            student.Password = student.Password.ToHash256();

            var result = await unitOfWork.Students.UpdateAsync(student);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }
    }
}
