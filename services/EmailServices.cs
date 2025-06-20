using Arna_Project_Track.Models;
using Arna_Project_Track.services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Arna_Project_Track.Services  
{
    public class EmailServices : IEmail
    {
        private readonly IOptions<SmtpSettings> _smtpsettings;

        public EmailServices(IOptions<SmtpSettings> options)
        {
            _smtpsettings = options;
        }

        public async Task SendEmailAsync(EmailModel emailModel)
        {
            using var client = new SmtpClient(_smtpsettings.Value.Host, _smtpsettings.Value.Port)
            {
                Credentials = new NetworkCredential(_smtpsettings.Value.Username, _smtpsettings.Value.Password),
                EnableSsl = _smtpsettings.Value.EnableSsl
            };

            var mail = new MailMessage(_smtpsettings.Value.Username, emailModel.ToEmail, emailModel.Subject, emailModel.Body)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(mail);
        }
    }
}
