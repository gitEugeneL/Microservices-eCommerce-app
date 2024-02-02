using FluentValidation;

namespace ProductApi.Models.DTO.Auctions;

public sealed record CreateAuctionDto(
    Guid CategoryId,
    string Title,
    string Description,
    decimal StartPrice,
    DateTime AuctionEnd
);

public sealed class CreateAuctionValidator : AbstractValidator<CreateAuctionDto>
{
    public CreateAuctionValidator()
    {
        RuleFor(a => a.CategoryId)
            .NotEmpty();

        RuleFor(a => a.Title)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(a => a.Description)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(a => a.StartPrice)
            .NotEmpty()
            .GreaterThanOrEqualTo(10);

        RuleFor(a => a.AuctionEnd)
            .NotEmpty()
            .Must(auctionEnd => auctionEnd > DateTime.Now.AddMinutes(5))
            .WithMessage("Auction end date must be greater than the current date");
    }
}

