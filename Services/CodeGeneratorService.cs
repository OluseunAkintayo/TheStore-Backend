using System.Security.Cryptography;

public class CodeGeneratorService {
  private readonly int OtpLength = 8;
  private const int TimeStep = 60; // time in seconds before otp is changed
  private const string Base32Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
  private readonly IConfiguration config;
  public CodeGeneratorService(IConfiguration _config) {
    config = _config;
  }
  public string GetOTP() {
    string SecretKey = config.GetConnectionString("SecretKey")!;
    long timeCounter = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / TimeStep;
    byte[] counterBytes = BitConverter.GetBytes(timeCounter);
    byte[] secretKeyBytes = ConvertToBytes(SecretKey);
    var hmac = new HMACSHA1(secretKeyBytes);
    byte[] hash = hmac.ComputeHash(counterBytes);
    int offset = hash[hash.Length - 1] & 0x0F;
    int binary = ((hash[offset] & 0x7f) << 24) | (hash[offset + 1] << 16) | (hash[offset + 2] << 8) | hash[offset + 3];
    int otp = binary % (int)Math.Pow(10, OtpLength);
    string otpString = otp.ToString().PadLeft(OtpLength, '0');
    return otpString;
  }

  private static byte[] ConvertToBytes(string input) {
    var bits = string.Join(string.Empty, input.Select(c => Convert.ToString(Base32Alphabet.IndexOf(c), 2).PadLeft(5, '0')));
    var bytes = Enumerable.Range(0, bits.Length / 8).Select(i => Convert.ToByte(bits.Substring(i * 8, 8), 2)).ToArray();
    return bytes;
  }
}
