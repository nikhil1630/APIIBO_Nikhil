using IBO.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBO.IRepository
{
   public interface ILoggerRepository
    {
        Task<bool> InsertIntoLog(Logger logger);
    }
}
