using GeometryAPI.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Entity.Models
{
    public class SubCategoryEntity : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public CategoryEntity Category { get; set; }

        public string Name { get; set; }
    }
}
