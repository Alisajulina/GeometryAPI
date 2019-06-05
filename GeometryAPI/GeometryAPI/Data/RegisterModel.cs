using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Data
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Guid GenderId { get; set; }
    }
}
