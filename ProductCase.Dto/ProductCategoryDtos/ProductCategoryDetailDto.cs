using ProductCase.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCase.Dto.ProductCategoryDtos
{
    public class ProductCategoryDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryAttributeDto> Attributes { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
