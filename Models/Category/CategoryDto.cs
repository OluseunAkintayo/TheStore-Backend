using System.ComponentModel.DataAnnotations;
namespace TheStore.Models.CategoryModel;

public class CategoryDto {
  [Required]
  public string CategoryName { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
}
