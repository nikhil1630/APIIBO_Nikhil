using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IBO.API.Validator;
using IBO.Business.DTOs;
using IBO.IBusiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IBO.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentsController> _logger;

        private readonly IStudentValidator _studentValidator;
        public StudentsController(IStudentService studentService, ILogger<StudentsController> logger,IStudentValidator studentValidator)
        {
            _studentService = studentService;
            _logger = logger;
            _studentValidator = studentValidator;

        }
       
        [HttpGet, MapToApiVersion("2.0"),MapToApiVersion("1.0")]
        public async Task<List<StudentDTOs>> GetAllStudent()
        {
            try
            {
                var getStudentsList = await _studentService.GetAllStudentDetails();
                return getStudentsList;
            }
            catch (Exception ex )
            {
                _logger.LogError($"Failed to get all student data", ex);
                return null;
            }
        }

        
        [HttpGet, MapToApiVersion("1.0")]
        [Route("GetAllStudentName")]
        public async Task<string> GetAllStudentName()
        {
            try
            {
                var studentsByName = await _studentService.GetAllStudentName();
                return studentsByName.ToString();
            }

            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all student name data", ex);
                return null;
            }
        }

        [HttpGet]
        [Route("GetStudentById/{id}")]
        public async Task<StudentDTOs> GetStudentById(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var studentById = await _studentService.GetStudentByID(id);
                    return studentById;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all student by {id} ", ex);
                return null;
            }

        }

        [ApiVersion("2.0")]
        [HttpPost("register")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> Register([FromBody] StudentDTOs studentDTOs)
        {
            try
            {
                if (_studentValidator.CanPostSchool(studentDTOs))
                {
                    if (!ModelState.IsValid)
                        return BadRequest();
                    await _studentService.Register(studentDTOs);
                    return Ok("Student with name = " + studentDTOs.FirstName + " " + studentDTOs.LastName + " Registered successfully.");
                }
                return BadRequest("User access denied");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to register student", ex);
                return BadRequest();
            }

        }

        [ApiVersion("2.0")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var deletdStudent = await _studentService.DeleteStudentById(id);
                if (deletdStudent)
                    return Ok("Student with Id = " + id.ToString() + " deleted successfully.");
                return BadRequest("Student with Id = " + id.ToString() + " not found to delete");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to register student", ex);
                return BadRequest();
            }
        }


        [ApiVersion("2.0")]
        [HttpPut]

        public async Task<IActionResult> Update(int id, [FromBody] StudentDTOs studentDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var updateStudent = await _studentService.Update(id, studentDTOs);
                if (updateStudent)
                    return Ok("Student with ID = " + id.ToString() + " Updated successfully.");
                return BadRequest("Student with Id = " + id.ToString() + " not found to update");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to register student", ex);
                return BadRequest();
            }

        }
    }
}