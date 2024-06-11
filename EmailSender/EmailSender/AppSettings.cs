﻿namespace EmailSender;

public class AppSettings
{
    public string? Username { get; set; }
    public string? AppSpecificPassword { get; set; }
    public string? FromName { get; set; }
    public string? FromEmail { get; set; }
    public string? ToName { get; set; }
    public string? ToEmail { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
}
