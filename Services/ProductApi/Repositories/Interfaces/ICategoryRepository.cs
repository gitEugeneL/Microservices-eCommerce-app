using ProductApi.Data.Entities;
using ProductApi.Models.Entities;

namespace ProductApi.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategories();
    
    Task<Category?> GetCategoryById(Guid categoryId);
}
