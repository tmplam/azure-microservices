using AutoMapper;
using FluentValidation;
using ProductsService.BusinessLogic.Dtos;
using ProductsService.BusinessLogic.ServiceContracts;
using ProductsService.DataAccess.Entities;
using ProductsService.DataAccess.RepositoryContracts;
using System.Linq.Expressions;

namespace ProductsService.BusinessLogic.Services;

public class ProductsService(
    IValidator<ProductAddRequest> _productAddRequestValidator,
    IValidator<ProductUpdateRequest> _productUpdateRequestValidator,
    IMapper _mapper,
    IProductsRepository _productsRepository)
    : IProductsService
{
    public async Task<ProductResponse?> AddProductAsync(ProductAddRequest productAddRequest)
    {
        if (productAddRequest is null)
            throw new ArgumentNullException(nameof(productAddRequest));

        var validationResult = await _productAddRequestValidator.ValidateAsync(productAddRequest);

        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ArgumentException(errors);
        }

        var product = _mapper.Map<Product>(productAddRequest);

        var addedProduct = await _productsRepository.AddProductAsync(product);

        if (addedProduct is null)
            return null;

        return _mapper.Map<ProductResponse>(addedProduct);
    }

    public async Task<bool> DeleteProductAsync(Guid productID)
    {
        var product = await _productsRepository.GetProductByConditionAsync(p => p.ProductID == productID);

        if (product is null)
            return false;

        return await _productsRepository.DeleteProductAsync(productID);
    }

    public async Task<ProductResponse?> GetProductByConditionAsync(Expression<Func<Product, bool>> expression)
    {
        var product = await _productsRepository.GetProductByConditionAsync(expression);
        if (product is null)
            return null;
        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<List<ProductResponse>> GetProductsAsync()
    {
        var products = await _productsRepository.GetProductsAsync();
        return _mapper.Map<List<ProductResponse>>(products);
    }

    public async Task<List<ProductResponse>> GetProductsByConditionAsync(Expression<Func<Product, bool>> expression)
    {
        var products = await _productsRepository.GetProductsByConditionAsync(expression);
        return _mapper.Map<List<ProductResponse>>(products);
    }

    public async Task<ProductResponse?> UpdateProductAsync(ProductUpdateRequest productUpdateRequest)
    {
        var existingProduct = await _productsRepository.GetProductByConditionAsync(p => p.ProductID == productUpdateRequest.ProductID);

        if (existingProduct is null)
            throw new ArgumentException("Product not found");

        var validationResult = await _productUpdateRequestValidator.ValidateAsync(productUpdateRequest);

        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ArgumentException(errors);
        }

        var product = _mapper.Map<Product>(productUpdateRequest);

        var updatedProduct = await _productsRepository.UpdateProductAsync(product);

        return _mapper.Map<ProductResponse>(updatedProduct);
    }
}
