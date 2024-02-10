namespace DiscountApi.Entities;

public sealed class Discount
{
    public Guid Id { get; init; }
    public required Guid ProductId { get; init; }
    public required string Code { get; init; }
    public required int Amount { get; init; }
    public DateTime Created { get; init; } = DateTime.UtcNow;
    public DateTime? LastModified { get; set; }
}