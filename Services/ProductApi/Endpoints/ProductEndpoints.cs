using ProductApi.Models.DTO.Products;
using ProductApi.Services.Interfaces;

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

    private static async Task<IResult> CreateProduct(ProductRequestDto dto, IProductService service)
    {
        var result = await service.CreateProduct(dto);
        return Results.Created(result.ProductId.ToString(), result);
    }

    private static async Task<IResult> GetProducts(IProductService service)
    {
        var result = await service.GetAllProducts();
        return Results.Ok(result);
    }

    private static async Task<IResult> GetProductById(Guid productId, IProductService service)
    {
        var result = await service.GetOneProduct(productId);
        return Results.Ok(result);
    }

    private static async Task<IResult> UpdateProduct(ProductUpdateDto dto, IProductService service)
    {
        var result = await service.UpdateProduct(dto);
        return Results.Ok(result);
    }

    private static async Task<IResult> DeleteProductById(Guid productId, IProductService service)
    {
        await service.DeleteProduct(productId);
        return Results.NoContent();
    }
}