using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCase.Dto.ProductCategoryDtos
{
    public class ProductCategoryFilterDto
    {
        public string CategoryName { get; set; }
        public List<int> AttributeIds { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
