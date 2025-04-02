using API_FEB.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace API_FEB.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendResetEmailAsync(string email, string token)
        {
            string subject = "Password Reset Request";
            string body = $"Use this token to reset your password: {token}";

            await SendEmailAsync(email, subject, body, false);
        }

        public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = true)
        {
            try
            {
                using (var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port))
                {
                    client.Credentials = new NetworkCredential(
                        _emailSettings.Username,  // ✅ Use `Username` instead of `SenderEmail`
                        _emailSettings.Password   // ✅ Use `Password` instead of `SenderPassword`

                    );

                    client.EnableSsl = _emailSettings.EnableSsl;  // ✅ Read from settings

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_emailSettings.SenderEmail),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = isHtml
                    };

                    mailMessage.To.Add(to);

                    await client.SendMailAsync(mailMessage);
                    Console.WriteLine($"✅ Email sent successfully to {to}");
                }
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"❌ SMTP Error: {smtpEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ General Error: {ex.Message}");
            }
        }
    }
}
