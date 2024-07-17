using System.ComponentModel.DataAnnotations;
namespace TheStore.Models;
public class Picture {
  [Key]
  public int PictureId { get; set; }
  public string Filename { get; set; } = string.Empty;
  public string Uri { get; set; } = string.Empty;
  public string ContentType { get; set; } = string.Empty;
  // public Guid ProductId { get; set; }
}
