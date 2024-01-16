using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models.Entities;
using ProductApi.Repositories.Interfaces;

namespace ProductApi.Repositories;

internal class ProductRepository(DataContext context) : IProductRepository
{
    public async Task DeleteProduct(Product product)
    {
        context.Products.Remove(product);
        await context.SaveChangesAsync();
    }

    public async Task<Product> CreateProduct(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> GetProductById(Guid id)
    {
        return await context.Products
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await context.Products.ToListAsync();
    }
}
