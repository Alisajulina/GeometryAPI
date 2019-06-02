using GeometryAPI.Entity.Models.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Entity.Models
{
    public class RoleEntity : BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public List<UserEntity> Users { get; set; }

        public RoleEntity()
        {
            Users = new List<UserEntity>();
        }
    }
}
