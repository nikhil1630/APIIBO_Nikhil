using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBO.Business.DTOs;
using IBO.IBusiness;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IBO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        private readonly ILoggerService _loggerService;

        public LoggerController(ILoggerService loggerService )
        {
            this._loggerService = loggerService;
        }

       [HttpPost]
        public async Task<IActionResult> Logger([FromBody] LoggerDTOs loggerDTOs)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            await _loggerService.InsertIntoLog(loggerDTOs);
            return Ok();
        }
    }
}