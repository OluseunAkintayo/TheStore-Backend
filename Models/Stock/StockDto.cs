namespace TheStore.Models.StockModel;
public class NewStockDto {
  public int Quantity { get; set; } = 0;
  public int ReorderLevel { get; set; } = 0;
  public decimal CostPrice { get; set; }
  public Guid ProductId { get; set; }
}


public class UpdateStockDto {
  public int Quantity { get; set; } = 0;
  public int ReorderLevel { get; set; } = 0;
  public decimal CostPrice { get; set; }
}
