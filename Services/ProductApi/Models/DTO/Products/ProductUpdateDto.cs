using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models.DTO.Products;

public sealed record ProductUpdateDto(
    
    [Required]
    Guid ProductId,
    
    [MaxLength(150)]
    string? Title,
    
    [Required]
    [Range(10, double.MaxValue)]
    decimal? Price,
    
    [MaxLength(250)]
    string? Description,
    
    [MaxLength(250)]
    string? ImageUrl
);