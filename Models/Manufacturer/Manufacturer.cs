using System.ComponentModel.DataAnnotations;

namespace TheStore.Models;

public class Manufacturer {
  [Key]
  public Guid Id { get; set; }
  [Required]
  public string ManufacturerName { get; set; } = string.Empty;
  public bool IsActive { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? ModifiedAt { get; set; }
  public Guid CreatedBy { get; set; }
  public Guid? UpdatedBy { get; set; }
}
