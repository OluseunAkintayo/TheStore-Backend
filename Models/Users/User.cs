using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheStore.Models.Users;

[Index(nameof(Username), IsUnique = true)]

public class User {
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }
  [Required, EmailAddress]
  public string Username { get; set; } = string.Empty;
  [Required]
  public string PasswordHash { get; set; } = string.Empty;
  [Required]
  public List<string> Role { get; set; } = new List<string>(){ "" };
  [Required]
  public bool IsActive { get; set; }
  public bool Deleted { get; set; }
  public string VerificationToken { get; set; } = string.Empty;
  public DateTime? VerificationDate { get; set; }
  public string? PasswordResetToken { get; set; } = string.Empty;
  public DateTime? PasswordResetTokenExpiryDate { get; set; }
  public DateTime? PasswordResetTokenVerificationDate { get; set; }
  [Required]
  public DateTime CreatedAt { get; set; }
  public DateTime? LastLogin { get; set; }
  public DateTime? ModifiedAt { get; set; }
}
