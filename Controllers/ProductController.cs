using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
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
  private readonly UploadService uploadService;
  private readonly RepoService repo;
  public ProductController(ProductService _productService, RepoService _repo, UploadService _uploadService) {
    productService = _productService;
    repo = _repo;
    uploadService = _uploadService;
  }

  
  [HttpPost("new", Name = "NewProduct"), Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public ActionResult NewProduct([FromBody] ProductDTO productDto){
    UserClaims? user = GetCurrentUser();
    var response = productService.NewProduct(productDto, Guid.Parse(user!.UserId));
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }


  [HttpGet("admin/list", Name = "GetAdminProducts")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public ActionResult GetAdminProducts(){
    // var response = productService.GetAdminProducts();
    // if(!response.Success) return BadRequest(response);
    var res = (
      from product in repo.Products
      join brand in repo.Brands
      on product.BrandId equals brand.BrandId
      join category in repo.Categories
      on product.CategoryId equals category.CategoryId
      join stock in repo.Stocks
      on product.Id equals stock.ProductId
      select new {
        product.Id,
        product.ProductName,
        product.ProductDescription,
        product.Price,
        stock = new {
          stock.StockId,
          stock.Quantity,
          stock.ReorderLevel,
          stock.CostPrice
        },
        brand = new {
          brand.BrandId,
          brand.BrandName
        },
        category = new {
          category.CategoryId,
          category.CategoryName
        }
      }
    ).ToList();
    return Ok(res);
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
    var response = productService.DeleteProduct(Id, Guid.Parse(user.UserId));
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }


  [HttpPost("file/upload/{productId}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public ActionResult UploadPicture(List<IFormFile> files, Guid productId) {
    var res = uploadService.UploadPictures(files, productId);
    if(!res.Success) return BadRequest(res);
    return Ok(res);
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
