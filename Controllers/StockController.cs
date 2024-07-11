using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TheStore.Models.StockModel;
using TheStore.Models.Users;
using TheStore.Services.StockService;
namespace TheStore.Controllers;

[ApiController]
[Route("api/inventory")]

public class StockController : ControllerBase {
  private readonly StockService stockService;
  public StockController(StockService _stockService) {
    stockService = _stockService;
  }

  [HttpGet("/update/{stockId}", Name = "GetStock"), Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public ActionResult<StockResponse> GetStock(Guid stockId, [FromBody] UpdateStockDto stock) {
    UserClaims? user = GetCurrentUser();
    if(user == null || user?.UserId == null) {
      StockResponse unauthorizedError = new() {
        Message = "Unauthorized: user not found",
        Success = false
      };
      return Unauthorized(unauthorizedError);
    }
    var response = stockService.UpdateStock(stockId, stock, Guid.Parse(user.UserId));
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
