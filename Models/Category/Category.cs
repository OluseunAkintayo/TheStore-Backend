using System.ComponentModel.DataAnnotations;
namespace TheStore.Models.CategoryModel;

public class Category {
  [Key]
  public Guid CategoryId { get; set; }
  [Required]
  public string CategoryName { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public bool IsActive { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? ModifiedAt { get; set; }
}
