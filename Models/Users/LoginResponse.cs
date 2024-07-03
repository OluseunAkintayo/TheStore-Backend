namespace TheStore.Models.Users;

public class LoginResponse {
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public Data? Data {get; set; }
}

public class Data {
  public string AccessToken { get; set; } = string.Empty;
  public DateTime ExpirationDate { get; set; }
  public string User { get; set; } = string.Empty;
}
