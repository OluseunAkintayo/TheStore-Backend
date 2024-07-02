using System.ComponentModel.DataAnnotations;

namespace TheStore.Models;

public class Brand {
  [Key]
  public Guid BrandId { get; set; }
  [Required]
  public string BrandName { get; set; } = string.Empty;
  public Guid ManufacturerId { get; set; }
  public Manufacturer? Manufacturer { get; set; }
  public bool IsActive { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? ModifiedAt { get; set; }
}
