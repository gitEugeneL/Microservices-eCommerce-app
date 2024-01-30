namespace BasketApi.Entities;

public class Basket
{
    public Guid UserId { get; init; }
    public List<BasketItem> Items { get; init; } = [];
    public decimal TotalPrice { get; init; }
}