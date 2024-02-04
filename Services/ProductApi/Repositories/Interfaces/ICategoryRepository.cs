using ProductApi.Models.Entities;

namespace ProductApi.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<bool> CategoryExists(Guid categoryId);
    
    Task<IEnumerable<Category>> GetAllCategories();
    
    Task<Category?> GetCategoryById(Guid categoryId);
}
