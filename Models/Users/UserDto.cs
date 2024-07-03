using System.ComponentModel.DataAnnotations;
namespace TheStore.Models.Users;

public class UserDto {
  [Required, EmailAddress]
  public string Username { get; set; } = string.Empty;
  [Required]
  public string Password { get; set; } = string.Empty;
  [Required]
  public List<string> Role { get; set; } = new List<string>(){ "" };
}
