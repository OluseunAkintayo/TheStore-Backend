using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TheStore.Models;

[Index(nameof(BrandName), IsUnique = true)]
public class Brand {
  [Key]
  public Guid BrandId { get; set; }
  [Required]
  public string BrandName { get; set; } = string.Empty;
  public Guid ManufacturerId { get; set; }
  public bool IsActive { get; set; }
  public bool Deleted { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? ModifiedAt { get; set; }
  public Guid CreatedBy { get; set; }
  public Guid? ModifiedBy { get; set; }
}
