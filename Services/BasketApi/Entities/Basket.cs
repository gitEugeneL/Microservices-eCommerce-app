namespace BasketApi.Entities;

public class Basket
{
    public string UserKey { get; init; } = string.Empty;
    public List<BasketItem> Items { get; init; } = [];
}