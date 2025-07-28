namespace RefugioMimi.Services;

public interface IEmailService
{
    Task SendConfirmationAsync(string toEmail, string subject, string message);
}
