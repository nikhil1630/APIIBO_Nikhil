using IBO.Business.DTOs;
using IBO.Common;
using IBO.Common.Mapper;
using IBO.IRepository;
using IBO.Repository.DBContextUtility;
using IBO.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static IBO.Common.Enums;

namespace IBO.Repository
{
    public class UserAccessRepository : IUserAccessRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILoggerRepository _loggerRepository;
        private IDistributedCache _distributedCache;

        public UserAccessRepository(DataContext dataContext, ILoggerRepository loggerRepository, IDistributedCache distributedCache)
        {
            _dataContext = dataContext;
            _loggerRepository = loggerRepository;
            _distributedCache = distributedCache;
        }
        public async Task<List<UserDTOs>> GetAllUserDetails()
        {
            try
            {
                var cacheUser = _distributedCache.GetString(Constant.StudentEntity);

                if (cacheUser == null)
                {
                    var usertListFromDB = await _dataContext.Users.ToListAsync();
                    if (usertListFromDB == null)
                    {
                        return null;
                    }
                    cacheUser = System.Text.Json.JsonSerializer.Serialize(usertListFromDB);
                    GetDataFromCache(cacheUser, Constant.StudentEntity);
                    return EntityMapper<User, UserDTOs>.MapEntityCollection(usertListFromDB);
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<UserDTOs>>(cacheUser);
                }
            }

            catch (Exception ex)
            {
                await _loggerRepository.InsertIntoLog(ExceptionHelper.HandleException(ErrorLevel.Error.ToString(), ex.ToString(), $"Failed to get Student details from db."));
                return null;
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

        public User GetUserByEmailId(string emailid)
        {
           return _dataContext.Users.FirstOrDefault(x => x.Email == emailid);
           
        }
    }
}
