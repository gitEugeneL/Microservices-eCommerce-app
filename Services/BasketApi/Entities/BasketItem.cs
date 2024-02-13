namespace BasketApi.Entities;

public class BasketItem
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; } = string.Empty;
    public decimal Price { get; set; }
    public uint Quantity { get; init; }
}