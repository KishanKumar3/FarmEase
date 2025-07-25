using FarmEase.Application.Services.Interface;
using FarmEase.Domain.Helper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Shared.Exceptions;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace FarmEase.Application.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly MailSetting _mailSettings;
        private readonly ILogger<EmailService> _logger;
        public EmailService(IOptions<MailSetting> mailSettings, ILogger<EmailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }
        public async Task<string> SendEmailAsync(string toEmail, string subject, string message)
        {
            _logger.LogInformation($"{nameof(EmailService)}.{nameof(SendEmailAsync)}: begin");
            if (string.IsNullOrEmpty(toEmail) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(message))
            {
                _logger.LogWarning($"{nameof(EmailService)}.{nameof(SendEmailAsync)}: Email, subject, or message is null or empty.");
                throw new ArgumentException("Email, subject, and message must not be empty.");
            }
            try
            {
                var email = new MimeMessage()
                {
                    Subject = subject,
                    From = { new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email)},
                    To = { MailboxAddress.Parse(toEmail) },
                    Body = new TextPart(MimeKit.Text.TextFormat.Html)
                    {
                        Text = message
                    }
                };
                _logger.LogInformation("{Service}.{Method} - Connecting to SMTP server {Host}:{Port}.", nameof(EmailService), nameof(SendEmailAsync), _mailSettings.Host, _mailSettings.Port);
                using var smtpClient = new SmtpClient();
                await smtpClient.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(_mailSettings.Email, _mailSettings.Password);
                await smtpClient.SendAsync(email);
                await smtpClient.DisconnectAsync(true);
                _logger.LogInformation("{Service}.{Method} - Email successfully sent to {ToEmail}.", nameof(EmailService), nameof(SendEmailAsync), toEmail);
                return String.Format(Constants.SuccessMessages.MailSent, toEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Service}.{Method} - Failed to send email to {ToEmail}: {Error}", nameof(EmailService), nameof(SendEmailAsync), toEmail, ex.Message);
                throw new CustomException(string.Join(Constants.Separator.Colon, Constants.ErrorMessages.MailFailed, ex.Message));
            }
        }
    }
}
