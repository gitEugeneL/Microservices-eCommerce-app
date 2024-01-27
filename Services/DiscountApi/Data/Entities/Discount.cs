using DiscountApi.Data.Entities.Common;

namespace DiscountApi.Models.Entities;

public sealed class Discount : BaseAuditableEntity
{
    public string Code { get; set; } = string.Empty;
    public double Quantity { get; set; }
    public int MinValue { get; set; }
}