using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheStore.Models;
using TheStore.Services.BrandService;
namespace TheStore.Controllers;

[ApiController]
[Route("api/products/brands")]
public class BrandController : ControllerBase {
  private readonly BrandService brandService;
  public BrandController(BrandService _brandService) {
    brandService = _brandService;
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpPost("", Name = "NewBrand")]
  public ActionResult<BrandResponse> NewBrand([FromBody] BrandDto brandDto){
    var response = brandService.CreateBrand(brandDto);
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }



  [HttpGet("", Name = "GetAllBrands")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public ActionResult<BrandResponse> GetAllBrands(){
    var response = brandService.GetAllBrands();
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpGet("{id}", Name = "GetBrand")]
  public ActionResult<BrandResponse> GetBrand(Guid id){
    var response = brandService.GetBrand(id);
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }


  [Authorize(Roles = "administrator")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpPut("update/{id}", Name = "UpdateBrand")]
  public ActionResult UpdateBrand(Guid id, [FromBody] BrandDto brand){
    var res = brandService.UpdateBrand(id, brand);
    if(!res.Success) return BadRequest(res);
    return Ok(res);
  }


  [Authorize(Roles = "administrator")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpPost("deactivate/{id}", Name = "DeactivateBrand")]
  public ActionResult<BrandResponse> DeactivateBrand(Guid id){
    var response = brandService.DeactivateBrand(id);
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }
}
