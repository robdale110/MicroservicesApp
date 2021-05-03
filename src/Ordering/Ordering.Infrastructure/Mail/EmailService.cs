using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);
            var response = await client.SendEmailAsync
            (
                MailHelper.CreateSingleEmail(
                    new EmailAddress
                    {
                        Email = _emailSettings.FromAddress,
                        Name = _emailSettings.FromName
                    },
                    new EmailAddress(email.To),
                    email.Subject,
                    email.Body,
                    email.Body));

            if (response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.OK)
            {
                _logger.LogInformation("Email sent.");
                return true;
            }

            _logger.LogError("Email sending failed.");

            return false;
        }
    }
}
