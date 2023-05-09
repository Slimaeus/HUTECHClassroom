namespace HUTECHClassroom.Domain.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string recipientEmail, string subject, string message);
}
