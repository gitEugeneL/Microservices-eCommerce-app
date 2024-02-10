using System.Reflection;
using DiscountApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscountApi.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public required DbSet<Discount> Discounts { get; init; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
    
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken token = default)
    {
        foreach (var entity in ChangeTracker
                     .Entries()
                     .Where(x => x is { Entity: Discount, State: EntityState.Modified })
                     .Select(x => x.Entity)
                     .Cast<Discount>())
        {
            entity.LastModified = DateTime.UtcNow;
        }
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, token);
    }
}
