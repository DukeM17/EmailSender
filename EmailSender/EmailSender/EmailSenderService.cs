using MimeKit;

namespace EmailSender;

public interface IEmailSenderService
{
    MimeMessage CreateMessage(string fromName, string fromEmail, string toName, string toEmail, string subject, string body);
    bool SendEmail(MimeMessage message, string server = "smtp.gmail.com");
}

public class EmailSenderService : IEmailSenderService
{
    private readonly AppSettings _appSettings;

    public EmailSenderService(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    public bool SendEmail(MimeMessage message, string server = "smtp.gmail.com")
    {
        try
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(server, _appSettings.EmailCredentials.ServerPort, _appSettings.EmailCredentials.UseSsl);

                client.Authenticate(_appSettings.EmailCredentials.FromEmail, _appSettings.EmailCredentials.AppSpecificPassword);

                client.Send(message);
                client.Disconnect(true);
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public MimeMessage CreateMessage(string fromName, string fromEmail, string toName, string toEmail, string subject, string body)
    {
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress(fromName, fromEmail));
        message.To.Add(new MailboxAddress(toName, toEmail));
        message.Subject = subject;
        message.Body = new TextPart("plain")
        {
            Text = body
        };

        return message;
    }
}