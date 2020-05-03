using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IBO.Business.DTOs;
using IBO.IBusiness;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IBO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<List<StudentDTOs>> GetAllStudent()
        {
            var getStudentsList = await _studentService.GetAllStudentDetails();
            return getStudentsList;
        }

        [HttpGet]
        [Route("GetAllStudentName")]
        public async Task<IEnumerable> GetAllStudentName()
        {
            var studentsByName = await _studentService.GetAllStudentName();
            return studentsByName;
        }

        [HttpGet]
        [Route("GetStudentById/{id}")]
        public async Task<StudentDTOs> GetStudentById(int? id)
        {
            if (id.HasValue)
            {
                var studentById = await _studentService.GetStudentByID(id);
                return studentById;
            }
            return null;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] StudentDTOs studentDTOs)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            await _studentService.Register(studentDTOs);
            return Ok("Student with name = " + studentDTOs.FirstName + " " + studentDTOs.LastName + " Registered successfully.");
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var deletdStudent = await _studentService.DeleteStudentById(id);
            if (deletdStudent)
                return Ok("Student with Id = " + id.ToString() + " deleted successfully.");
            return BadRequest("Student with Id = " + id.ToString() + " not found to delete");
        }

        [HttpPut]

        public async Task<IActionResult> Update(int id, [FromBody] StudentDTOs studentDTOs)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var updateStudent= await _studentService.Update(id, studentDTOs);
            if(updateStudent)
            return Ok("Student with ID = " + id.ToString() + " Updated successfully.");
            return BadRequest("Student with Id = " + id.ToString() + " not found to update");
        }
    }
}