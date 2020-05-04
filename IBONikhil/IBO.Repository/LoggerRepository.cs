using IBO.IRepository;
using IBO.Repository.DBContextUtility;
using IBO.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBO.Repository
{
    public class LoggerRepository : ILoggerRepository
    {
        private readonly DataContext _dataContext;

        public LoggerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> InsertIntoLog(Logger logger)
        {
            logger.Date = DateTime.Now;
            await _dataContext.Loggers.AddAsync(logger);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}
