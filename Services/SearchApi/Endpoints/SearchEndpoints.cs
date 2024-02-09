using SearchApi.Entities;
using SearchApi.Models;
using SearchApi.Repositories;

namespace SearchApi.Endpoints;

public static class SearchEndpoints
{
    public static void MapSearchEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/search")
            .WithTags("Search");

        group.MapGet("", GetItems)
            .Produces<List<Item>>();
    }

    private static async Task<IResult> GetItems(
        [AsParameters] SearchParams searchParams, 
        ISearchRepository repository)
    {
        return TypedResults.Ok(
            await repository.FindWithPagination(searchParams)
        );
    }
}