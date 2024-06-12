namespace EmailSender;

public class AppSettings
{
    public EmailCredentials EmailCredentials { get; set; }
    public EmailDetails EmailDetails { get; set; }
}

public class EmailDetails
{
    public string? ToName { get; set; }
    public string? ToEmail { get; set; }
    public EmailContent EmailContent { get; set; }
}

public class EmailCredentials
{
    public string? OutgoingMailServer { get; set; }
    public int ServerPort { get; set; }
    public bool UseSsl { get; set; }
    public string? Username
    {
        get
        {
            return FromEmail;
        }
    }
    public string? AppSpecificPassword { get; set; }
    public string? FromName { get; set; }
    public string? FromEmail { get; set; }
}

public class EmailContent
{
    public string? Subject { get; set; }
    public string? Body { get; set; }
}