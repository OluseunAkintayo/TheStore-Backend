namespace TheStore.Models;

public class ProductResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public List<Product>? Data { get; set; }
}
