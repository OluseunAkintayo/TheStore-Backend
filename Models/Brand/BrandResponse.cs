namespace TheStore.Models;

public class BrandResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public List<Brand>? Data { get; set; }
}
