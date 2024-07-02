using Microsoft.AspNetCore.Mvc;
using TheStore.Models.CategoryModel;
using TheStore.Services.CategoryService;
namespace TheStore.Controllers.CategoryController;

[ApiController]
[Route("api/products/category")]
public class CategoryController : ControllerBase {
  private readonly CategoryService categoryService;
  public CategoryController(CategoryService _categoryService) {
    categoryService = _categoryService;
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpPost("new", Name = "NewCategory")]
  public ActionResult<CategoryResponse> NewCategory([FromBody] CategoryDto brandDto){
    var response = categoryService.CreateCategory(brandDto);
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpGet("list", Name = "GetCategories")]
  public ActionResult<CategoryResponse> GetCategories(){
    var response = categoryService.GetAllcategories();
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpGet("{id}", Name = "GetCategory")]
  public ActionResult<CategoryResponse> GetCategory(Guid id){
    var response = categoryService.GetCategory(id);
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }


  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpPut("update/{id}", Name = "UpdateCategory")]
  public ActionResult UpdateCategory(Guid id, [FromBody] CategoryDto brand){
    var res = categoryService.UpdateCategory(id, brand);
    if(!res.Success) return BadRequest(res);
    return Ok(res);
  }


  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpPut("deactivate/{id}", Name = "DeactivateCategory")]
  public ActionResult<CategoryResponse> DeactivateCategory(Guid id){
    var response = categoryService.DeactivateCategory(id);
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }
}
