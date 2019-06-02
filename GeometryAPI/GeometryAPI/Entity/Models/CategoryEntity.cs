using GeometryAPI.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Entity.Models
{
    public class CategoryEntity : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<SubCategoryEntity> SubCategoryLink { get; set; }
    }
}
