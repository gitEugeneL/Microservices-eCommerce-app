using ProductApi.Models.Entities;

namespace ProductApi.Models.DTO.Auctions;

public sealed record ResponseAuctionDto
{
    public Guid AuctionId { get; set; }
    public Guid CategoryId { get; set; }
    public string ProductTitle { get; set; }
    public string ProductDescription { get; set; }
    public string? ImageName { get; set; }
    public Guid SellerId { get; set; }
    public Guid? WinnerId { get; set; }
    public decimal StartPrice { get; set; }
    public decimal? MaxBid { get; set; }
    public string Status { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime CreatedTime { get; set; }

    public ResponseAuctionDto(Auction auction)
    {
        AuctionId = auction.Id;
        CategoryId = auction.CategoryId;
        ProductTitle = auction.Product.Title;
        ProductDescription = auction.Product.Description;
        ImageName = auction.Product.ImageName;
        SellerId = auction.SellerId;
        WinnerId = auction.WinnerId;
        StartPrice = auction.StartPrice;
        MaxBid = auction.MaxBid;
        Status = auction.Status.ToString();
        EndTime = auction.EndTime;
        CreatedTime = auction.Created;
    }
}