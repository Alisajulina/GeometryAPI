using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Data.Response
{
    public class LoginResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string GenderId { get; set; }

        public string RoleId { get; set; }
    }
}
