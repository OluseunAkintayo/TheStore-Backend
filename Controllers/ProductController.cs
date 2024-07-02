using Microsoft.AspNetCore.Mvc;
using TheStore.Models;
using TheStore.Services.ProductService;
namespace TheStore.Controllers.ProductController;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase {
  private readonly ProductService productService;
  public ProductController(ProductService _productService) {
    productService = _productService;
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpPost("new", Name = "NewProduct")]
  public ActionResult<ProductResponse> NewProduct([FromBody] ProductDTO productDto){
    var response = productService.NewProduct(productDto);
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


  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpPut("update/{id}", Name = "UpdateProduct")]
  public ActionResult UpdateProduct(Guid id, [FromBody] ProductDTO product){
    var res = productService.UpdateProduct(id, product);
    if(!res.Success) return BadRequest(res);
    return Ok(res);
  }
}
