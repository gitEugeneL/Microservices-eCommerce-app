namespace ProductApi.Data;

public static class DataInitializer
{
    public static void Init(DataContext context)
    {
        // dotnet ef database update
        context.Database.EnsureCreated(); 
        
        if(!context.Products.Any() && !context.Categories.Any())
            DataGenerator.SeedData(context); // seed data
    }
}
