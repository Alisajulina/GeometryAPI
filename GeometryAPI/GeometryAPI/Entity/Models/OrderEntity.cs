using GeometryAPI.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Entity.Models
{
    public class OrderEntity : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public UserEntity Customer { get; set; }

        public DateTime ShippingDate { get; set; }
        public string Address { get; set; }

        public Guid OrderStatusId { get; set; }
        public OrderStatusEntity OrderStatus { get; set; }
    }
}
