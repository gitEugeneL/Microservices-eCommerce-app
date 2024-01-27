using ProductApi.Entities;
using ProductApi.Exceptions;
using ProductApi.Models.DTO.Categories;
using ProductApi.Repositories.Interfaces;
using ProductApi.Services.Interfaces;

namespace ProductApi.Services;

internal class CategoryService(ICategoryRepository repository) : ICategoryService
{
    public async Task<CategoryResponseDto> GetOneCategory(Guid categoryId)
    {
        var category = await repository.GetCategoryById(categoryId)
                       ?? throw new NotFoundException(nameof(Category), categoryId);
        return new CategoryResponseDto(category);
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllCategories()
    {
        var categories = await repository.GetAllCategories();
        return categories
            .Select(c => new CategoryResponseDto(c));
    }
}