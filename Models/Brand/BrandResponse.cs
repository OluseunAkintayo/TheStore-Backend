namespace TheStore.Models;

public class BrandResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public List<Brand>? Data { get; set; }
}

public class BrandManufacturer {
  public Guid BrandId { get; set; }
  public string BrandName { get; set; } = string.Empty;
  public string ManufacturerName { get; set; } = string.Empty;
  public Guid ManufacturerId { get; set; }
  public bool IsActive { get; set; }
  public DateTime CreatedAt { get; set; }
}

public class BrandManufacturerResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public List<BrandManufacturer>? Data { get; set; }
}
