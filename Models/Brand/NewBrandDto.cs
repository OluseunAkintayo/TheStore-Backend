using System.ComponentModel.DataAnnotations;

namespace TheStore.Models;

public class NewBrandDto {
  [Required]
  public string BrandName { get; set; } = string.Empty;
  public Guid ManufacturerId { get; set; }
}
