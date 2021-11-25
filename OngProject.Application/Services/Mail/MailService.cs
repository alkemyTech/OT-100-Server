using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OngProject.Application.DTOs.Mails;
using OngProject.Application.Helpers.Mail;
using OngProject.Application.Interfaces.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace OngProject.Application.Services.Mail
{
    public class MailService : IMailService
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly ILogger _logger;
        private readonly MailConfiguration _mail;

        public MailService(ISendGridClient sendGridClient, IOptionsMonitor<MailConfiguration> options,
            ILoggerFactory loggerFactory)
        {
            _sendGridClient = sendGridClient;
            _logger = loggerFactory.CreateLogger("logs");;
            _mail = options.CurrentValue;
        }
        
        public async Task SendMail(SendMailDto sendmail)
        {
            try
            {
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(_mail.SendGridMail, _mail.SendGridUser),
                    Subject = sendmail.Subject
                };

                msg.AddContent(MimeType.Html, sendmail.Text);
                msg.AddTo(new EmailAddress(sendmail.EmailTo, sendmail.Name));
                await _sendGridClient.SendEmailAsync(msg).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}