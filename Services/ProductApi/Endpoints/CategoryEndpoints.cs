using Microsoft.AspNetCore.Http.HttpResults;
using ProductApi.Models.DTO.Categories;
using ProductApi.Repositories.Interfaces;

namespace ProductApi.Endpoints;

public static class CategoryEndpoint
{
    public static void MapCategoryEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/category")
            .WithTags("Category");
        
        group.MapGet("", GetCategories)
            .Produces<List<ResponseCategoryDto>>();

        group.MapGet("{categoryId:guid}", GetCategoryById)
            .Produces<ResponseCategoryDto>();
    }

    private static async Task<IResult> GetCategories(ICategoryRepository repository)
    {
        var categories = await repository.GetAllCategories();
        return TypedResults.Ok(categories.Select(c => new ResponseCategoryDto(c)));
    }
    
    private static async Task<Results<Ok<ResponseCategoryDto>, NotFound<string>>> GetCategoryById(
        Guid categoryId, 
        ICategoryRepository repository)
    {
        var category = await repository.GetCategoryById(categoryId);
        return category is not null 
            ? TypedResults.Ok(new ResponseCategoryDto(category)) 
            : TypedResults.NotFound($"Category: {categoryId} not found");
    }
}
