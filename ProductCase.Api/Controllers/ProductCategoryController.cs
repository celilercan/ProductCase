using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductCase.Caching;
using ProductCase.Caching.Extensions;
using ProductCase.Common.Constants;
using ProductCase.Common.Enums;
using ProductCase.Common.Extensions;
using ProductCase.Dto;
using ProductCase.Dto.ProductCategoryDtos;
using ProductCase.Dto.ProductDtos;
using ProductCase.Service.ProductCategoryServices;

namespace ProductCase.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly ICacheManager _cacheManager;

        public ProductCategoryController(IProductCategoryService productCategoryService, ICacheManager cacheManager)
        {
            _productCategoryService = productCategoryService;
            _cacheManager = cacheManager;
        }

        [HttpGet]
        [Route("getList")]
        public ApiResultDto<List<ProductCategoryDetailDto>> GetList([FromBody] ProductCategoryFilterDto filter)
        {
            var result = _productCategoryService.Search(filter);
            Response.StatusCode = result.Status.GetHttpStatusCode();
            return result;
        }

        [HttpGet]
        [Route("getById/{id}")]
        public ApiResultDto<ProductCategoryDetailDto> ProductCategoryGetById(int id)
        {
            var result = _cacheManager.Get(string.Format(Constant.ProductCategory.ProductCategoryDetailKey, id), 
                () => 
                {
                    return _productCategoryService.GetCategoryById(id);
                });

            Response.StatusCode = result.Status.GetHttpStatusCode();
            return result;
        }

        [HttpPost]
        [Route("addProductCategory")]
        public ApiResultDto<ProductCategoryDetailDto> AddProduct([FromBody] ProductCategoryAddDto dto)
        {
            var result = new ApiResultDto<ProductCategoryDetailDto>();

            if (!ModelState.IsValid)
            {
                result.Status = ResponseStatusEnum.ValidationError;
                result.Message = "Validation error.";
            }
            else
            {
                result = _productCategoryService.AddCategory(dto);
            }

            Response.StatusCode = result.Status.GetHttpStatusCode();
            return result;
        }

        [HttpPut]
        [Route("updateProductCategory")]
        public ApiResultDto<ProductCategoryDetailDto> UpdateProduct([FromBody] ProductCategoryUpdateDto dto)
        {
            var result = new ApiResultDto<ProductCategoryDetailDto>();

            if (!ModelState.IsValid)
            {
                result.Status = ResponseStatusEnum.ValidationError;
                result.Message = "Validation error.";
            }
            else
            {
                result = _productCategoryService.UpdateCategory(dto);
            }

            Response.StatusCode = result.Status.GetHttpStatusCode();
            return result;
        }

        [HttpDelete]
        [Route("deleteProductCategory/{id}")]
        public ApiResultDto<bool> Delete(int id)
        {
            var result = _productCategoryService.DeleteCategory(id);
            Response.StatusCode = result.Status.GetHttpStatusCode();
            return result;
        }
    }
}
