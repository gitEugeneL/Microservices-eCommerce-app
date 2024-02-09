using SearchApi.Entities;

namespace SearchApi.Models;

public sealed record SearchResult(
    IReadOnlyList<Item> Results,
    long TotalCount, 
    int PageCount
);