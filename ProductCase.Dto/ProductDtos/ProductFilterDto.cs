using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCase.Dto.ProductDtos
{
    public class ProductFilterDto
    {
        public ProductFilterDto()
        {
            this.PageIndex = 1;
            this.PageSize = 20;
        }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public List<int> AttributeIds { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
