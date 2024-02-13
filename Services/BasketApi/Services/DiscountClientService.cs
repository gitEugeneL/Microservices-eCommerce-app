using DiscountGrpc;

namespace BasketApi.Services;

public class DiscountClientService(DiscountProto.DiscountProtoClient client) : IDiscountClientService
{
    public async Task<DiscountResponse> GetDiscountByProductId(Guid productId)
    {
        var discountRequest = new GetDiscountByProductIdRequest { ProductId = productId.ToString() };
        return await client.GetDiscountByProductIdAsync(discountRequest);
    }
}