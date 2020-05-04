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
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;

        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }
        //[HttpGet]
        //public async Task<List<BoardDTOs>> GetSchoolsByBoard(int boardId)
        //{
        //    return null;
        //}
    }
}