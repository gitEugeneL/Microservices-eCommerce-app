using BasketApi.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace BasketApi.Repositories;

internal class BasketRepository(IDistributedCache cache) : IBasketRepository
{
    public async Task<Basket?> GetBasket(string userKey)
    {
        var basket = await cache
            .GetStringAsync(userKey);

        return basket is not null
            ? JsonConvert.DeserializeObject<Basket>(basket)
            : null;
    }

    public async Task<Basket?> UpdatedBasket(Basket basket)
    {
        await cache
            .SetStringAsync(
                key: basket.UserKey, 
                value: JsonConvert.SerializeObject(basket)
            );
        return await GetBasket(basket.UserKey);
    }

    public async Task DeleteBasket(string userKey)
    {
        await cache.RefreshAsync(userKey);
    }
}