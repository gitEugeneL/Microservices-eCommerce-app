namespace BasketApi.Entities;

public class BasketItem
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; } = string.Empty;
    public decimal ProductPrice { get; init; }
    public int Quantity { get; init; }
}