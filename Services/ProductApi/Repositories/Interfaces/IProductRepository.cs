using ProductApi.Data.Entities;

namespace ProductApi.Repositories.Interfaces;

public interface IProductRepository
{
    Task<bool> ProductExistsByTitle(string title);
    
    Task<bool> CreateProduct(Product product);
    
    Task<bool> DeleteProduct(Product product);
    
    Task<bool> UpdateProduct(Product product);
    
    Task<IEnumerable<Product>> GetAllProducts();
    
    Task<Product?> GetProductById(Guid productId);
    
    Task<Product?> GetProductByTitle(string title);
}
