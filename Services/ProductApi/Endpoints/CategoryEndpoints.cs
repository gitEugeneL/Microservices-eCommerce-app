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

    private static async Task<IResult> GetCategories(ICategoryService service)
    {
        var result = await service.GetAllCategories();
        return Results.Ok(result);
    }

    private static async Task<IResult> GetCategoryById(Guid categoryId, ICategoryService service)
    {
        var result = await service.GetOneCategory(categoryId);
        return Results.Ok(result);
    }
}