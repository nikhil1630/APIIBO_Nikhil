using System;
using System.Collections.Generic;
using System.Text;

namespace IBO.Repository.Entities
{
    public class User
    {
          public int Id { get; set; }

        public string Name {get; set;}

        public string Email { get; set; }

        public string UserRole { get; set; }
    }
}
