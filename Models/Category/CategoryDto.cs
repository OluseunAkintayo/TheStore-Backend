using System.ComponentModel.DataAnnotations;
namespace TheStore.Models.CategoryModel;

public class CategoryDto {
  [Required]
  public string CategoryName { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
}

public class EditCategoryDto {
  [Required]
  public string CategoryName { get; set; } = string.Empty;
  [Required]
  public string Description { get; set; } = string.Empty;
  [Required]
  public bool IsActive { get; set; }
}
