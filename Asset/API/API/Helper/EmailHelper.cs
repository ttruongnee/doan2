using MailKit.Net.Smtp;
using MimeKit;

public class EmailHelper
{
    private const string FromEmail = "chuongg.utehy@gmail.com";
    private const string FromEmailPassword = "rmra cwrc odrx ksue";

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("Admin", FromEmail));
        email.To.Add(new MailboxAddress("", toEmail));
        email.Subject = subject;

        email.Body = new TextPart("plain")
        {
            Text = body
        };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(FromEmail, FromEmailPassword);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
