using DiscountApi.Data;
using DiscountApi.Data.Entities;
using DiscountApi.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProductApi.Utils;

namespace DiscountApi.Endpoints;

public static class DiscountEndpoints
{
    public static void MapDiscountEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/discounts")
            .WithTags("Discounts");

        group.MapPost("", CreateDiscount)
            .WithValidator<DiscountRequestDto>()
            .Produces<DiscountResponseDto>(StatusCodes.Status201Created);
        
        group.MapGet("", GetDiscounts)
            .Produces<List<DiscountRequestDto>>();

        group.MapGet("{discountId:guid}", GetOneDiscount)
            .Produces<DiscountResponseDto>();

        group.MapDelete("{discountId:guid}", DeleteDiscount)
            .Produces(StatusCodes.Status204NoContent);
    }

    private static async Task<Results<Created<DiscountResponseDto>, Conflict<string>>> CreateDiscount(
        DiscountRequestDto dto,
        DataContext context
    )
    {
        if (await context.Discounts.AnyAsync(d => d.Code == dto.Code))
            return TypedResults.Conflict($"Discount {dto.Code} already exist");
        
        var discount = new Discount { Code = dto.Code, Quantity = dto.Quantity, MinValue = dto.MinValue };
        await context.Discounts.AddAsync(discount);
        await context.SaveChangesAsync();
        return TypedResults.Created(discount.Id.ToString(), new DiscountResponseDto(discount));
    }
    
    private static async Task<IResult> GetDiscounts(DataContext context)
    {
        var discounts = await context.Discounts.ToListAsync();
        var result = discounts
            .Select(d => new DiscountResponseDto(d));
        return TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<DiscountResponseDto>, NotFound<string>>> GetOneDiscount(
        Guid discountId, 
        DataContext context)
    {
        var discount = await context.Discounts.SingleOrDefaultAsync(d => d.Id == discountId);
        if (discount is null)
            return TypedResults.NotFound($"discount: {discountId} not found");
        return TypedResults.Ok(new DiscountResponseDto(discount));
    }

    private static async Task<Results<NoContent, NotFound<string>>> DeleteDiscount(
        Guid discountId, 
        DataContext context)
    {
        var discount = await context.Discounts.SingleOrDefaultAsync(d => d.Id == discountId);
        if (discount is null)
            return TypedResults.NotFound($"discount: {discountId} not found");
        context.Discounts.Remove(discount);
        await context.SaveChangesAsync();
        return TypedResults.NoContent();
    }
}