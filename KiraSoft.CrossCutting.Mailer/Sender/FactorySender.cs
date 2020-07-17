using KiraSoft.CrossCutting.Mailer.Enum;
using KiraSoft.CrossCutting.Mailer.Mailer;
using KiraSoft.CrossCutting.Mailer.Mailer.Contract;
using KiraSoft.CrossCutting.Mailer.Message.Contract;

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
            string configuration) => new SmtpMailer(configuration);
    }
}
