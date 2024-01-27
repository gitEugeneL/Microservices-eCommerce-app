using DiscountApi.Models.Entities;

namespace DiscountApi.Data;

public static class DataGenerator
{
    public static void SeedData(DataContext context)
    {
        var discount = new Discount
        {
            Code = "CLP50DISCOUNT",
            Quantity = 50,
            MinValue = 1,
            Created = DateTime.UtcNow
        };
        
        context.AddRange(discount);
        context.SaveChanges();
    }
}