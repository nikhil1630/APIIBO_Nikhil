using IBO.Business.DTOs;
using IBO.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBO.Common
{
    public static class ExceptionHelper
    {
        public static Logger HandleException(string level, string ex, string message)
        {
            return new Logger()
            {
                Level = level,
                Exception = ex,
                Message = message,

            };
        }
    }
}
