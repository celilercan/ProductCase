using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCase.Data.Entity
{
    public class ProductAttribute : BaseEntity
    {
        public CategoryAttribute CategoryAttribute { get; set; }
        public int CategoryAttributeId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public string AttributeValue { get; set; }
    }
}
