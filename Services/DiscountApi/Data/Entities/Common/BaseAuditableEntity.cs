namespace DiscountApi.Data.Entities.Common;

public abstract class BaseAuditableEntity
{
    public Guid Id { get; set; }
    public DateTime Created { get; init; }
    public DateTime? LastModified { get; set; }
}
