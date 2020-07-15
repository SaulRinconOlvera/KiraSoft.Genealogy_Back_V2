using KiraSoft.CrossCutting.Mailer.Mailer.Contract;
using KiraSoft.CrossCutting.Mailer.Message.Contract;
using KiraSoft.CrossCutting.Mailer.Register;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace KiraSoft.CrossCutting.Mailer.Mailer
{
    internal class SmtpMailer : IMailer
    {
        private SmtpClient _server;
        private IMailMessage _message;

        internal SmtpMailer(string configuration)
        {
            ValidateConfiguration(configuration);
            _server = GetServer(configuration);
        }

        private SmtpClient GetServer(string configuration)
        {
            return new SmtpClient(
                GetValue<string>($"{configuration}:Host"),
                GetValue<int>($"{configuration}:Port"))
            {
                EnableSsl = GetValue<bool>($"{configuration}:EnableSsl"),
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                    GetValue<string>($"{configuration}:User"),
                     GetValue<string>($"{configuration}:Password")),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 20000
            };
        }

        private static T GetValue<T>(string key) =>
            MailerRegister.Configuration.GetValue<T>($"SmtpConfiguration:{key}");

        public void SendEmail(IMailMessage message)
        {
            _message = message;
            var x = _message.GetMessage();
            _server.Send(GetMailMessage());
        }

        public async Task SendEmailAsync(IMailMessage message)
        {
            _message = message;
            await _server.SendMailAsync(GetMailMessage());
        }

        private MailMessage GetMailMessage() =>
            new MailMessage(_message.From, GetTo(), _message.Subject, _message.GetMessage()) { IsBodyHtml = true };

        private string GetTo() =>
            string.Join(";", _message.To);

        private static void ValidateConfiguration(string configuration)
        {
            if (string.IsNullOrWhiteSpace(configuration))
                throw new ArgumentNullException("ConfigurationName");

            if (MailerRegister.Configuration is null)
                throw new NullReferenceException("IConfiguration");

            var section = MailerRegister.Configuration.GetSection($"SmtpConfiguration:{configuration}");
            if (section is null)
                throw new NullReferenceException(configuration);
        }
    }
}
