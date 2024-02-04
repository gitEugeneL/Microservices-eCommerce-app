using ProductApi.Models.Entities;

namespace ProductApi.Repositories.Interfaces;

public interface IAuctionRepository
{
    Task<bool> CreateAuction(Auction auction);
    
    Task<IEnumerable<Auction>> GetAllActions();
    
    Task<Auction?> GetAuctionById(Guid auctionId);

    Task<bool> DeleteAuction(Auction auction);
}