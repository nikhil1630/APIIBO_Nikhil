using IBO.Business.DTOs;
using IBO.Common;
using IBO.Common.Mapper;
using IBO.IRepository;
using IBO.Repository.DBContextUtility;
using IBO.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static IBO.Common.Enums;

namespace IBO.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILoggerRepository _loggerRepository;
        private IDistributedCache _distributedCache;

        public StudentRepository(DataContext dataContext, ILoggerRepository loggerRepository, IDistributedCache distributedCache)
        {
            _dataContext = dataContext;
            _loggerRepository = loggerRepository;
            _distributedCache = distributedCache;
        }

        public async Task<List<StudentDTOs>> GetAllStudentDetails()
        {
            try
            {
                var cacheStudent = _distributedCache.GetString(Constant.StudentEntity);

                if (cacheStudent == null)
                {
                    var studentListFromDB = await _dataContext.Students.ToListAsync();
                    if (studentListFromDB == null)
                    {
                        return null;
                    }
                    cacheStudent = System.Text.Json.JsonSerializer.Serialize(studentListFromDB);
                    GetDataFromCache(cacheStudent,Constant.StudentEntity);
                    return EntityMapper<Student, StudentDTOs>.MapEntityCollection(studentListFromDB);
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<StudentDTOs>>(cacheStudent);
                }
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
                var cacheStudentName = await _distributedCache.GetStringAsync(Constant.StudentEntity);
                var listOfStudentFullName = new List<string>();
                if (cacheStudentName == null)
                {
                    var studentsFromDB = await _dataContext.Students.ToListAsync();
                    if (studentsFromDB == null)
                        return null;
                    

                    foreach (var studentFullName in studentsFromDB)
                    {
                        var student = studentFullName.FirstName + " " + studentFullName.LastName;

                        listOfStudentFullName.Add(student);
                    }
                    cacheStudentName = System.Text.Json.JsonSerializer.Serialize(listOfStudentFullName);
                    GetDataFromCache(cacheStudentName, Constant.StudentEntity);
                    return cacheStudentName.ToString();
                }
                else
                {
                    return cacheStudentName.ToString();
                }
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
                var cacheStudentName = await _distributedCache.GetStringAsync(Constant.StudentEntity);
                if (cacheStudentName == null)
                {
                    var studentByIdFromDB = await _dataContext.Students.FirstOrDefaultAsync(x => x.Id == id);
                    if (studentByIdFromDB == null)
                        return null;
                    cacheStudentName = System.Text.Json.JsonSerializer.Serialize(studentByIdFromDB);
                    GetDataFromCache(cacheStudentName, Constant.StudentEntity);

                    return EntityMapper<Student, StudentDTOs>.MapEntity(studentByIdFromDB);
                }
                else
                {
                    return JsonConvert.DeserializeObject<StudentDTOs>(cacheStudentName);
                }

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
                await _loggerRepository.InsertIntoLog(ExceptionHelper.HandleException(ErrorLevel.Error.ToString(), ex.ToString(), $"Failed to register Student with {student.FirstName + " " + student.LastName} in db."));
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
        private async void GetDataFromCache(string cacheStudent, string dataEntity)
        {
            try
            {
                var options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(DateTimeOffset.Now.AddMinutes(1));
                _distributedCache.SetString(dataEntity, cacheStudent, options);
            }
            catch (Exception ex)
            {
                await _loggerRepository.InsertIntoLog(ExceptionHelper.HandleException(ErrorLevel.Error.ToString(), ex.ToString(), $"Failed to Get Data from Redis Cache by {cacheStudent} "));
               
            }
        }
    }
}
