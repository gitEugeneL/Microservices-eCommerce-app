using ProductApi.Models.Entities.Common;

namespace ProductApi.Models.Entities;

public sealed class Product : BaseEntity
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public string? ImageName { get; init; }
}