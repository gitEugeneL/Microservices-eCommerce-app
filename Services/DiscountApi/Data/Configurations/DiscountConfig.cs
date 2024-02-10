using DiscountApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountApi.Data.Configurations;

internal class DiscountConfig : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.Property(d => d.ProductId)
            .IsRequired();
        
        builder.HasIndex(d => d.Code)
            .IsUnique();

        builder.Property(d => d.Code)
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(d => d.Amount)
            .IsRequired();
    }
}
