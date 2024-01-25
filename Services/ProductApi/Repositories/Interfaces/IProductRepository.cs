using ProductApi.Models.Entities;

namespace ProductApi.Repositories.Interfaces;

public interface IProductRepository
{
    Task DeleteProduct(Product product);
    Task<Product> CreateProduct(Product product);
    Task<Product> UpdateProduct(Product product);
    Task<Product?> GetProductById(Guid id);
    Task<IEnumerable<Product>> GetAllProducts();
}
