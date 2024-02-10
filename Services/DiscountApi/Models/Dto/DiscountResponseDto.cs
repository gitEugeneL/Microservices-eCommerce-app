using DiscountApi.Entities;

namespace DiscountApi.Models.Dto;

public sealed record DiscountResponseDto
{
    public Guid DiscountId { get; set; }
    public Guid ProductId { get; set; }
    public string Code { get; set; }
    public int Amount { get; set; }
    public string CreatedAt { get; set; }

    public DiscountResponseDto(Discount discount)
    {
        DiscountId = discount.Id;
        ProductId = discount.ProductId;
        Code = discount.Code;
        Amount = discount.Amount;
        CreatedAt = discount.Created.ToString("yyyy MMMM dd");
    }
}
