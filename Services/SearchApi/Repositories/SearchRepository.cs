using MongoDB.Entities;
using SearchApi.Entities;
using SearchApi.Models;

namespace SearchApi.Repositories;

internal class SearchRepository : ISearchRepository
{
    public async Task<SearchResult> FindWithPagination(SearchParams searchParams)
    {
        var query = DB.PagedSearch<Item>()
            .Sort(e => e.Ascending(i => i.CreatedTime));

        if (searchParams.Param is not null)
            query
                .Match(Search.Full, searchParams.Param)
                .SortByTextScore();
        
        query
            .PageSize(searchParams.PageSize)
            .PageNumber(searchParams.PageNumber);

        var result = await query.ExecuteAsync();
        return new SearchResult(result.Results, result.TotalCount, result.PageCount);
    }
}