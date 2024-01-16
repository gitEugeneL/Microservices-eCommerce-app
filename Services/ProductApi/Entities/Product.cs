using ProductApi.Entities.Common;

namespace ProductApi.Entities;

public sealed class Product : BaseAuditableEntity
{
    public required string Title { get; init; }
    public string Description { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
    public decimal Price { get; init; }

    /*** Relations ***/
    public Guid CategoryId { get; init; }
    public required Category Category { get; init; }
}
