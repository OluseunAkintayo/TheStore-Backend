using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheStore.Models;
using TheStore.Models.Users;
using TheStore.Services.ProductService;
namespace TheStore.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase {
  private readonly ProductService productService;
  public ProductController(ProductService _productService) {
    productService = _productService;
  }

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

  [Authorize(Roles = "administrator, vendor")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpPost("new", Name = "NewProduct")]
  public ActionResult<ProductResponse> NewProduct([FromBody] ProductDTO productDto){
    var currentUser = GetCurrentUser();
    var response = productService.NewProduct(productDto, Guid.Parse(currentUser!.UserId));
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }


  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpGet("list", Name = "GetProducts")]
  public ActionResult<ProductResponse> GetProducts(){
    var response = productService.GetProducts();
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpGet("{id}", Name = "GetProduct")]
  public ActionResult<ProductResponse> GetProduct(Guid id){
    var response = productService.GetProduct(id);
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }


  [Authorize(Roles = "administrator, vendor")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpPut("update/{id}", Name = "UpdateProduct")]
  public ActionResult UpdateProduct(Guid id, [FromBody] ProductDTO product){
    var currentUser = GetCurrentUser();
    var res = productService.UpdateProduct(id, product, Guid.Parse(currentUser.UserId));
    if(!res.Success) return BadRequest(res);
    return Ok(res);
  }
}
