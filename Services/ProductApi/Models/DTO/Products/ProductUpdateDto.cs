using FluentValidation;

namespace ProductApi.Models.DTO.Products;

public sealed record ProductUpdateDto(
    
    Guid ProductId,
    string? Title,
    decimal? Price,
    string? Description,
    string? ImageUrl
);

public sealed class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateValidator()
    {
        RuleFor(p => p.ProductId)
            .NotEmpty();

        RuleFor(p => p.Title)
            .MaximumLength(150);
        
        RuleFor(p => p.Price)
            .GreaterThanOrEqualTo(1);

        RuleFor(p => p.Description)
            .MaximumLength(250);

        RuleFor(p => p.ImageUrl)
            .MaximumLength(250);
    }
}