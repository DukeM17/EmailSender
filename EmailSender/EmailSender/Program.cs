using EmailSender;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        //TODOM: add appsettings
        services.AddTransient<IEmailSenderService, EmailSenderService>();
    })
    .Build();

var emailService = host.Services.GetService<IEmailSenderService>();
var message = emailService!.CreateMessage("m.duke401@gmail.com", "morin24792@noefa.com", "Test Email", "This is my test email to my temp email account");
var result = emailService.SendEmail(message);

if (result)
{
    Console.WriteLine("Email successfully sent");
}
else 
{
    Console.WriteLine("Email not sent");
}