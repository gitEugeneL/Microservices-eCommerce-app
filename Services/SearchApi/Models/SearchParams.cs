namespace SearchApi.Models;

public sealed record SearchParams(
    string? Param,
    int PageNumber = 1,
    int PageSize = 10
);