using ProductApi.Entities;
using ProductApi.Exceptions;
using ProductApi.Models.DTO.Categories;
using ProductApi.Repositories.Interfaces;
using ProductApi.Services.Interfaces;

namespace ProductApi.Services;

internal class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<CategoryResponseDto> GetOneCategory(Guid id)
    {
        var category = await categoryRepository.GetCategoryById(id)
                       ?? throw new NotFoundException(nameof(Category), id);

        return new CategoryResponseDto()
            .ToCategoryResponseDto(category);
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllCategories()
    {
        var categories = await categoryRepository.GetAllCategories();
        
        return categories
            .Select(c => new CategoryResponseDto().ToCategoryResponseDto(c));
    }
}
