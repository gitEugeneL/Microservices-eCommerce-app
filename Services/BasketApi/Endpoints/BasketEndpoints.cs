using BasketApi.Entities;
using BasketApi.Repositories;

namespace BasketApi.Endpoints;

public static class BasketEndpoints
{
    public static void MapBasketEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/basket")
            .WithTags("Basket");

        group.MapGet("{userKey}", GetBasket)
            .Produces<Basket>();

        group.MapPut("", UpdateBasket)
            .Produces<Basket>();

        group.MapDelete("{userKey}", DeleteBasket)
            .Produces(StatusCodes.Status204NoContent);
    }

    private static async Task<IResult> GetBasket(string userKey, IBasketRepository repository)
    {
        var basket = await repository.GetBasket(userKey);
        return basket is not null
            ? TypedResults.Ok(basket)
            : TypedResults.Ok(new Basket { UserKey = userKey });
    }

    private static async Task<IResult> UpdateBasket(Basket basket, IBasketRepository repository)
    {
        return TypedResults
            .Ok(await repository.UpdatedBasket(basket));
    }

    private static async Task<IResult> DeleteBasket(string userKey, IBasketRepository repository)
    {
        await repository.DeleteBasket(userKey);
        return TypedResults.NoContent();
    }
}