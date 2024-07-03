using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace TheStore.Services;
public class EmailService {
  private readonly IConfiguration config;
  public EmailService(IConfiguration _config) {
    config = _config;
  }
  public void SendMail(string recipient, string subject, string content) {
    string Username = config.GetSection("MailSettings:UserName").Value!;
    string Passcode = config.GetSection("MailSettings:Password").Value!;
    string SmtpServer = config.GetSection("MailSettings:Server").Value!;
    int SmtpServerPort = int.Parse(config.GetSection("MailSettings:Port").Value!);

    MimeMessage Message = new();
    Message.From.Add(new MailboxAddress("The Store", "accounts@Ecommerce.com"));
    Message.To.Add(MailboxAddress.Parse(recipient));
    Message.Subject = subject;
    Message.Body = new TextPart(TextFormat.Html) { Text = content };

    var smtpClient = new SmtpClient();
    smtpClient.Connect(SmtpServer, SmtpServerPort, SecureSocketOptions.SslOnConnect);
    smtpClient.Authenticate(Username, Passcode);
    smtpClient.Send(Message);
    smtpClient.Disconnect(true);
  }
}
