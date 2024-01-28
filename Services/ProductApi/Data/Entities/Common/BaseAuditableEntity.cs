namespace ProductApi.Data.Entities.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime Created { get; init; }
    public DateTime? LastModified { get; set; }
}
