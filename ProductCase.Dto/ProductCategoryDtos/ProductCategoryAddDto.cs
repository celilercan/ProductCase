using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductCase.Dto.ProductCategoryDtos
{
    public class ProductCategoryAddDto
    {
        [StringLength(50, ErrorMessage = "You can enter up to 50 characters.")]
        [Required(ErrorMessage = "Category Name is required.")]
        public string CategoryName { get; set; }

        public List<string> Attributes { get; set; }
    }
}
