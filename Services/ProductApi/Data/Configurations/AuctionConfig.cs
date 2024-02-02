using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductApi.Models.Entities;

namespace ProductApi.Data.Configurations;

public class AuctionConfig : IEntityTypeConfiguration<Auction>
{
    public void Configure(EntityTypeBuilder<Auction> builder)
    {
        builder.Property(a => a.SellerId)
            .IsRequired();
        
        builder.Property(a => a.StartPrice)
            .IsRequired()
            .HasColumnType("decimal(20, 2)");
        
        builder.Property(a => a.MaxBid)
            .HasColumnType("decimal(20, 2)");
    }
}