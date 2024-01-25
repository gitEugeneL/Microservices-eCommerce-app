using FluentValidation;

namespace ProductApi.Models.DTO.Products;

public sealed record ProductRequestDto(
     Guid CategoryId,
     string Title,
     decimal Price,
     string? Description,
     string? ImageUrl
);

public sealed class ProductRequestValidator : AbstractValidator<ProductRequestDto>
{
     public ProductRequestValidator()
     {
          RuleFor(p => p.CategoryId)
               .NotEmpty();

          RuleFor(p => p.Title)
               .NotEmpty()
               .MaximumLength(150);

          RuleFor(p => p.Price)
               .NotEmpty()
               .GreaterThanOrEqualTo(1);

          RuleFor(p => p.Description)
               .MaximumLength(250);

          RuleFor(p => p.ImageUrl)
               .MaximumLength(250);
     }
}

