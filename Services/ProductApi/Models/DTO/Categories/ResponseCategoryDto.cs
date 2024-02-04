using ProductApi.Models.Entities;

namespace ProductApi.Models.DTO.Categories;

public sealed record ResponseCategoryDto
{
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;

    public ResponseCategoryDto(Category category)
    {
        CategoryId = category.Id;
        CategoryName = category.Value;
    }
} 