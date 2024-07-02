using System.ComponentModel.DataAnnotations;
namespace TheStore.Models;

public class ProductDTO {
  public string? ProductCode { get; set; } = string.Empty;
  [Required]
  public string ProductName { get; set; } = string.Empty;
  [Required]
  public double Cost { get; set; }
  [Required]
  public double Price { get; set; }
  public Guid BrandId { get; set; }
  public Guid CategoryId { get; set; }
}
