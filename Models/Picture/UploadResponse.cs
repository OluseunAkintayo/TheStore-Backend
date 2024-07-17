namespace TheStore.Models;
public class UploadResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public Picture? Data { get; set; }
  
}


public class UploadPictureResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public List<string>? PictureUri { get; set; }
}
