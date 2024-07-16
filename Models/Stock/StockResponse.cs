namespace TheStore.Models.StockModel;

public class StockResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public List<Stock>? Data { get; set; }
}

public class ProductStockResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public List<StockItems>? Data { get; set; }
}


public class StockItems {
  public Guid StockId { get; set; }
  public Guid ProductId { get; set; }
  public string ProductName { get; set; } = string.Empty;
  public int Quantity { get; set; }
  public decimal Cost { get; set; }
  public decimal Price { get; set; }
  public DateTime CreatedAt { get; set; }
}