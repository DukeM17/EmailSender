using EmailSender;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        // AppSettings
        var configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(new DirectoryInfo(Environment.CurrentDirectory).Parent!.Parent!.Parent!.FullName)
        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);

        var configuration = configurationBuilder.Build();

        var appSettings = new AppSettings();
        configuration.Bind(appSettings);
        services.AddSingleton(appSettings);

        // EmailSender service
        services.AddTransient<IEmailSenderService, EmailSenderService>();
    })
    .Build();

var appSettings = host.Services.GetService<AppSettings>();
var emailService = host.Services.GetService<IEmailSenderService>();
var message = emailService!.CreateMessage(appSettings!.EmailCredentials.FromName!, appSettings!.EmailCredentials.FromEmail!, appSettings.EmailDetails.ToName!, appSettings.EmailDetails.ToEmail!, appSettings.EmailDetails.EmailContent.Subject!, appSettings.EmailDetails.EmailContent.Body!);
var result = emailService.SendEmail(message, appSettings.EmailCredentials.OutgoingMailServer!);

var finalResult = result ? "Email successfully sent" : "Email not sent";
Console.WriteLine(finalResult);