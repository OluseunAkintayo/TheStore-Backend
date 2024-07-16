using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheStore.Models;
using TheStore.Models.Users;
using TheStore.Services;
namespace TheStore.Controllers;

[ApiController]
[Route("api/products/manufacturer")]
public class ManufacturerController : ControllerBase {
  private readonly ManufacturerService manufacturerService;
  public ManufacturerController(ManufacturerService _manufacturerService) {
    manufacturerService = _manufacturerService;
  }

  [Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpPost("new", Name = "NewManufacturer")]
  public ActionResult NewManufacturer([FromBody] ManufacturerDto manufacturerDto){
    UserClaims? claims = GetCurrentUser();
    if(claims?.UserId == null) {
      var error = new {
        Success = false,
        Message = "Unauthorized access"
      };
      return Unauthorized(error);
    }
    var response = manufacturerService.CreateManufacturer(manufacturerDto, Guid.Parse(claims!.UserId));
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }

  
  [Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpGet("list", Name = "GetManufacturers")]
  public ActionResult GetManufacturers(){
    var response = manufacturerService.GetManufacturers();
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }

  
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpGet("{id}", Name = "GetManufacturer")]
  public ActionResult<ManufacturerResponse> GetManufacturer(Guid id){
    var response = manufacturerService.GetManufacturer(id);
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }


  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpPut("update/{id}", Name = "UpdateManufacturer")]
  public ActionResult UpdateManufacturer(Guid id, [FromBody] EditManufacturerDto manufacturer){
    UserClaims? claims = GetCurrentUser();
    if(claims?.UserId == null) {
      var error = new {
        Success = false,
        Message = "Unauthorized access"
      };
      return Unauthorized(error);
    }
    var res = manufacturerService.UpdateManufacturer(id, manufacturer, Guid.Parse(claims.UserId));
    if(!res.Success) return BadRequest(res);
    return Ok(res);
  }


  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpDelete("delete/{id}", Name = "DeactivateManufacturer")]
  public ActionResult<ManufacturerResponse> DeactivateManufacturer(Guid id){
    UserClaims? claims = GetCurrentUser();
    if(claims?.UserId == null) {
      var error = new {
        Success = false,
        Message = "Unauthorized access"
      };
      return Unauthorized(error);
    }
    var response = manufacturerService.DeactivateManufacturer(id, Guid.Parse(claims.UserId));
    if(!response.Success) return BadRequest(response);
    return Ok(response);
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
}
