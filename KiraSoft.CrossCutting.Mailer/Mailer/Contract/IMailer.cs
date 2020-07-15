using KiraSoft.CrossCutting.Mailer.Message.Contract;
using System.Threading.Tasks;

namespace KiraSoft.CrossCutting.Mailer.Mailer.Contract
{
    internal interface IMailer
    {
        void SendEmail(IMailMessage message);
        Task SendEmailAsync(IMailMessage message);
    }
}
