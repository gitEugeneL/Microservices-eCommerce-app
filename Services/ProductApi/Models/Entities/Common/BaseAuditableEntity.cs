namespace ProductApi.Models.Entities.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime Created { get; init; } = DateTime.UtcNow;
    public DateTime? LastModified { get; set; }
}
