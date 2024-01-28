using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductApi.Data.Entities;

namespace ProductApi.Data.Configurations;

internal class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.Description)
            .HasMaxLength(250);

        builder.Property(p => p.ImageUrl);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(20, 2)");
        
        builder.Property(p => p.Created)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
