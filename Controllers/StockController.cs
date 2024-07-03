using Microsoft.AspNetCore.Mvc;
using TheStore.Models.StockModel;
using TheStore.Services.StockService;
namespace TheStore.Controllers;

[ApiController]
[Route("api/inventory")]

public class StockController : ControllerBase {
  private readonly StockService stockService;
  public StockController(StockService _stockService) {
    stockService = _stockService;
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpGet("/list", Name = "GetStock")]
  public ActionResult<StockResponse> GetStock() {
    var response = stockService.GetStock();
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }
}
