using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheStore.Models;
using TheStore.Models.Users;
using TheStore.Services;
using TheStore.Services.ProductService;
namespace TheStore.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase {
  private readonly ProductService productService;
  private readonly RepoService repo;
  public ProductController(ProductService _productService, RepoService _repo) {
    productService = _productService;
    repo = _repo;
  }

  [HttpPost("new", Name = "NewProduct"), Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public ActionResult NewProduct([FromForm] ProductDTO productDto, List<IFormFile> Pictures){
    UserClaims? user = GetCurrentUser();
    if(user == null) return Unauthorized();
    if(user.UserId == null) return Unauthorized();
    var response = productService.NewProduct(productDto, Guid.Parse(user.UserId), Pictures);
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }


  [HttpGet("admin-products", Name = "GetAdminProducts")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public ActionResult GetAdminProducts(){
    var response = productService.GetAdminProducts();
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }

  
  [HttpGet("list", Name = "GetProducts")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public ActionResult GetProducts(){
    var response = productService.GetProducts();
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }

  [HttpGet("{id}", Name = "GetProduct")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public ActionResult GetProduct(Guid id){
    var response = productService.GetProduct(id);
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }


  [HttpPut("update/{id}", Name = "UpdateProduct"), Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public ActionResult UpdateProduct(Guid id, [FromBody] ProductDTO product){
    var res = productService.UpdateProduct(id, product);
    if(!res.Success) return BadRequest(res);
    return Ok(res);
  }

  [HttpDelete("delete/{id}"), Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public ActionResult DeleteProduct(Guid Id) {
    UserClaims? user = GetCurrentUser();
    if(user == null) return Unauthorized();
    if(user.UserId == null) return Unauthorized();
    var response = productService.DeleteProduct(Id, Guid.Parse(user.UserId));
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }


  // get current user from token
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
}
