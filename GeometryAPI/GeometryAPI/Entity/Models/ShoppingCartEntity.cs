using GeometryAPI.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Entity.Models
{
    public class ShoppingCartEntity : BaseEntity
    {
        //TODO
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; }


    }
}
