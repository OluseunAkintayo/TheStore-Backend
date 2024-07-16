using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace TheStore.Models.CategoryModel;

[Index(nameof(CategoryName), IsUnique = true)]

public class Category {
  [Key]
  public Guid CategoryId { get; set; }
  [Required]
  public string CategoryName { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public bool IsActive { get; set; }
  public bool Deleted { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? ModifiedAt { get; set; }
  public Guid CreatedBy { get; set; }
  public Guid? ModifiedBy { get; set; }
}
