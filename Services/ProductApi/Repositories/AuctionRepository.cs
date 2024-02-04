using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models.Entities;
using ProductApi.Repositories.Interfaces;

namespace ProductApi.Repositories;

internal class AuctionRepository(DataContext context) : IAuctionRepository
{
    public async Task<bool> CreateAuction(Auction auction)
    {
        await context.AddAsync(auction);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Auction>> GetAllActions()
    {
        return await context
            .Auctions
            .Include(a => a.Product)
            .ToListAsync();
    }

    public async Task<Auction?> GetAuctionById(Guid auctionId)
    {
        return await context
            .Auctions
            .Include(a => a.Product)
            .SingleOrDefaultAsync(a => a.Id == auctionId);
    }

    public async Task<bool> DeleteAuction(Auction auction)
    {
        context.Remove(auction);
        return await context.SaveChangesAsync() > 0;
    }
}