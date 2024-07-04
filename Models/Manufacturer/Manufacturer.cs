using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TheStore.Models;

[Index(nameof(ManufacturerName), IsUnique = true)]
public class Manufacturer {
  [Key]
  public Guid Id { get; set; }
  [Required]
  public string ManufacturerName { get; set; } = string.Empty;
  public bool IsActive { get; set; }
  public Guid CreatedBy { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? ModifiedAt { get; set; }
  public Guid? ModifiedBy { get; set; }
}
