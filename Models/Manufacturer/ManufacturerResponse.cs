using TheStore.Models;

namespace TheStore.Models;

public class ManufacturerResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public List<Manufacturer>? Data { get; set; }
}
