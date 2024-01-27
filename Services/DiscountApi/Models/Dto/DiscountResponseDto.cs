using DiscountApi.Data.Entities;

namespace DiscountApi.Models.Dto;

public sealed record DiscountResponseDto
{
    public Guid DiscountId { get; set; }
    public string Code { get; set; }
    public double Quantity { get; set; }
    public int MinValue { get; set; }
    public string? CreatedAt { get; set; }

    public DiscountResponseDto(Discount discount)
    {
        DiscountId = discount.Id;
        Code = discount.Code;
        Quantity = discount.Quantity;
        MinValue = discount.MinValue;
        CreatedAt = discount.Created.ToString("yyyy MMMM dd");
    }
}
