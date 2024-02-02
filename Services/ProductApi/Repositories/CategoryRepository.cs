using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models.Entities;
using ProductApi.Repositories.Interfaces;

namespace ProductApi.Repositories;

internal class CategoryRepository(DataContext context) : ICategoryRepository
{
    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        return await context.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategoryById(Guid id)
    {
        return await context.Categories
            .SingleOrDefaultAsync(c => c.Id == id);
    }
}
