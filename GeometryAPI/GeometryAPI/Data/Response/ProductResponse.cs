using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Data.Response
{
    public class ProductResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Descriprion { get; set; }

        public decimal Price { get; set; }

        public long VendorCode { get; set; }

        public string AvailableSizeId { get; set; }

        public Guid[] Thumbnail { get; set; }
    }
}
