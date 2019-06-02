using GeometryAPI.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryAPI.Entity.Models
{
   public class UserEntity : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Guid GenderId { get; set; }
        public GenderEntity Gender { get; set; }

        public Guid RoleId { get; set; }
        public RoleEntity Role { get; set; }

        public ICollection<ShoppingCartEntity> ShoppingCartLink { get; set; }
        public ICollection<OrderEntity> OrderLink { get; set; }
        
    }
}
