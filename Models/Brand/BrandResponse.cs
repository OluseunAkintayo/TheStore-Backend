namespace TheStore.Models;

public class AllBrandsResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public List<AllBrands>? Data { get; set; }
}
public class AllBrands {
  public Guid BrandId {get; set; }
  public string BrandName { get; set; } = string.Empty;
  public Guid ManufacturerId {get; set;}
  public string ManufacturerName { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; }
}

public class BrandResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public List<Brand>? Data { get; set; }
}

public class BrandItemResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public Brand? Data { get; set; }
}
