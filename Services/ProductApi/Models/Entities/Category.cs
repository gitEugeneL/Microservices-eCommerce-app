using ProductApi.Models.Entities.Common;

namespace ProductApi.Models.Entities;

public sealed class Category : BaseEntity
{
    public required string Value { get; init; }
    
    /*** Relations ***/
    public List<Auction> Auctions { get; init; } = [];
}
