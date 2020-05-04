using IBO.Business.DTOs;
using IBO.IBusiness;
using IBO.IRepository;
using IBO.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBO.Business
{
    public class LoggerService : ILoggerService
    {
        private readonly ILoggerRepository _loggerRepository;

        public LoggerService(ILoggerRepository loggerRepository)
        {
            _loggerRepository = loggerRepository;
        }
        public async Task<bool> InsertIntoLog(LoggerDTOs loggerDTOs)
        {
            var createLog = new Logger
            {
                Exception = loggerDTOs.Exception,
                Message = loggerDTOs.Message,
                Level = loggerDTOs.Level,
                Date=loggerDTOs.Date
                
            };
            await _loggerRepository.InsertIntoLog(createLog);
            return true;
        }
    }
}
