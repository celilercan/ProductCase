using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductCase.Caching;
using ProductCase.Caching.Extensions;
using ProductCase.Common.Constants;
using ProductCase.Common.Enums;
using ProductCase.Common.Extensions;
using ProductCase.Dto;
using ProductCase.Dto.ProductDtos;
using ProductCase.Service.ProductServices;

namespace ProductCase.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICacheManager _cacheManager;

        public ProductController(IProductService productService, ICacheManager cacheManager)
        {
            _productService = productService;
            _cacheManager = cacheManager;
        }

        [HttpGet]
        [Route("getList")]
        public ApiResultDto<List<ProductDetailDto>> GetList([FromBody] ProductFilterDto filter)
        {
            var result = _productService.Search(filter);
            Response.StatusCode = result.Status.GetHttpStatusCode();
            return result;
        }

        [HttpGet]
        [Route("getById/{id}")]
        public ApiResultDto<ProductDetailDto> ProductGetById(int id)
        {
            var result = _cacheManager.Get(string.Format(Constant.Product.ProductDetailKey, id), 
                () => 
                {
                    return _productService.GetProductById(id);
                });
            Response.StatusCode = result.Status.GetHttpStatusCode();
            return result;
        }

        [HttpPost]
        [Route("addProduct")]
        public ApiResultDto<ProductDetailDto> AddProduct([FromBody] ProductAddDto dto)
        {
            var result = new ApiResultDto<ProductDetailDto>();
            if (!ModelState.IsValid)
            {
                result.Status = ResponseStatusEnum.ValidationError;
                result.Message = "Validation error.";
            }
            else
            {
                result = _productService.AddProduct(dto);
            }

            Response.StatusCode = result.Status.GetHttpStatusCode();
            return result;
        }

        [HttpPut]
        [Route("updateProduct")]
        public ApiResultDto<ProductDetailDto> UpdateProduct([FromBody] ProductUpdateDto dto)
        {
            var result = new ApiResultDto<ProductDetailDto>();
            if (!ModelState.IsValid)
            {
                result.Status = ResponseStatusEnum.ValidationError;
                result.Message = "Validation error.";
            }
            else
            {
                result = _productService.UpdateProduct(dto);
            }

            Response.StatusCode = result.Status.GetHttpStatusCode();
            return result;
        }

        [HttpDelete]
        [Route("deleteProduct/{id}")]
        public ApiResultDto<bool> Delete(int id)
        {
            var result = _productService.DeleteProduct(id);
            Response.StatusCode = result.Status.GetHttpStatusCode();
            return result;
        }
    }
}
