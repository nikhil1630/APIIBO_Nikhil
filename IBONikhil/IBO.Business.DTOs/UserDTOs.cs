using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IBO.Business.DTOs
{
    public class UserDTOs
    {
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string UserRole { get; set; }
    }
}
