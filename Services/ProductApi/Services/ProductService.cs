using ProductApi.Entities;
using ProductApi.Exceptions;
using ProductApi.Models.DTO.Products;
using ProductApi.Models.Entities;
using ProductApi.Repositories.Interfaces;
using ProductApi.Services.Interfaces;

namespace ProductApi.Services;

internal class ProductService(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository
) : IProductService
{
    private async Task<Product> FindProductOrThrow(Guid id)
    {
        return await productRepository.GetProductById(id)
                      ?? throw new NotFoundException(nameof(Product), id);
    }

    private async Task<Category> FindCategoryOrThrow(Guid id)
    {
        return await categoryRepository.GetCategoryById(id)
                       ?? throw new NotFoundException(nameof(Category), id);
    }
    
    public async Task<ProductResponseDto> CreateProduct(ProductRequestDto dto)
    {
        var product = await productRepository.CreateProduct(
            new Product
            {
                Title = dto.Title,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                Price = dto.Price,
                Category = await FindCategoryOrThrow(dto.CategoryId)
            });
        
        return new ProductResponseDto().ToProductResponseDto(product);
    }

    public async Task<ProductResponseDto> GetOneProduct(Guid id)
    {
        var product = await FindProductOrThrow(id);
        return new ProductResponseDto().ToProductResponseDto(product);
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllProducts()
    {
        var products = await productRepository.GetAllProducts();
        return products
            .Select(p => new ProductResponseDto().ToProductResponseDto(p));
    }

    public async Task<ProductResponseDto> UpdateProduct(ProductUpdateDto dto)
    {
        var product = await FindProductOrThrow(dto.ProductId);

        product.Title = dto.Title ?? product.Title;
        product.Description = dto.Description ?? product.Description;
        product.Price = dto.Price ?? product.Price;
        product.ImageUrl = dto.ImageUrl ?? product.ImageUrl;

        var updatedProduct = await productRepository.UpdateProduct(product);
        return new ProductResponseDto().ToProductResponseDto(updatedProduct);
    }

    public async Task DeleteProduct(Guid id)
    {
        var product = await FindProductOrThrow(id);
        await productRepository.DeleteProduct(product);
    }
}
