using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheStore.Models.CategoryModel;
using TheStore.Models.Users;
using TheStore.Services.CategoryService;
namespace TheStore.Controllers;

[ApiController]
[Route("api/products/categories")]
public class CategoryController : ControllerBase {

  private UserClaims? GetCurrentUser(){
    if (HttpContext.User.Identity is not ClaimsIdentity identity) {
      return null;
    };
    var claims = identity.Claims;
    UserClaims userClaims = new() {
      UserId = claims.FirstOrDefault(u => u.Type == "UserId")?.Value!,
      Email = claims.FirstOrDefault(u => u.Type == ClaimTypes.Email)?.Value!,
      Role = claims.FirstOrDefault(u => u.Type == ClaimTypes.Role)?.Value!
    };
    return userClaims;
  }
  
  private readonly CategoryService categoryService;
  public CategoryController(CategoryService _categoryService) {
    categoryService = _categoryService;
  }

  [Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpPost("new", Name = "NewCategory")]
  public ActionResult NewCategory([FromBody] CategoryDto brandDto){
    var response = categoryService.CreateCategory(brandDto);
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }

  [Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpGet("list", Name = "GetCategories")]
  public ActionResult GetCategories(){
    var response = categoryService.GetAllcategories();
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpGet("{id}", Name = "GetCategory")]
  public ActionResult GetCategory(Guid id){
    var response = categoryService.GetCategory(id);
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }

  [Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpPut("update/{id}", Name = "UpdateCategory")]
  public ActionResult UpdateCategory(Guid id, [FromBody] EditCategoryDto category){
    var res = categoryService.UpdateCategory(id, category);
    if(!res.Success) return BadRequest(res);
    return Ok(res);
  }

  [Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpDelete("delete/{id}", Name = "DeleteCategory")]
  public ActionResult DeleteCategory(Guid id){
    var response = categoryService.DeactivateCategory(id);
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }

  [Authorize(Roles = "administrator")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpGet("list/deleted", Name = "GetDeletedCategories")]
  public ActionResult GetDeletedCategories(){
    var user = GetCurrentUser();
    if(user?.UserId == null) {
      var error = new {
        Success = false,
        Message = "Unauthorized access"
      };
      return Unauthorized(error);
    }
    var response = categoryService.GetDeletedCategories();
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }


  [Authorize(Roles = "administrator")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpPost("restore/{id}", Name = "RestoreCategory")]
  public ActionResult RestoreCategory(Guid id){
    var user = GetCurrentUser();
    if(user?.UserId == null) {
      var error = new {
        Success = false,
        Message = "Unauthorized access"
      };
      return Unauthorized(error);
    }
    var response = categoryService.RestoreCategory(id, Guid.Parse(user.UserId));
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }
}
