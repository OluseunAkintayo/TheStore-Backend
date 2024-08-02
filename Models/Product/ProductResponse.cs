namespace TheStore.Models;

public class AdminProductResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public List<AdminProduct>? Data { get; set; }
}

public class AdminProduct {
  public Guid Id { get; set; }
  public string ProductName { get; set; } = string.Empty;
  public string ProductDescription { get; set; } = string.Empty;
  public decimal Price { get; set; }
  public Guid StockId { get; set; }
  public int Quantity { get; set; }
  public int ReorderLevel { get; set; }
  public decimal CostPrice { get; set; }
  public Guid BrandId { get; set; }
  public string BrandName { get; set; } = string.Empty;
  public Guid CategoryId { get; set; }
  public string CategoryName { get; set; } = string.Empty;
}

public class ProductResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public List<Product>? Data { get; set; }
}

public class ProductItemResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public Product? Data { get; set; }
}
