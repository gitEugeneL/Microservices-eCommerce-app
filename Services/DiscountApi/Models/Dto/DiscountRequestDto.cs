using FluentValidation;

namespace DiscountApi.Models.Dto;

public sealed record DiscountRequestDto(
    string Code,
    double Quantity,
    int MinValue    
);

public sealed class DiscountRequestValidator : AbstractValidator<DiscountRequestDto>
{
    public DiscountRequestValidator()
    {
        RuleFor(p => p.Code)
            .NotEmpty();

        RuleFor(p => p.Quantity)
            .GreaterThan(5)
            .LessThanOrEqualTo(95)
            .NotEmpty();

        RuleFor(p => p.MinValue)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
    }
}
