using ProductApi.Entities.Common;
using ProductApi.Models.Entities;

namespace ProductApi.Entities;

public class Category : BaseEntity
{
    public required string Value { get; init; }
    
    /*** Relations ***/
    public List<Product> Products { get; init; } = [];
}
