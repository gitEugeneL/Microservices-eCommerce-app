using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models.DTO.Products;

public sealed record ProductRequestDto(

     [Required] 
     Guid CategoryId,
     
     [Required] 
     [MaxLength(150)] 
     string Title,
     
     [Required]
     [Range(10, double.MaxValue)]
     decimal Price,
     
     [MaxLength(250)] 
     string? Description,
     
     [MaxLength(250)] 
     string? ImageUrl
);