using ProductCase.Dto;
using ProductCase.Dto.ProductCategoryDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCase.Service.ProductCategoryServices
{
    public interface IProductCategoryService
    {
        ApiResultDto<List<ProductCategoryDetailDto>> Search(ProductCategoryFilterDto filter);
        ApiResultDto<ProductCategoryDetailDto> GetCategoryById(int id);
        ApiResultDto<ProductCategoryDetailDto> AddCategory(ProductCategoryAddDto dto);
        ApiResultDto<ProductCategoryDetailDto> UpdateCategory(ProductCategoryUpdateDto dto);
        ApiResultDto<bool> DeleteCategory(int id);
    }
}
