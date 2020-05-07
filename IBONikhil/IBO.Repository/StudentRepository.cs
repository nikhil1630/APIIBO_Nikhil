using IBO.Business.DTOs;
using IBO.Common;
using IBO.Common.Mapper;
using IBO.IRepository;
using IBO.Repository.DBContextUtility;
using IBO.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static IBO.Common.Enums;

namespace IBO.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILoggerRepository _loggerRepository;

        public StudentRepository(DataContext dataContext, ILoggerRepository loggerRepository)
        {
            _dataContext = dataContext;
            _loggerRepository = loggerRepository;
        }

        public async Task<List<StudentDTOs>> GetAllStudentDetails()
        {
            try
            {
                var studentListFromDB = await _dataContext.Students.ToListAsync();
                if (studentListFromDB == null)
                {
                    return null;
                }
                return EntityMapper<Student, StudentDTOs>.MapEntityCollection(studentListFromDB);
            }
            catch (Exception ex)
            {
                await _loggerRepository.InsertIntoLog(ExceptionHelper.HandleException(ErrorLevel.Error.ToString(), ex.ToString(), $"Failed to get Student details from db."));
                return null;
            }

        }

        public async Task<string> GetAllStudentName()
        {
            try
            {
                var listOfStudentFullName = new List<string>();

                var studentsFromDB = await _dataContext.Students.ToListAsync();
                if (studentsFromDB == null)
                    return null;

                foreach (var studentFullName in studentsFromDB)
                {
                    var student = studentFullName.FirstName + " " + studentFullName.LastName;

                    listOfStudentFullName.Add(student);
                }
                return listOfStudentFullName.ToString();
            }

            catch (Exception ex)
            {
                await _loggerRepository.InsertIntoLog(ExceptionHelper.HandleException(ErrorLevel.Error.ToString(), ex.ToString(), $"Failed to get Student name from db."));
                return null;
            }

        }

        public async Task<StudentDTOs> GetStudentByID(int? id)
        {
            try
            {
                var studentByIdFromDB = await _dataContext.Students.FirstOrDefaultAsync(x => x.Id == id);
                if (studentByIdFromDB == null)
                    return null;
                return EntityMapper<Student, StudentDTOs>.MapEntity(studentByIdFromDB);
            }
            catch (Exception ex)
            {
                await _loggerRepository.InsertIntoLog(ExceptionHelper.HandleException(ErrorLevel.Error.ToString(), ex.ToString(), $"Failed to get Student by {id} from db."));
                return null;
            }

        }

        public async Task<bool> Register(Student student)
        {
            try
            {
                await _dataContext.Students.AddAsync(student);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _loggerRepository.InsertIntoLog(ExceptionHelper.HandleException(ErrorLevel.Error.ToString(), ex.ToString(), $"Failed to register Student with {student.FirstName+" "+student.LastName} in db."));
                return false;
            }

        }

        public async Task<bool> UpdateStudent(int id, Student student)
        {
            try
            {
                var studentByIdFromDB = await _dataContext.Students.FirstOrDefaultAsync(x => x.Id == id);
                if (studentByIdFromDB == null)
                {
                    return false;
                }
                else
                {
                    studentByIdFromDB.FirstName = student.FirstName;
                    studentByIdFromDB.LastName = student.LastName;
                    studentByIdFromDB.MobileNumber = student.MobileNumber;
                    studentByIdFromDB.DateOfBirth = student.DateOfBirth;
                    studentByIdFromDB.Email = student.Email;

                    await _dataContext.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                await _loggerRepository.InsertIntoLog(ExceptionHelper.HandleException(ErrorLevel.Error.ToString(), ex.ToString(), $"Failed to update Student with {student.FirstName + " " + student.LastName} in db."));
                return false;
            }

        }
        public async Task<bool> DeleteStudentById(int? id)
        {
            try
            {
                var removeStudentFromDB = await _dataContext.Students.FirstOrDefaultAsync(x => x.Id == id);
                if (removeStudentFromDB == null)
                {
                    return false;
                }
                else
                {
                    _dataContext.Remove(removeStudentFromDB);
                    _dataContext.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                await _loggerRepository.InsertIntoLog(ExceptionHelper.HandleException(ErrorLevel.Error.ToString(), ex.ToString(), $"Failed to delete Student by {id} in db."));
                return false;
            }

        }
    }
}
