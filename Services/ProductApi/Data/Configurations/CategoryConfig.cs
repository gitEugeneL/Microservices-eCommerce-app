using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductApi.Models.Entities;

namespace ProductApi.Data.Configurations;

internal class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasIndex(c => c.Value)
            .IsUnique();

        builder.Property(c => c.Value)
            .IsRequired()
            .HasMaxLength(100);
        
        /*** many to one ***/
        builder.HasMany(c => c.Auctions)
            .WithOne(a => a.Category)
            .HasForeignKey(a => a.CategoryId);
    }
}
