using BasketApi.Entities;

namespace BasketApi.Repositories;

public interface IBasketRepository
{
    Task<Basket?> GetBasket(string userKey);
    
    Task<Basket?> UpdatedBasket(Basket basket);

    Task DeleteBasket(string userKey);
}