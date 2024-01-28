using ProductApi.Data.Entities;

namespace ProductApi.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategories();
    Task<Category?> GetCategoryById(Guid id);
}
