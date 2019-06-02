using GeometryAPI.Entity.Models.Base;
using GeometryAPI.Entity.Models.Many;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Entity.Models
{
    public class FileStorageEntity : BaseEntity
    {
        public Guid FileContentId { get; set; }
        public FileContentEntity FileContent { get; set; }

        public string FileName { get; set; }

        public ICollection<ProductFileEntity> ProductFileLink { get; set; }
    }
}
