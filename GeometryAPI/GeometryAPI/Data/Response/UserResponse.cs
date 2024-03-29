﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Data.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Guid GenderId { get; set; }

        public Guid RoleId { get; set; }
    }
}
