using ProductApi.Data.Entities.Common;

namespace ProductApi.Models.Entities;

public sealed class Auction : BaseAuditableEntity
{
    public Guid SellerId { get; init; }
    public Guid? WinnerId { get; init; }
    public DateTime EndTime { get; init; }
    public decimal StartPrice { get; init; }
    public decimal? MaxBid { get; init; }
    public Status Status { get; init; }
    
    /*** Relations ***/
    public required Product Product { get; init; }
    
    public Guid CategoryId { get; init; }
    public required Category Category { get; init; }
}
