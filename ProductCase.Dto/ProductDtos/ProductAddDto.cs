using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductCase.Dto.ProductDtos
{
    public class ProductAddDto
    {
        [StringLength(250, ErrorMessage = "You can enter up to 250 characters.")]
        [Required(ErrorMessage = "Product Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product Catalog Id is required.")]
        public int? ProductCatalogId { get; set; }

        [Required(ErrorMessage = "Product Price is required.")]
        public decimal? Price { get; set; }

        public List<ProductAttributeAddUpdateDto> Attributes { get; set; }
    }
}
