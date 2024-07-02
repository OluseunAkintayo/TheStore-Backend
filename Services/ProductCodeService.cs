using System.Security.Cryptography;

public class ProductCodeService {
  private readonly int OtpLength = 12;
  private const int TimeStep = 1; // time in seconds before otp is changed
  private const string Base32Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
  private readonly IConfiguration configuration;
  public ProductCodeService(IConfiguration _configuration) {
    configuration = _configuration;
  }
  public string GetProductCode() {
    string SecretKey = configuration.GetSection("ConnectionStrings:SecretKey").Value!;
    long counter = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / TimeStep;
    byte[] counterBytes = BitConverter.GetBytes(counter);
    byte[] secretKeyBytes = ConvertToBytes(SecretKey);
    var hmac = new HMACSHA1(secretKeyBytes);
    byte[] hash = hmac.ComputeHash(counterBytes);
    int offset = hash[^1] & 0x0F;
    int binary = ((hash[offset] & 0x7f) << 24) | (hash[offset + 1] << 16) | (hash[offset + 2] << 8) | hash[offset + 3];
    int otp = binary % (int)Math.Pow(10, OtpLength);
    string productCode = otp.ToString().PadLeft(OtpLength, '0');
    return productCode;
  }

  private static byte[] ConvertToBytes(string input) {
    var bits = string.Join(string.Empty, input.Select(c => Convert.ToString(Base32Alphabet.IndexOf(c), 2).PadLeft(5, '0')));
    var bytes = Enumerable.Range(0, bits.Length / 8).Select(i => Convert.ToByte(bits.Substring(i * 8, 8), 2)).ToArray();
    return bytes;
  }
}
