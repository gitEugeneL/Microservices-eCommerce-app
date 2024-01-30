using ProductApi.Models.DTO.Categories;

namespace ProductApi.Services.Interfaces;

public interface ICategoryService
{
    Task<CategoryResponseDto> GetOneCategory(Guid id);
    
    Task<IEnumerable<CategoryResponseDto>> GetAllCategories();
}