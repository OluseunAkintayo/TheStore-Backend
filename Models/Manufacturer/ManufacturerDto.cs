using System.ComponentModel.DataAnnotations;

namespace TheStore.Models;

public class ManufacturerDto {
  [Required]
  public string ManufacturerName { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; }
}
