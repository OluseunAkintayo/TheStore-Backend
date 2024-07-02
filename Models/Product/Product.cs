using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TheStore.Models.CategoryModel;
using TheStore.Models.StockModel;
namespace TheStore.Models;

[Index(nameof(ProductCode), IsUnique = true)]
[Index(nameof(ProductName), IsUnique = true)]

public class Product {
  [Key]
  public Guid Id { get; set; }
  [Required]
  public string ProductCode { get; set; } = string.Empty;
  public string ProductName { get; set; } = string.Empty;
  public string ProductDescription { get; set; } = string.Empty;
  public bool IsActive { get; set; }
  public double Cost { get; set; }
  public double Price { get; set; }
  public Guid StockId { get; set; }
  public Stock? StockLevel { get; set; }
  public Guid BrandId { get; set; }
  public Brand? Brand { get; set; }
  public Guid CategoryId { get; set; }
  public Category? Category { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? ModifiedAt { get; set; }
}

public class Picture {
  [Key]
  public int PictureId { get; set; }
  public string FileName { get; set; } = string.Empty;
  public string File { get; set; } = string.Empty;
}
