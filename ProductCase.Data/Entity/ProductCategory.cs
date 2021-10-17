using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCase.Data.Entity
{
    public class ProductCategory : BaseEntity
    {
        public string Name { get; set; }
        public List<CategoryAttribute> CategoryAttributes { get; set; }
    }
}
