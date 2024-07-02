using System.ComponentModel.DataAnnotations;
namespace TheStore.Models.StockModel;

public class StockDto {
  [Required]
  public int Warehouse { get; set; } = 0;
  [Required]
  public int Shop { get; set; } = 0;
}
