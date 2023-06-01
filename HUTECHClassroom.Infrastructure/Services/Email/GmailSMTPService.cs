using HUTECHClassroom.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Serilog;
using System.Net;
using System.Net.Mail;

namespace HUTECHClassroom.Infrastructure.Services.Email;

public class GmailSMTPService : IEmailService
{
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;

    public GmailSMTPService(IOptions<GmailSMTPSettings> options)
    {
        _smtpHost = options.Value.Host;
        _smtpPort = options.Value.Port;
        _smtpUsername = options.Value.UserName;
        _smtpPassword = options.Value.Password;
    }

    public async Task SendEmailAsync(string recipientEmail, string subject, string message)
    {
        try
        {
            var smtpClient = new SmtpClient(_smtpHost, _smtpPort)
            {
                Host = _smtpHost,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpUsername),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(recipientEmail);

            await smtpClient.SendMailAsync(mailMessage);
            Log.Information("Email sent successfully to: {RecipentEmail}", recipientEmail);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while sending an email to: {RecipientEmail}", recipientEmail);
            throw;
        }

    }
}
