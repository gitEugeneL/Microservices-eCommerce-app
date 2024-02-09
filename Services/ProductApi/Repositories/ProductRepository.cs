using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Entities;
using ProductApi.Repositories.Interfaces;

namespace ProductApi.Repositories;

internal class ProductRepository(DataContext context) : IProductRepository
{
    public async Task<bool> ProductExistsByTitle(string title)
    {
        return await context
            .Products
            .AnyAsync(p => p.Title.ToLower().Equals(title.ToLower()));
    }

    public async Task<bool> CreateProduct(Product product)
    {
        await context
            .Products
            .AddAsync(product);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteProduct(Product product)
    {
        context.Products
            .Remove(product);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        context.Products
            .Update(product);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await context.Products
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryId(Guid categoryId)
    {
        return await context
            .Products
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<Product?> GetProductById(Guid productId)
    {
        return await context
            .Products
            .SingleOrDefaultAsync(p => p.Id == productId);
    }

    public async Task<Product?> GetProductByTitle(string title)
    {
        return await context
            .Products
            .SingleOrDefaultAsync(p => p.Title.ToLower().Equals(title.ToLower()));
    }
}
