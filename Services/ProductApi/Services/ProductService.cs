using ProductApi.Data.Entities;
using ProductApi.Exceptions;
using ProductApi.Models.DTO.Products;
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
        if (await productRepository.ProductExistsByTitle(dto.Title))
            throw new AlreadyExistException(nameof(Product), dto.Title);
        
        var product = new Product
        {
            Title = dto.Title,
            Description = dto.Description,
            ImageName = dto.ImageUrl,
            Price = dto.Price,
            Category = await FindCategoryOrThrow(dto.CategoryId)
        };
        
        return await productRepository.CreateProduct(product)
            ? new ProductResponseDto(product)
            : throw new DbException();
    }

    public async Task<ProductResponseDto> GetOneProduct(Guid productId)
    {
        var product = await FindProductOrThrow(productId);
        return new ProductResponseDto(product);
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllProducts()
    {
        var products = await productRepository.GetAllProducts();
        return products
            .Select(p => new ProductResponseDto(p));
    }
    
    public async Task<ProductResponseDto> UpdateProduct(ProductUpdateDto dto)
    {
        var product = await FindProductOrThrow(dto.ProductId);

        product.Title = dto.Title ?? product.Title;
        product.Description = dto.Description ?? product.Description;
        product.Price = dto.Price ?? product.Price;
        product.ImageName = dto.ImageUrl ?? product.ImageName;

        return await productRepository.UpdateProduct(product)
            ? new ProductResponseDto(product)
            : throw new DbException();
    }

    public async Task DeleteProduct(Guid id)
    {
        var product = await FindProductOrThrow(id);
        
        if (!await productRepository.DeleteProduct(product))
            throw new DbException();
    }
}