using IBO.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBO.API.Validator
{
    public interface IStudentValidator
    {
        bool CanPostSchool(StudentDTOs studentDTOs);
        bool CanUpdateSchool(StudentDTOs  studentDTOs);
    }
}
