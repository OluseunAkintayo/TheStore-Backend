using System.ComponentModel.DataAnnotations;

namespace TheStore.Models;

public class ManufacturerDto {
  [Required]
  public string ManufacturerName { get; set; } = string.Empty;
}

public class EditManufacturerDto {
  [Required]
  public string ManufacturerName { get; set; } = string.Empty;
  [Required]
  public bool IsActive { get; set; }
}
