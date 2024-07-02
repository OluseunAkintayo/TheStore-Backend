using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheStore.Controllers.User;

[Index(nameof(Username), IsUnique = true)]

public class User {
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }
  [Required]
  public string Username { get; set; } = string.Empty;
  public string PasswordHash { get; set; } = string.Empty;
  public List<string> Role { get; set; } = new List<string>(){ "" };
  public string IsActive { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; }
  public DateTime? LastLogin { get; set; }
  public DateTime? ModifiedAt { get; set; }
}
