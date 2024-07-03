namespace TheStore.Models.Users;

public class NewUserResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public User? Data { get; set; }
}
