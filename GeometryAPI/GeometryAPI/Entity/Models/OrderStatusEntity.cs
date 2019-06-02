using GeometryAPI.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Entity.Models
{
    public class OrderStatusEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
