using GeometryAPI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Data.Response
{
    public class CategoriesResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<SubCategoryEntity> SubCategories { get; set; }
    }
}
