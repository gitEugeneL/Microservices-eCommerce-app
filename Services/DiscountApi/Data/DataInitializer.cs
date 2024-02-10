namespace DiscountApi.Data;

public static class DataInitializer
{
    public static void Init(DataContext context)
    {
        // dotnet ef database update
        context.Database.EnsureCreated();
    }
}
