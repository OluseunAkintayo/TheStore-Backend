using System.ComponentModel.DataAnnotations;
namespace TheStore.Models;

public class ProductDTO {
  public string ProductCode { get; set; } = string.Empty;
  [Required]
  public string ProductName { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  [Required]
  public decimal Cost { get; set; }
  [Required]
  public decimal Price { get; set; }
  public Guid BrandId { get; set; }
  public Guid CategoryId { get; set; }
  public List<string> Pictures { get; set; }
}


public class EditProductDTO {
  public string ProductCode { get; set; } = string.Empty;
  [Required]
  public string ProductName { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public bool IsActive { get; set; }
  [Required]
  public decimal Cost { get; set; }
  [Required]
  public decimal Price { get; set; }
  public Guid BrandId { get; set; }
  public Guid CategoryId { get; set; }
  public List<string> Pictures { get; set; }
}
