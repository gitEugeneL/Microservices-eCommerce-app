using DiscountApi.Entities;

namespace DiscountApi.Repositories;

public interface IDiscountRepository
{
    Task<Discount?> GetDiscountByProductId(Guid productId);

    Task<Discount?> GetDiscountByCode(string code);
    
    Task<bool> CreateDiscount(Discount discount);
    
    Task<bool> DeleteDiscount(Discount discount);
}
