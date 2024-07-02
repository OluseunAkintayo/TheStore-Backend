using System.ComponentModel.DataAnnotations;

namespace TheStore.Models;

public class BrandDto {
  [Required]
  public string BrandName { get; set; } = string.Empty;
  public Guid ManufacturerId { get; set; }
  public bool IsActive { get; set; }
}
