using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data.Entities;
using ProductApi.Data.Entities.Common;
using ProductApi.Models.Entities;

namespace ProductApi.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public required DbSet<Auction> Auctions { get; init; }
    public required DbSet<Product> Products { get; init; }
    public required DbSet<Category> Categories { get; init; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
    
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken token = default)
    {
        foreach (var entity in ChangeTracker
                     .Entries()
                     .Where(x => x is { Entity: BaseAuditableEntity, State: EntityState.Modified })
                     .Select(x => x.Entity)
                     .Cast<BaseAuditableEntity>())
        {
            entity.LastModified = DateTime.UtcNow;
        }
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, token);
    }
}
