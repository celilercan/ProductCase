using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCase.Data.Entity
{
    public class Product : BaseEntity
    {
        public ProductCategory ProductCatalog { get; set; }
        public int? ProductCatalogId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; }
    }
}
