using ProductCase.Common.Enums;
using ProductCase.Common.Extensions;
using ProductCase.Data;
using ProductCase.Data.Entity;
using ProductCase.Dto;
using ProductCase.Dto.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductCase.Service.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepo;

        public ProductService(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        public ApiResultDto<ProductDetailDto> AddProduct(ProductAddDto dto)
        {
            var result = new ApiResultDto<ProductDetailDto>();

            try
            {
                var entity = new Product
                {
                    Name = dto.Name,
                    Price = dto.Price.Value,
                    ProductCatalogId = dto.ProductCatalogId,
                    ProductAttributes = dto.Attributes.Select(x => new ProductAttribute 
                    {
                        CategoryAttributeId = x.CategoryAttributeId,
                        AttributeValue = x.AttributeValue
                    }).ToList()
                };

                _productRepo.Add(entity);

                result.Data = new ProductDetailDto
                {
                    Id = entity.Id,
                    CategoryName = entity.ProductCatalog.Name,
                    Name = entity.Name,
                    Price = entity.Price,
                    Attributes = entity.ProductAttributes.Select(x => new ProductAttributeDetailDto
                    {
                        AttributeName = x.CategoryAttribute.AttributeName,
                        AttributeValue = x.AttributeValue
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                result.Status = ResponseStatusEnum.Exception;
                result.Message = ex.Message;
            }

            return result;
        }

        public ApiResultDto<bool> DeleteProduct(int id)
        {
            var result = new ApiResultDto<bool>();

            try
            {
                result.Data = _productRepo.Delete(id);
            }
            catch (Exception ex)
            {
                result.Status = ResponseStatusEnum.Exception;
                result.Message = ex.Message;
            }

            return result;
        }

        public ApiResultDto<ProductDetailDto> GetProductById(int id)
        {
            var result = new ApiResultDto<ProductDetailDto>();
            try
            {
                var entity = _productRepo.GetById(id);
                if (entity == null)
                {
                    result.Status = ResponseStatusEnum.NotFound;
                    result.Message = "Product is not found";
                    return result;
                }

                result.Data = new ProductDetailDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    CategoryName = entity.ProductCatalog.Name,
                    Price = entity.Price,
                    Attributes = entity.ProductAttributes.Select(x => new ProductAttributeDetailDto
                    {
                        AttributeName = x.CategoryAttribute.AttributeName,
                        AttributeValue = x.AttributeValue
                    }).ToList(),
                    CreatedDate = entity.CreatedDate
                };
            }
            catch (Exception ex)
            {
                result.Status = ResponseStatusEnum.Exception;
                result.Message = ex.Message;
            }
            return result;
        }

        public ApiResultDto<List<ProductDetailDto>> Search(ProductFilterDto filter)
        {
            var result = new ApiResultDto<List<ProductDetailDto>>();
            try
            {
                result.Data = _productRepo.Query()
                        .AppendWhereIf(x => x.Name.ToLower().Contains(filter.Name.ToLower()), !string.IsNullOrEmpty(filter.Name))
                        .AppendWhereIf(x => x.ProductCatalog.Name.ToLower().Contains(filter.CategoryName.ToLower()), !string.IsNullOrEmpty(filter.CategoryName))
                        .AppendWhereIf(x => x.Price >= filter.MinPrice, filter.MinPrice.HasValue)
                        .AppendWhereIf(x => x.Price <= filter.MaxPrice, filter.MaxPrice.HasValue)
                        .AppendWhereIf(x => x.ProductAttributes.Any(y => filter.AttributeIds.Contains(y.Id)), filter.AttributeIds != null && filter.AttributeIds.Any())
                        .OrderBy(x => x.CreatedDate)
                        .Skip(filter.PageIndex)
                        .Take(filter.PageSize)
                        .Select(x => new ProductDetailDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                            CategoryName = x.ProductCatalog.Name,
                            Price = x.Price,
                            CreatedDate = x.CreatedDate,
                            Attributes = x.ProductAttributes.Select(y => new ProductAttributeDetailDto
                            {
                                AttributeName = y.CategoryAttribute.AttributeName,
                                AttributeValue = y.AttributeValue
                            }).ToList()
                        }).ToList();
            }
            catch (Exception ex)
            {
                result.Status = ResponseStatusEnum.Exception;
                result.Message = ex.Message;
            }

            return result;
        }

        public ApiResultDto<ProductDetailDto> UpdateProduct(ProductUpdateDto dto)
        {
            var result = new ApiResultDto<ProductDetailDto>();
            try
            {
                var entity = _productRepo.GetById(dto.Id);
                if (entity == null)
                {
                    result.Status = ResponseStatusEnum.NotFound;
                    result.Message = "Product is not found";
                    return result;
                }

                entity.Name = dto.Name;
                entity.Price = dto.Price.Value;
                entity.ProductCatalogId = dto.ProductCatalogId;

                var currentAttrIds = entity.ProductAttributes.Select(x => x.CategoryAttributeId);
                var removeIds = currentAttrIds.Except(dto.Attributes.Select(x => x.CategoryAttributeId));
                var updateIds = dto.Attributes.Where(x => currentAttrIds.Contains(x.CategoryAttributeId)).Select(x => x.CategoryAttributeId);
                var addIds = dto.Attributes.Select(x => x.CategoryAttributeId).Except(currentAttrIds);

                entity.ProductAttributes.RemoveAll(x => removeIds.Contains(x.CategoryAttributeId));
                entity.ProductAttributes.AddRange(dto.Attributes.Where(y => addIds.Contains(y.CategoryAttributeId)).Select(k => new ProductAttribute
                {
                    CategoryAttributeId = k.CategoryAttributeId,
                    AttributeValue = k.AttributeValue,
                    ProductId = entity.Id
                }).ToList());

                foreach (var item in entity.ProductAttributes.Where(x => updateIds.Contains(x.CategoryAttributeId)))
                {
                    item.AttributeValue = item.AttributeValue;
                }

                _productRepo.Update(entity);

                result.Data = new ProductDetailDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    CategoryName = entity.ProductCatalog.Name,
                    Price = entity.Price,
                    Attributes = entity.ProductAttributes.Select(x => new ProductAttributeDetailDto
                    {
                        AttributeName = x.CategoryAttribute.AttributeName,
                        AttributeValue = x.AttributeValue
                    }).ToList(),
                    CreatedDate = entity.CreatedDate
                };
            }
            catch (Exception ex)
            {
                result.Status = ResponseStatusEnum.Exception;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
