using ProductApi.Entities;
using ProductApi.Entities.Common;

namespace ProductApi.Models.Entities;

public sealed class Product : BaseAuditableEntity
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }

    /*** Relations ***/
    public Guid CategoryId { get; init; }
    public required Category Category { get; init; }
}
