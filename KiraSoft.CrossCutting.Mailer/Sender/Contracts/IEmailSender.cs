using KiraSoft.CrossCutting.Mailer.Enum;
using KiraSoft.CrossCutting.Mailer.Message.Contract;

namespace KiraSoft.CrossCutting.Mailer.Sender.Contracts
{
    public interface IEmailSender
    {
        void SendEmail(IMailMessage message, MailSenderEnum sender, string configuration = null);
    }
}
