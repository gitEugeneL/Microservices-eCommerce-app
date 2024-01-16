using Microsoft.AspNetCore.Mvc;
using ProductApi.Models.DTO.Categories;
using ProductApi.Services.Interfaces;

namespace ProductApi.Controllers.v1;

[Route("api/v{v:apiVersion}/categories")]
public class CategoryController(ICategoryService categoryService) : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetAll()
    {
        var result = await categoryService.GetAllCategories();
        return Ok(result);
    }
    
    [HttpGet("{categoryId:guid}")]
    [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<CategoryResponseDto>> GetOne(Guid categoryId)
    {
        var result = await categoryService.GetOneCategory(categoryId);
        return Ok(result);
    }
}
