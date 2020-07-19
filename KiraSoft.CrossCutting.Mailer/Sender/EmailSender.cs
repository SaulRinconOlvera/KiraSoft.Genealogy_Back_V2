using KiraSoft.CrossCutting.Mailer.Enum;
using KiraSoft.CrossCutting.Mailer.Message.Contract;
using KiraSoft.CrossCutting.Mailer.Sender.Contracts;

namespace KiraSoft.CrossCutting.Mailer.Sender
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(IMailMessage message, MailSenderEnum sender, string configuration = null) =>
            FactorySender.SendEmail(message, sender, configuration);
       
    }
}
