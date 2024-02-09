using SearchApi.Models;

namespace SearchApi.Repositories;

public interface ISearchRepository
{
    Task<SearchResult> FindWithPagination(SearchParams searchParams);
}