using System;
using System.Collections.Generic;
using System.Text;

namespace IBO.Business.DTOs
{
    public class LoggerDTOs
    {
        public string Exception { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string Level { get; set; }
    }
}
