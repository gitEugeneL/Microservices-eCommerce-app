using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models.DTO.Categories;
using ProductApi.Repositories.Interfaces;

namespace ProductApi.Endpoints;

public static class CategoryEndpoint
{
    public static void MapCategoryEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/categories")
            .WithTags("Categories");
        
        group.MapGet("", GetCategories)
            .Produces<List<CategoryResponseDto>>();

        group.MapGet("{categoryId:guid}", GetCategoryById)
            .Produces<CategoryResponseDto>()
            .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> GetCategories(ICategoryRepository repository)
    {
        var categories = await repository.GetAllCategories();
        return TypedResults.Ok(
            categories
                .Select(c => new CategoryResponseDto(c))
        );
    }
    
    private static async Task<Results<Ok<CategoryResponseDto>, NotFound<string>>> GetCategoryById(
        [FromRoute] Guid categoryId, 
        ICategoryRepository repository)
    {
        var category = await repository.GetCategoryById(categoryId);
        return category is not null
            ? TypedResults.Ok(new CategoryResponseDto(category))
            : TypedResults.NotFound($"Category {categoryId} not found");
    }
}
