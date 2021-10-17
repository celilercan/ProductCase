using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCase.Dto.ProductCategoryDtos
{
    public class ProductCategoryUpdateDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<CategoryAttributeDto> Attributes { get; set; }
    }
}
