﻿using ProductsService.BusinessLogic.Dtos;
using ProductsService.DataAccess.Entities;
using System.Linq.Expressions;

namespace ProductsService.BusinessLogic.ServiceContracts;

public interface IProductsService
{
    Task<List<ProductResponse>> GetProductsAsync();
    Task<List<ProductResponse>> GetProductsByConditionAsync(Expression<Func<Product, bool>> expression);
    Task<ProductResponse?> GetProductByConditionAsync(Expression<Func<Product, bool>> expression);

    Task<ProductResponse?> AddProductAsync(ProductAddRequest productAddRequest);
    Task<ProductResponse?> UpdateProductAsync(ProductUpdateRequest productUpdateRequest);
    Task<bool> DeleteProductAsync(Guid productID);
}
