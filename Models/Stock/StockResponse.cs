namespace TheStore.Models.StockModel;

public class StockResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public List<Stock>? Data { get; set; }
  public List<Product>? ProductStock { get; set; }
}