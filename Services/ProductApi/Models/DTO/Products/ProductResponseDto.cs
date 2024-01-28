using ProductApi.Data.Entities;

namespace ProductApi.Models.DTO.Products;

public sealed record ProductResponseDto
{
    public Guid ProductId { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public Guid CategoryId { get; set; }

    public ProductResponseDto(Product product)
    {
        ProductId = product.Id;
        Title = product.Title;
        Price = product.Price;
        Description = product.Description;
        ImageUrl = product.ImageUrl;
        CategoryId = product.CategoryId;
    }
}
