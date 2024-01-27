using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Exceptions;
using ProductApi.Models.DTO.Categories;
using ProductApi.Services.Interfaces;

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
            .Produces<CategoryResponseDto>();
    }

    private static async Task<IResult> GetCategories([FromServices] ICategoryService service)
    {
        return TypedResults.Ok(await service.GetAllCategories());
    }
    
    private static async Task<Results<Ok<CategoryResponseDto>, NotFound<string>>> GetCategoryById(
        [FromRoute] Guid categoryId, 
        [FromServices] ICategoryService service)
    {
        try
        {
            return TypedResults.Ok(await service.GetOneCategory(categoryId));
        }
        catch (NotFoundException e)
        {
            return TypedResults.NotFound(e.Message);
        }
    }
}
