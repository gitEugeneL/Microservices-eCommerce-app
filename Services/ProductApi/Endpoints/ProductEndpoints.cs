using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Entities;
using ProductApi.Models.DTO.Products;
using ProductApi.Repositories.Interfaces;
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
            .Produces<ProductResponseDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict);
        
        group.MapGet("", GetProducts)
            .Produces<List<ProductResponseDto>>();
        
        group.MapGet("{productId:guid}", GetProductById)
            .Produces<ProductResponseDto>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPut("", UpdateProduct)
            .WithValidator<ProductUpdateDto>()
            .Produces<ProductResponseDto>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("{productId:guid}", DeleteProductById)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<Results<Created<ProductResponseDto>, Conflict<string>, NotFound<string>>> 
        CreateProduct(
            [FromBody] ProductRequestDto dto, 
            IProductRepository productRepository,
            ICategoryRepository categoryRepository)
    {
        if (await productRepository.ProductExistsByTitle(dto.Title))
            return TypedResults.Conflict($"Product: {dto.Title} already exists");

        var category = await categoryRepository.GetCategoryById(dto.CategoryId);

        if (category is null)
            return TypedResults.NotFound($"Category: {dto.CategoryId} doesn't exist");

        var product = new Product
        {
            Title = dto.Title,
            Description = dto.Description,
            ImageUrl = dto.ImageUrl,
            Price = dto.Price,
            Category = category
        };
        await productRepository.CreateProduct(product);
        
        return TypedResults.Created(product.Id.ToString(), new ProductResponseDto(product));
    }
    
    private static async Task<Results<Ok<ProductResponseDto>, NotFound<string>>> GetProductById(
        [FromRoute] Guid productId, 
        IProductRepository repository)
    {
        var product = await repository.GetProductById(productId);
        return product is not null
            ? TypedResults.Ok(new ProductResponseDto(product))
            : TypedResults.NotFound($"Product: {productId} not found");
    }
    
    private static async Task<IResult> GetProducts(IProductRepository repository)
    {
        // todo add pagination !!!!!
        var products = await repository.GetAllProducts();
        return TypedResults.Ok(
            products
                .Select(p => new ProductResponseDto(p))
        );
    }
    
    private static async Task<Results<Ok<ProductResponseDto>, BadRequest<string>, NotFound<string>>> UpdateProduct(
        [FromBody] ProductUpdateDto dto, 
        IProductRepository repository)
    {
        var product = await repository.GetProductById(dto.ProductId);
        if (product is null)
            return TypedResults.NotFound($"Product: {dto.ProductId} not found");
        
        product.Title = dto.Title ?? product.Title;
        product.Description = dto.Description ?? product.Description;
        product.Price = dto.Price ?? product.Price;
        product.ImageUrl = dto.ImageUrl ?? product.ImageUrl;

        await repository.UpdateProduct(product);
        return TypedResults.Ok(new ProductResponseDto(product));
    }

    private static async Task<Results<NoContent, BadRequest<string>, NotFound<string>>> DeleteProductById(
        [FromRoute] Guid productId, 
        IProductRepository repository)
    {
        var product = await repository.GetProductById(productId);
        if (product is null)
            return TypedResults.NotFound($"Product {productId} not found");
        await repository.DeleteProduct(product);
        return TypedResults.NoContent();
    }
}