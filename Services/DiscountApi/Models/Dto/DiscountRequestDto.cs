using FluentValidation;

namespace DiscountApi.Models.Dto;

public sealed record DiscountRequestDto(
    Guid ProductId,
    string Code,
    int Amount
);

public sealed class DiscountRequestValidator : AbstractValidator<DiscountRequestDto>
{
    public DiscountRequestValidator()
    {
        RuleFor(d => d.ProductId)
            .NotEmpty();
        
        RuleFor(d => d.Code)
            .NotEmpty();

        RuleFor(d => d.Amount)
            .GreaterThan(1)
            .LessThanOrEqualTo(95)
            .NotEmpty();
    }
}
