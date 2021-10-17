using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductCase.Api.Controllers;
using ProductCase.Caching;
using ProductCase.Dto;
using ProductCase.Dto.ProductDtos;
using ProductCase.Service.ProductServices;
using Xunit;

namespace ProductCase.Test
{
    public class ProductTest
    {
        private readonly Mock<IProductService> _productService;
        private readonly Mock<ICacheManager> _cacheManager;
        private readonly Mock<HttpContext> _contextMock;

        public ProductTest(Mock<IProductService> productService, Mock<ICacheManager> cacheManager, Mock<HttpContext> contextMock)
        {
            _productService = productService;
            _cacheManager = cacheManager;
            _contextMock = contextMock;
        }

        [Fact]
        public void AddProduct_ShouldSuccess_WhenValidInputs_Test()
        {
            //Arrange
            var fakeCatalogId = 5;
            var fakeResultDto = new ApiResultDto<ProductDetailDto>();

            var result = _productService.Setup(x => x.AddProduct(It.IsAny<ProductAddDto>()))
                .Returns(fakeResultDto);

            //Act
            var productController = new ProductController(_productService.Object, _cacheManager.Object);
            productController.ControllerContext.HttpContext = _contextMock.Object;

            var actionResult = productController.AddProduct(new ProductAddDto { Name = "Test Product", Price = 100, ProductCatalogId = fakeCatalogId });

            //Assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);

            Assert.Equal(viewResult.StatusCode, StatusCodes.Status200OK);
        }
    }
}
