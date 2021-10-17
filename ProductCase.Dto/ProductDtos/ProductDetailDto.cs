using ProductCase.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCase.Dto.ProductDtos
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<ProductAttributeDetailDto> Attributes { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
