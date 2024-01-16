using ProductApi.Entities;

namespace ProductApi.Models.DTO.Categories;

public sealed record CategoryResponseDto
{
    public Guid CategoryId { get; set; }
    public string Value { get; set; } = string.Empty;

    public CategoryResponseDto ToCategoryResponseDto(Category category)
    {
        CategoryId = category.Id;
        Value = category.Value;
        return this;
    }
}
