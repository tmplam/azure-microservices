using ProductsService.DataAccess.Entities;
using System.Linq.Expressions;

namespace ProductsService.DataAccess.RepositoryContracts;

public interface IProductsRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<IEnumerable<Product>> GetProductsByConditionAsync(Expression<Func<Product, bool>> expression);
    Task<Product?> GetProductByConditionAsync(Expression<Func<Product, bool>> expression);

    Task<Product?> AddProductAsync(Product product);
    Task<Product?> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(Guid productID);
}
