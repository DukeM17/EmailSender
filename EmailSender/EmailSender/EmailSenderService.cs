using MimeKit;

namespace EmailSender;

public interface IEmailSenderService
{
    MimeMessage CreateMessage(string fromEmail, string toEmail, string subject, string body);
    bool SendEmail(MimeMessage message, string server = "smtp.gmail.com");
}

public class EmailSenderService : IEmailSenderService
{
    public EmailSenderService()
    {
    }

    public bool SendEmail(MimeMessage message, string server = "smtp.gmail.com")
    {
        try
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(server, 587, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("m.duke401@gmail.com", "ubuf ezth nckq jcbw\r\n");

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

    public MimeMessage CreateMessage(string fromEmail, string toEmail, string subject, string body)
    {
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress("Joey Tribbiani", fromEmail));
        message.To.Add(new MailboxAddress("ToMailName", toEmail));
        message.Subject = subject;
        message.Body = new TextPart("plain")
        {
            Text = body
        };

        return message;
    }
}