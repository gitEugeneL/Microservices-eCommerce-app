using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Exceptions;
using ProductApi.Models.DTO.Products;
using ProductApi.Services.Interfaces;
using ProductApi.Utils;

namespace ProductApi.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/products")
            .WithTags("Products");

        group.MapPost("", CreateProduct)
            .WithValidator<ProductRequestDto>()
            .Produces<ProductResponseDto>(StatusCodes.Status201Created);

        group.MapGet("", GetProducts)
            .Produces<List<ProductResponseDto>>();

        group.MapGet("{productId:guid}", GetProductById)
            .Produces<ProductResponseDto>();

        group.MapPut("", UpdateProduct)
            .WithValidator<ProductUpdateDto>()
            .Produces<ProductResponseDto>();

        group.MapDelete("{productId:guid}", DeleteProductById)
            .Produces(StatusCodes.Status204NoContent);
    }

    private static async Task<Results<Created<ProductResponseDto>, Conflict<string>, BadRequest<string>, NotFound<string>>> 
        CreateProduct(
            [FromBody] ProductRequestDto dto, 
            [FromServices] IProductService service)
    {
        try
        {
            var result = await service.CreateProduct(dto);
            return TypedResults.Created(result.ProductId.ToString(), result);
        }
        catch (AlreadyExistException e)
        {
            return TypedResults.Conflict(e.Message);
        }
        catch (NotFoundException e)
        {
            return TypedResults.NotFound(e.Message);
        }
        catch (DbException e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }
    
    private static async Task<Results<Ok<ProductResponseDto>, NotFound<string>>> GetProductById(
        [FromRoute] Guid productId, 
        [FromServices] IProductService service)
    {
        try
        {
            return TypedResults.Ok(await service.GetOneProduct(productId));
        }
        catch (Exception e)
        {
            return TypedResults.NotFound(e.Message);
        }
    }
    
    private static async Task<IResult> GetProducts([FromServices] IProductService service)
    {
        return TypedResults.Ok(await service.GetAllProducts());
    }
    
    private static async Task<Results<Ok<ProductResponseDto>, BadRequest<string>, NotFound<string>>> UpdateProduct(
        [FromBody] ProductUpdateDto dto, 
        [FromServices] IProductService service)
    {
        try
        {
            return TypedResults.Ok(await service.UpdateProduct(dto));
        }
        catch (NotFoundException e)
        {
            return TypedResults.NotFound(e.Message);
        }
        catch (DbException e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    private static async Task<Results<NoContent, BadRequest<string>, NotFound<string>>> DeleteProductById(
        [FromRoute] Guid productId, 
        [FromServices] IProductService service)
    {
        try
        {
            await service.DeleteProduct(productId);
            return TypedResults.NoContent();
        }
        catch (NotFoundException e)
        {
            return TypedResults.NotFound(e.Message);
        }
        catch (DbException e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }
}