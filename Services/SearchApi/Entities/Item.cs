using MongoDB.Entities;

namespace SearchApi.Entities;

public sealed class Item : Entity
{
    public required string AuctionId { get; set; }
    public required string CategoryId { get; set; }
    public required string ProductTitle { get; set; }
    public required string ProductDescription { get; set; }
    public string? ImageName { get; set; }
    public required string SellerId { get; set; }
    public string? WinnerId { get; set; }
    public decimal StartPrice { get; set; }
    public decimal? MaxBid { get; set; }
    public required string Status { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime CreatedTime { get; set; }
}