using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using TheStore.Models.Users;
namespace TheStore.Services;

public class AuthService {
  private  readonly RepoService repo;
  private readonly EmailService emailService;
  private readonly IConfiguration config;

  private readonly IHttpContextAccessor httpContext;
  public AuthService(RepoService _repo, EmailService _emailService, IConfiguration _config, IHttpContextAccessor _httpContext){
    repo = _repo;
    emailService = _emailService;
    config = _config;
    httpContext = _httpContext;
  }

  public async Task<NewUserResponse> NewUser(UserDto userDto) {
    var currentUser = repo.Users.FirstOrDefault(item => item.Username == userDto.Username);
    if (currentUser != null) {
      var error = new NewUserResponse {
        Success = false,
        Message = "Username already exists"
      };
      return error;
    }

    string VerificationToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

    User user = new() {
      Username = userDto.Username,
      PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(userDto.Password, BCrypt.Net.HashType.SHA512, workFactor: 12),
      Role = userDto.Role,
      IsActive = false,
      VerificationToken = VerificationToken,
      CreatedAt = DateTime.UtcNow
    };

    repo.Users.Add(user);
    await repo.SaveChangesAsync();

    var response = new NewUserResponse() {
      Success = true,
      Message = "User created successfully! A confirmation email has been sent to the provided email address",
    };

    string messageContent = $"""
      <!DOCTYPE html>
      <html lang="en">
      <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Welcome to TheStore</title>
        <style>
          * "{" margin: 0; padding: 0; font-family: system-ui, -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif; color: #222222; "}"
        </style>
      </head>
      <body>
        <div>
          <p>Dear Customer,</p>
          <p style="margin: 16px 0 12px 0;"><b>Welcome to TheStore!</b> <span>We are pleased to have you on board.</span></p>
          <p>To complete the registration process, please click <a style="color: teal;" href="http://localhost:5279/api/auth/verify?token={user.VerificationToken}">here</a> to confirm your email.</p>
          <p style="margin-top: 18px;">Thank you!</p>
        </div>
      </body>
      </html>
    """;

    emailService.SendMail(user.Username, "Verify your email address", messageContent);
    return response;
  }

  
  public async Task<NewUserResponse> VerifyUser(string token) {
    var user = repo.Users.FirstOrDefault(u => u.VerificationToken == token);
    if (user == null) {
      var error = new NewUserResponse() {
        Success = false,
        Message = "Invalid token"
      };
      return error;
    }

    if(user.IsActive) {
      var message = new NewUserResponse() {
        Success = false,
        Message = "The email address has been confirmed already"
      };
      return message;
    }

    user.IsActive = true;
    user.VerificationDate = DateTime.UtcNow;
    
    repo.Users.Update(user);
    await repo.SaveChangesAsync();

    var response = new NewUserResponse() {
      Success = true,
      Message = "User verified successfully"
    };
    return response;
  }
  

  public LoginResponse Login(LoginUser loginUser) {
    var user = repo.Users.FirstOrDefault(item => item.Username == loginUser.Username);
    if(user == null) {
      var error = new LoginResponse {
        Success = false,
        Message = "Invalid username or password"
      };
      return error;
    }

    if(!BCrypt.Net.BCrypt.EnhancedVerify(loginUser.Passcode, user.PasswordHash, HashType.SHA512)) {
      var passwordError = new LoginResponse {
        Success = false,
        Message = "Invalid username or password"
      };
      return passwordError;
    }

    TokenResponse token = CreateToken(user);
    LoginResponse response = new() {
      Success = true,
      Message = "User logged in",
      Data = new() {
        User = user.Username,
        AccessToken = token.Token,
        ExpirationDate = token.ExpirationDate
      }
    };
    user.LastLogin = DateTime.UtcNow;
    repo.SaveChanges();
    return response;
  }

  private TokenResponse CreateToken(User user){
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
    var claims = new List<Claim> {
      new("UserId", $"{user.Id}"),
      new(ClaimTypes.Email, user.Username),
      new(ClaimTypes.Role, user.Role.Contains("administrator") ? "administrator" : (user.Role.Contains("vendor") ? "vendor" : "customer" ))
    };
    DateTime exp = DateTime.Now.AddMinutes(5);
    var token = new JwtSecurityToken(
      config["Jwt:Issuer"],
      config["Jwt:Audience"],
      claims: claims,
      expires: exp,
      signingCredentials: credentials
    );
    TokenResponse tokenResponse = new() {
      Token = new JwtSecurityTokenHandler().WriteToken(token),
      ExpirationDate = exp,
    };
    return tokenResponse;
  }
}

public class TokenResponse {
  public string Token { get; set; } = string.Empty;
  public DateTime ExpirationDate { get; set; }
}