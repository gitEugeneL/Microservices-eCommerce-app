using Bogus;
using ProductApi.Entities;

namespace ProductApi.Data;

public static class DataGenerator
{
    public static void SeedData(DataContext context)
    {
        var categories = new List<Category>
        {
            new() { Value = "Room" },
            new() { Value = "Apartment" },
            new() { Value = "House" },
            new() { Value = "Commercial space" },
        };

        var products = new Faker<Product>()
            .RuleFor(p => p.Title, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Price, f => Math.Round(f.Random.Decimal(10000, 500000), 2))
            .RuleFor(p => p.Category, f => f.Random.ListItem(categories))
            .Generate(20);

        context.AddRangeAsync(products);
        context.SaveChanges();
    }
}