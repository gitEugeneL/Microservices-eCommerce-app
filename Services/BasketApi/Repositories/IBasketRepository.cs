using BasketApi.Entities;

namespace BasketApi.Repositories;

public interface IBasketRepository
{
    Task<Basket?> GetBasket(Guid userId);
    
    Task<Basket?> UpdatedBasket(Basket basket);

    Task DeleteBasket(Basket basket);
}