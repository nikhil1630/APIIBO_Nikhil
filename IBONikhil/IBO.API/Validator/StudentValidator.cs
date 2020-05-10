using IBO.Business.DTOs;
using IBO.Common;
using IBO.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBO.API.Validator
{
    public class StudentValidator : IStudentValidator
    {
        private readonly IUserAccessService _userAccessService;

        public StudentValidator(IUserAccessService userAccessService)
        {
            _userAccessService = userAccessService;
        }
        public bool CanPostSchool(StudentDTOs studentDTOs)
        {
            var user = _userAccessService.GetUserByEmailId(studentDTOs.Email);
            if(user!=null)
            {
                return (user.UserRole == Enums.UserRole.Admin.ToString()) ? true : false;
            }
            return false;
           
        }

        public bool CanUpdateSchool(StudentDTOs studentDTOs)
        {
            throw new NotImplementedException();
        }
    }
}
