using ProductApi.Data.Entities.Common;

namespace ProductApi.Data.Entities;

public class Category : BaseEntity
{
    public required string Value { get; init; }
    
    /*** Relations ***/
    public List<Product> Products { get; init; } = [];
}
