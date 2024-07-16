namespace TheStore.Models;

public class AdminProcuct {
  public string ProductCode { get; set; } = string.Empty;
  public string ProductName { get; set; } = string.Empty;
  public string ProductDescription { get; set; } = string.Empty;
  public int? PictureId { get; set; }
  public class Picture {
    public int PictureId { get; set; }
    public string FileSrc { get; set; } = string.Empty;
  }
  public bool IsActive { get; set; }
  public decimal Cost { get; set; }
  public decimal Price { get; set; }
  public Guid? StockId { get; set; }
  public class Stock {
    public Guid Id { get; set; }
    public int MyProperty { get; set; }
  }
  public Guid BrandId { get; set; }
  public class Brand {
    public Guid Id { get; set; }
    public string BrandName { get; set; } = string.Empty;
  }
  public Guid CategoryId { get; set; }
  public class CategoryInfo {
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
  }
  public DateTime CreatedAt { get; set; }
  public DateTime? ModifiedAt { get; set; }
  public Guid CreatedBy { get; set; }
  public Guid? ModifiedBy { get; set; }
}
