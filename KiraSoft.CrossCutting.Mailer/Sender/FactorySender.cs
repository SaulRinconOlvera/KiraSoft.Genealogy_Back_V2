using KiraSoft.CrossCutting.Mailer.Enum;
using KiraSoft.CrossCutting.Mailer.Mailer;
using KiraSoft.CrossCutting.Mailer.Mailer.Contract;
using KiraSoft.CrossCutting.Mailer.Mailer.Model;
using KiraSoft.CrossCutting.Mailer.Message.Contract;
using KiraSoft.CrossCutting.Mailer.Register;
using Microsoft.Extensions.Configuration;
using System;

namespace KiraSoft.CrossCutting.Mailer.Sender
{
    public static class FactorySender
    {
        public static void SendEmail(
            IMailMessage message, 
            MailSenderEnum sender, 
            string configuration = null) 
        {
            IMailer mailer = GetMailer(sender, configuration);

            mailer.SendEmail(message);
        }

        private static IMailer GetMailer(
            MailSenderEnum sender, 
            string configuration) => new SmtpMailer(GetConfiguration(configuration));
            
        private static SmtpConfigurationViewModel GetConfiguration(string configuration)
        {
            ValidateConfiguration(configuration);

            return new SmtpConfigurationViewModel()
            {
                EnableSsl = GetValue<bool>($"{configuration}:EnableSsl"),
                Host = GetValue<string>($"{configuration}:Host"),
                Port = GetValue<int>($"{configuration}:Port"),
                User = GetValue<string>($"{configuration}:User"),
                Password = GetValue<string>($"{configuration}:Password")
            };
        }

        private static T GetValue<T>(string key) =>
            MailerRegister.Configuration.GetValue<T>($"SmtpConfiguration:{key}");

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
