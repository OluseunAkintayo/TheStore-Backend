namespace TheStore.Models;

public class AdminProductResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public List<AdminProduct>? Data { get; set; }
}

public class AdminProduct {
  public Guid Id { get; set; }
  public string ProductCode { get; set; } = string.Empty;
  public string ProductName { get; set; } = string.Empty;
  public string ProductDescription { get; set; } = string.Empty;
  public decimal Price { get; set; }
  public Guid StockId { get; set; }
  public int Quantity { get; set; }
  public int ReorderLevel { get; set; }
  public decimal Cost { get; set; }
  public Guid BrandId { get; set; }
  public string BrandName { get; set; } = string.Empty;
  public Guid CategoryId { get; set; }
  public string CategoryName { get; set; } = string.Empty;
  public List<string> Pictures { get; set; }
  public bool IsActive { get; set; }
  public bool Deleted { get; set; }
  public Guid CreatedBy { get; set; }
  public DateTime? ModifiedAt { get; set; }
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
