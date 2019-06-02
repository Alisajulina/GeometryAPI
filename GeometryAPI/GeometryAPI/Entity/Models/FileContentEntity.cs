using GeometryAPI.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Entity.Models
{
    public class FileContentEntity : BaseEntity
    {
        public ICollection<FileStorageEntity> FileStorage { get; set; }
        public byte[] FullContentData { get; set; }
        public byte[] CutContentData { get; set; }
    }
}
