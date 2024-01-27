using DiscountApi.Data.Entities.Common;

namespace DiscountApi.Data.Entities;

public sealed class Discount : BaseAuditableEntity
{
    public string Code { get; init; } = string.Empty;
    public double Quantity { get; init; }
    public int MinValue { get; init; }
}