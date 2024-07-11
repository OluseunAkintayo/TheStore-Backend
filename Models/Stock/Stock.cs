using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace TheStore.Models.StockModel;

[Index(nameof(ProductId), IsUnique = true)]
public class Stock {
  [Key]
  public Guid StockId { get; set; }
  [Required]
  public int Quantity { get; set; } = 0;
  public int ReorderLevel { get; set; } = 0;
  public decimal CostPrice { get; set; }
  public Guid ProductId { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? ModifiedAt { get; set; }
  public Guid CreatedBy { get; set; }
  public Guid? ModifiedBy { get; set; }
}
