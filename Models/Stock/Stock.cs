using System.ComponentModel.DataAnnotations;
namespace TheStore.Models.StockModel;

public class Stock {
  [Key]
  public Guid StockId { get; set; }
  [Required]
  public int Warehouse { get; set; } = 0;
  [Required]
  public int Shop { get; set; } = 0;
  public int ReorderLevel { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? ModifiedAt { get; set; }
  public Guid CreatedBy { get; set; }
  public Guid? ModifiedBy { get; set; }
}
