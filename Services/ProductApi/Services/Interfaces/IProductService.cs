using ProductApi.Models.DTO.Products;

namespace ProductApi.Services.Interfaces;

public interface IProductService
{
    Task<ProductResponseDto> CreateProduct(ProductRequestDto dto);

    Task<ProductResponseDto> GetOneProduct(Guid id);

    Task<IEnumerable<ProductResponseDto>> GetAllProducts();

    Task<ProductResponseDto> UpdateProduct(ProductUpdateDto dto);

    Task DeleteProduct(Guid id);
}