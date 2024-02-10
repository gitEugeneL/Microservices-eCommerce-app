using DiscountApi.Entities;
using DiscountApi.Models.Dto;
using DiscountApi.Repositories;
using DiscountApi.Utils;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DiscountApi.Endpoints;

public static class DiscountEndpoints
{
    public static void MapDiscountEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/discounts")
            .WithTags("Discounts");

        group.MapPost("", CreateDiscount)
            .WithValidator<DiscountRequestDto>()
            .Produces<DiscountResponseDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status409Conflict);
        
        group.MapDelete("{productId:guid}", DeleteDiscount)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
        
        group.MapGet("{productId:guid}", GetDiscountByProductId)
            .Produces<DiscountResponseDto>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapGet("{code}", GetDiscountByCode)
            .Produces<DiscountResponseDto>()
            .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<Results<Created<DiscountResponseDto>, Conflict<string>>> CreateDiscount(
        DiscountRequestDto dto,
        IDiscountRepository repository)
    {
        if (await repository.GetDiscountByCode(dto.Code) is not null)
            return TypedResults.Conflict($"Discount {dto.Code} already exist");
        
        var discount = new Discount
        {
            ProductId =dto.ProductId,
            Code = dto.Code,
            Amount = dto.Amount
        };
        await repository.CreateDiscount(discount);
        return TypedResults.Created(discount.Id.ToString(), new DiscountResponseDto(discount));
    }
    
    private static async Task<Results<Ok<DiscountResponseDto>, NotFound<string>>> GetDiscountByProductId(
        Guid productId, 
        IDiscountRepository repository)
    {
        var discount = await repository.GetDiscountByProductId(productId);
        return discount is not null
            ? TypedResults.Ok(new DiscountResponseDto(discount))
            : TypedResults.NotFound($"Discount for product: {productId} doesn't exist");
    }

    private static async Task<Results<Ok<DiscountResponseDto>, NotFound<string>>> GetDiscountByCode(
        string code,
        IDiscountRepository repository)
    {
        var discount = await repository.GetDiscountByCode(code);
        return discount is not null
            ? TypedResults.Ok(new DiscountResponseDto(discount))
            : TypedResults.NotFound($"Discount: {code} not found");
    }
    
    private static async Task<Results<NoContent, NotFound<string>>> DeleteDiscount(
        Guid productId, 
        IDiscountRepository repository)
    {
        var discount = await repository.GetDiscountByProductId(productId);
        if (discount is null)
            return TypedResults.NotFound($"discount for product: {productId} not found");
        await repository.DeleteDiscount(discount);
        return TypedResults.NoContent();
    }
}