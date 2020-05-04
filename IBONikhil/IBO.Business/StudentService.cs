using IBO.Business.DTOs;
using IBO.Common;
using IBO.IBusiness;
using IBO.IRepository;
using IBO.Repository.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static IBO.Common.Enums;

namespace IBO.Business
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILoggerRepository _loggerRepository;

        public StudentService(IStudentRepository studentRepository, ILoggerRepository loggerRepository)
        {
            _studentRepository = studentRepository;
            _loggerRepository = loggerRepository;
        }
        public async Task<List<StudentDTOs>> GetAllStudentDetails()
        {
            try
            {
                return await _studentRepository.GetAllStudentDetails();
            }
            catch (Exception ex)
            {
                await _loggerRepository.InsertIntoLog(ExceptionHelper.HandleException(ErrorLevel.Error.ToString(), ex.ToString(), $"Failed to get Student details."));
                return null;
            }

        }

        public async Task<IEnumerable> GetAllStudentName()
        {
            try
            {
                var studentName = await _studentRepository.GetAllStudentName();
                return studentName;
            }
            catch (Exception ex)
            {
                await _loggerRepository.InsertIntoLog(ExceptionHelper.HandleException(ErrorLevel.Error.ToString(), ex.ToString(), $"Failed to get Student Name."));
                return null;
            }
        }

        public async Task<StudentDTOs> GetStudentByID(int? id)
        {
            try
            {
                return await _studentRepository.GetStudentByID(id);
            }
            catch (Exception ex)
            {
                await _loggerRepository.InsertIntoLog(ExceptionHelper.HandleException(ErrorLevel.Error.ToString(), ex.ToString(), $"Failed to get Student by {id}."));
                return null;
            }

        }
        public async Task<bool> Register(StudentDTOs studentDTOs)
        {
            try
            {
                var createStudent = new Student
                {
                    FirstName = studentDTOs.FirstName,
                    LastName = studentDTOs.LastName,
                    MobileNumber = studentDTOs.MobileNumber,
                    DateOfBirth = studentDTOs.DateOfBirth,
                    Email = studentDTOs.Email
                };
                await _studentRepository.Register(createStudent);
                return true;
            }
            catch (Exception ex)
            {
                await _loggerRepository.InsertIntoLog(ExceptionHelper.HandleException(ErrorLevel.Error.ToString(), ex.ToString(), $"Failed to register Student by {studentDTOs.FirstName + " " + studentDTOs.LastName}."));
                return false;
            }

        }
        public async Task<bool> DeleteStudentById(int? id)
        {
            try
            {
                return await _studentRepository.DeleteStudentById(id);
            }
            catch (Exception ex)
            {
                await _loggerRepository.InsertIntoLog(ExceptionHelper.HandleException(ErrorLevel.Error.ToString(), ex.ToString(), $"Failed to delete Student by {id}."));
                return false;
            }

        }

        public async Task<bool> Update(int id, StudentDTOs studentDTOs)
        {
            try
            {
                var createStudent = new Student
                {
                    FirstName = studentDTOs.FirstName,
                    LastName = studentDTOs.LastName,
                    MobileNumber = studentDTOs.MobileNumber,
                    DateOfBirth = studentDTOs.DateOfBirth,
                    Email = studentDTOs.Email
                };
                await _studentRepository.UpdateStudent(id, createStudent);
                return true;
            }
            catch (Exception ex)
            {

                await _loggerRepository.InsertIntoLog(ExceptionHelper.HandleException(ErrorLevel.Error.ToString(), ex.ToString(), $"Failed to update Student by {studentDTOs.FirstName + " " + studentDTOs.LastName}."));
                return false;
            }
        }
    }
}
