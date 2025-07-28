using MailKit.Net.Smtp;
using MimeKit;

namespace RefugioMimi.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendConfirmationAsync(string toEmail, string subject, string message)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config["Email:From"]));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_config["Email:SmtpHost"], int.Parse(_config["Email:SmtpPort"]), false);
        await smtp.AuthenticateAsync(_config["Email:User"], _config["Email:Password"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
