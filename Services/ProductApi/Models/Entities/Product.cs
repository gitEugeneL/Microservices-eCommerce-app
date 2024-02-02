using ProductApi.Data.Entities.Common;

namespace ProductApi.Models.Entities;

public sealed class Product : BaseEntity
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public string? ImageName { get; init; }
    
    /*** Relations ***/
    public Guid AuctionId { get; init; }
    public required Auction Auction { get; init; }
}