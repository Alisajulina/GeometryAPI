using GeometryAPI.Entity.Models;
using GeometryAPI.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Entity.Models.Many
{
    public class ProductFileEntity : BaseEntity
    {
        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; }

        public Guid FileId { get; set; }
        public FileStorageEntity File { get; set; }


    }
}
