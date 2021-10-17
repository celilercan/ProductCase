using ProductCase.Common.Enums;
using ProductCase.Common.Extensions;
using ProductCase.Data;
using ProductCase.Data.Entity;
using ProductCase.Dto;
using ProductCase.Dto.ProductCategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductCase.Service.ProductCategoryServices
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IRepository<ProductCategory> _productCategoryRepo;

        public ProductCategoryService(IRepository<ProductCategory> productCategoryRepo)
        {
            _productCategoryRepo = productCategoryRepo;
        }

        public ApiResultDto<ProductCategoryDetailDto> AddCategory(ProductCategoryAddDto dto)
        {
            var result = new ApiResultDto<ProductCategoryDetailDto>();

            try
            {
                var entity = new ProductCategory
                {
                    Name = dto.CategoryName,
                    CategoryAttributes = dto.Attributes.Select(x => new CategoryAttribute
                    {
                        AttributeName = x
                    }).ToList()
                };

                _productCategoryRepo.Add(entity);

                result.Data = new ProductCategoryDetailDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    CreatedDate = entity.CreatedDate,
                    Attributes = entity.CategoryAttributes.Select(x => new CategoryAttributeDto
                    {
                        Id = x.Id,
                        AttributeName = x.AttributeName
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

        public ApiResultDto<bool> DeleteCategory(int id)
        {
            var result = new ApiResultDto<bool>();

            try
            {
                result.Data = _productCategoryRepo.Delete(id);
            }
            catch (Exception ex)
            {
                result.Status = ResponseStatusEnum.Exception;
                result.Message = ex.Message;
            }

            return result;
        }

        public ApiResultDto<ProductCategoryDetailDto> GetCategoryById(int id)
        {
            var result = new ApiResultDto<ProductCategoryDetailDto>();
            try
            {
                var entity = _productCategoryRepo.GetById(id);
                if (entity == null)
                {
                    result.Status = ResponseStatusEnum.NotFound;
                    result.Message = "Product Category is not found";
                    return result;
                }

                result.Data = new ProductCategoryDetailDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Attributes = entity.CategoryAttributes.Select(x => new CategoryAttributeDto
                    {
                        Id = x.Id,
                        AttributeName = x.AttributeName
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

        public ApiResultDto<List<ProductCategoryDetailDto>> Search(ProductCategoryFilterDto filter)
        {
            var result = new ApiResultDto<List<ProductCategoryDetailDto>>();
            try
            {
                result.Data = _productCategoryRepo.Query()
                        .AppendWhereIf(x => x.Name.ToLower().Contains(filter.CategoryName.ToLower()), !string.IsNullOrEmpty(filter.CategoryName))
                        .AppendWhereIf(x => x.CategoryAttributes.Any(y => filter.AttributeIds.Contains(y.Id)), filter.AttributeIds != null && filter.AttributeIds.Any())
                        .OrderBy(x => x.CreatedDate)
                        .Skip(filter.PageIndex)
                        .Take(filter.PageSize)
                        .Select(x => new ProductCategoryDetailDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Attributes = x.CategoryAttributes.Select(y => new CategoryAttributeDto
                            {
                                Id = y.Id,
                                AttributeName = y.AttributeName
                            }).ToList(),
                            CreatedDate = x.CreatedDate
                        }).ToList();
            }
            catch (Exception ex)
            {
                result.Status = ResponseStatusEnum.Exception;
                result.Message = ex.Message;
            }

            return result;
        }

        public ApiResultDto<ProductCategoryDetailDto> UpdateCategory(ProductCategoryUpdateDto dto)
        {
            var result = new ApiResultDto<ProductCategoryDetailDto>();
            try
            {
                var entity = _productCategoryRepo.GetById(dto.Id);
                if (entity == null)
                {
                    result.Status = ResponseStatusEnum.NotFound;
                    result.Message = "Product Category is not found";
                    return result;
                }

                entity.Name = dto.CategoryName;

                var currentAttrIds = entity.CategoryAttributes.Select(x => x.Id);
                var removeIds = currentAttrIds.Except(dto.Attributes.Where(x => x.Id.HasValue).Select(x => x.Id.Value));
                var updateIds = dto.Attributes.Where(x => x.Id.HasValue).Where(x => currentAttrIds.Contains(x.Id.Value)).Select(x => x.Id);

                entity.CategoryAttributes.RemoveAll(x => removeIds.Contains(x.Id));
                entity.CategoryAttributes.AddRange(dto.Attributes.Where(y => !y.Id.HasValue).Select(k => new CategoryAttribute
                {
                    AttributeName = k.AttributeName,
                    ProductCategoryId = entity.Id
                }).ToList());

                foreach (var item in entity.CategoryAttributes.Where(x => updateIds.Contains(x.Id)))
                {
                    item.AttributeName = item.AttributeName;
                }

                _productCategoryRepo.Update(entity);

                result.Data = new ProductCategoryDetailDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Attributes = entity.CategoryAttributes.Select(x => new CategoryAttributeDto
                    {
                        Id = x.Id,
                        AttributeName = x.AttributeName
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
