using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TheStore.Models.Users;
using TheStore.Services;
namespace TheStore.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase {
  private readonly AuthService authService;
  public AuthController(AuthService _authService) {
    authService = _authService;
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpPost("users", Name = "NewUser")]
  public async Task<ActionResult> NewUser([FromBody] UserDto user){
    var response = await authService.NewUser(user);
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpGet("verify", Name = "VerifyUser")]
  public async Task<ActionResult> VerifyUser(string token){
    var response = await authService.VerifyUser(token);
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpPost("login", Name = "Login")]
  public ActionResult Login([FromBody] LoginUser user){
    var response = authService.Login(user);
    if(!response.Success) return BadRequest(response);
    return Ok(response);
  }


  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [HttpGet("currentuser", Name = "GetCurrentUser")]
  public ActionResult? GetCurrentUser(){
    var identity = HttpContext.User.Identity as ClaimsIdentity;
    if(identity == null) {
      return null;
    };
    var claims = identity.Claims;
    UserClaims userClaims = new() {
      UserId = claims.FirstOrDefault(u => u.Type == "UserId")?.Value!,
      Email = claims.FirstOrDefault(u => u.Type == ClaimTypes.Email)?.Value!,
      Role = claims.FirstOrDefault(u => u.Type == ClaimTypes.Role)?.Value!
    };
    return Ok(userClaims);
  }
}
