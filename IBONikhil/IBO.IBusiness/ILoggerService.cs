using IBO.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBO.IBusiness
{
    public interface ILoggerService
    {
        Task<bool> InsertIntoLog(LoggerDTOs loggerDTOs);
    }
}
