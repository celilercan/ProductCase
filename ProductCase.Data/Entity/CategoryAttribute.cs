using ProductCase.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCase.Data.Entity
{
    public class CategoryAttribute : BaseEntity
    {
        public ProductCategory ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }
        public string AttributeName { get; set; }
    }
}
