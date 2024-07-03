using System.ComponentModel.DataAnnotations;

namespace TheStore.Models.Users;

public class LoginUser {
  [Required, EmailAddress]
  public string Username { get; set; } = string.Empty;
  [Required]
  public string Passcode { get; set; } = string.Empty;
}
