using ProductApi.Models.Entities;

namespace ProductApi.Data;

public static class DataGenerator
{
    public static void SeedData(DataContext context)
    {
        var categories = new List<Category>
        {
            new() { Value = "Category 1" },
            new() { Value = "Category 2" },
            new() { Value = "Category 3" },
            new() { Value = "Category 4" },
        };
        
        context.AddRangeAsync(categories);
        context.SaveChanges();
    }
}