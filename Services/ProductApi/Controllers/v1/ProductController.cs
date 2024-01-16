using Microsoft.AspNetCore.Mvc;
using ProductApi.Models.DTO.Products;
using ProductApi.Services.Interfaces;

namespace ProductApi.Controllers.v1;

[Route("api/v{v:apiVersion}/products")]
public class ProductController(IProductService productService) : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ProductResponseDto>> Create([FromBody] ProductRequestDto dto)
    {
        var result = await productService.CreateProduct(dto);
        return Created(result.ProductId.ToString(), result);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll()
    {
        var result = await productService.GetAllProducts();
        return Ok(result);
    }
    
    [HttpGet("{productId:guid}")]
    [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductResponseDto>> GetOne(Guid productId)
    {
        var result = await productService.GetOneProduct(productId);
        return Ok(result);
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductResponseDto>> Update([FromBody] ProductUpdateDto dto)
    {
        var result = await productService.UpdateProduct(dto);
        return Ok(result);
    }
    
    [HttpDelete("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(Guid productId)
    {
        await productService.DeleteProduct(productId);
        return NoContent();
    }
}
