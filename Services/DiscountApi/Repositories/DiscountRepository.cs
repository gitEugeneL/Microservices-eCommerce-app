using DiscountApi.Data;
using DiscountApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscountApi.Repositories;

internal class DiscountRepository(DataContext context) : IDiscountRepository
{
    public async Task<Discount?> GetDiscountByProductId(Guid productId)
    {
        return await context
            .Discounts
            .SingleOrDefaultAsync(d => d.ProductId == productId);
    }

    public async Task<Discount?> GetDiscountByCode(string code)
    {
        return await context
            .Discounts
            .SingleOrDefaultAsync(d => d.Code.ToLower().Equals(code.ToLower()));
    }

    public async Task<bool> CreateDiscount(Discount discount)
    {
        await context
            .Discounts
            .AddAsync(discount);
        return await context.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> DeleteDiscount(Discount discount)
    {
        context
            .Discounts
            .Remove(discount);
        return await context.SaveChangesAsync() > 0; 
    }
}