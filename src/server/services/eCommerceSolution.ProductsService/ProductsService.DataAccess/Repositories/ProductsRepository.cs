using Microsoft.EntityFrameworkCore;
using ProductsService.DataAccess.Context;
using ProductsService.DataAccess.Entities;
using ProductsService.DataAccess.RepositoryContracts;
using System.Linq.Expressions;

namespace ProductsService.DataAccess.Repositories;

public class ProductsRepository(
    ApplicationDbContext _dbContext) 
    : IProductsRepository
{
    public async Task<Product?> AddProductAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProductAsync(Guid productID)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductID == productID);
        if (product is null)
            return false;

        _dbContext.Products.Remove(product);
        int rowAffected = await _dbContext.SaveChangesAsync();
        return rowAffected > 0;
    }

    public async Task<Product?> GetProductByConditionAsync(Expression<Func<Product, bool>> expression)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(expression);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByConditionAsync(Expression<Func<Product, bool>> expression)
    {
        return (await _dbContext.Products.AsQueryable().ToListAsync()).AsQueryable().Where(expression);
    }

    public async Task<Product?> UpdateProductAsync(Product product)
    {
        var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductID == product.ProductID);
        if (existingProduct is null)
            return null;

        existingProduct.ProductName = product.ProductName;
        existingProduct.Category = product.Category;
        existingProduct.UnitPrice = product.UnitPrice;
        existingProduct.QuantityInStock = product.QuantityInStock;

        await _dbContext.SaveChangesAsync();

        return existingProduct;
    }
}
