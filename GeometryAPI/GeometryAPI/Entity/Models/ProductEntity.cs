using GeometryAPI.Entity.Models.Base;
using GeometryAPI.Entity.Models.Many;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Entity.Models
{
    public class ProductEntity : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public CategoryEntity Category { get; set; }

        public Guid SubCategoryId { get; set; }
        public SubCategoryEntity SubCategory { get; set; }

        public string Name { get; set; }

        public string ShortDescriprion { get; set; }
        public string Descriprion { get; set; }

        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }

        public long VendorCode { get; set; }

        public string AvailableSizeId { get; set; }

        public Guid BrandId { get; set; }
        public BrandEntity Brand { get; set; }

        public Guid ClothTypeId { get; set; }
        public ClothTypeEntity ClothType { get; set; }

        public ICollection<ProductFileEntity> ProductFileLink { get; set; }
        
    }
}
