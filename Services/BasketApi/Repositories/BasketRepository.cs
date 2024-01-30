using BasketApi.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace BasketApi.Repositories;

internal class BasketRepository(IDistributedCache cache) : IBasketRepository
{
    public async Task<Basket?> GetBasket(Guid userId)
    {
        var basket = await cache
            .GetStringAsync(userId.ToString());

        return basket is not null
            ? JsonConvert.DeserializeObject<Basket>(basket)
            : null;
    }

    public async Task<Basket?> UpdatedBasket(Basket basket)
    {
        await cache
            .SetStringAsync(
                key: basket.UserId.ToString(), 
                value: JsonConvert.SerializeObject(basket)
            );
        return await GetBasket(basket.UserId);
    }

    public async Task DeleteBasket(Basket basket)
    {
        await cache
            .RefreshAsync(basket.UserId.ToString());
    }
}