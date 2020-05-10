using IBO.Business.DTOs;
using IBO.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBO.IBusiness
{
    public interface IUserAccessService
    {
        Task<List<UserDTOs>> GetAllUserDetails();
        User GetUserByEmailId(string emailid);
    }
}
