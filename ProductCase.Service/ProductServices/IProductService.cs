using ProductCase.Dto;
using ProductCase.Dto.ProductDtos;
using System.Collections.Generic;

namespace ProductCase.Service.ProductServices
{
    public interface IProductService
    {
        ApiResultDto<List<ProductDetailDto>> Search(ProductFilterDto filter);
        ApiResultDto<ProductDetailDto> GetProductById(int id);
        ApiResultDto<ProductDetailDto> AddProduct(ProductAddDto dto);
        ApiResultDto<ProductDetailDto> UpdateProduct(ProductUpdateDto dto);
        ApiResultDto<bool> DeleteProduct(int id);
    }
}
