using IBO.Business.DTOs;
using IBO.Common;
using IBO.IBusiness;
using IBO.IRepository;
using IBO.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBO.Business
{
    public class UserAccessService : IUserAccessService
    {
        private readonly IUserAccessRepository _userAccessRepository;

        public UserAccessService(IUserAccessRepository userAccessRepository)
        {
            _userAccessRepository = userAccessRepository;
        }

        public Task<List<UserDTOs>> GetAllUserDetails()
        {
            return _userAccessRepository.GetAllUserDetails();
        }

        public User GetUserByEmailId(string emailid)
        {
           return _userAccessRepository.GetUserByEmailId(emailid);
        }
    }
}