using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TheStore.Models.CategoryModel;
using TheStore.Models.StockModel;
namespace TheStore.Models;

[Index(nameof(ProductCode), IsUnique = true)]
[Index(nameof(StockId), IsUnique = true)]
// [Index(nameof(ProductName), IsUnique = true)]

public class Product {
  [Key]
  public Guid Id { get; set; }
  [Required]
  public string ProductCode { get; set; } = string.Empty;
  public string ProductName { get; set; } = string.Empty;
  public string ProductDescription { get; set; } = string.Empty;
  public int? PictureId { get; set; }
  public Picture? Picture { get; set; }
  public bool IsActive { get; set; }
  public bool Deleted { get; set; }
  public decimal Cost { get; set; }
  public decimal Price { get; set; }
  public Guid StockId { get; set; }
  public Stock? Stock { get; set; }
  public Guid BrandId { get; set; }
  public Brand? Brand { get; set; }
  public Guid CategoryId { get; set; }
  public Category? Category { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? ModifiedAt { get; set; }
  public Guid CreatedBy { get; set; }
  public Guid? ModifiedBy { get; set; }
}

public class Picture {
  [Key]
  public int PictureId { get; set; }
  public string FileName { get; set; } = string.Empty;
  public string FileSrc { get; set; } = string.Empty;
}
