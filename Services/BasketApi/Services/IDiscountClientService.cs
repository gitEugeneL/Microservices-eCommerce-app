using DiscountGrpc;

namespace BasketApi.Services;

public interface IDiscountClientService
{
    Task<DiscountResponse> GetDiscountByProductId(Guid productId);
}