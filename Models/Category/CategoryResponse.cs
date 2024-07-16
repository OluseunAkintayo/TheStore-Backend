namespace TheStore.Models.CategoryModel;

public class CategoryResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public List<Category>? Data { get; set; }
}

public class CategoryItemResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public Category? Data { get; set; }
}
