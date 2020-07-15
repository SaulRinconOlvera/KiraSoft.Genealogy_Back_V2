using KiraSoft.CrossCutting.Mailer.Mailer.Contract;
using KiraSoft.CrossCutting.Mailer.Mailer.Model;
using KiraSoft.CrossCutting.Mailer.Message.Contract;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace KiraSoft.CrossCutting.Mailer.Mailer
{
    internal class SmtpMailer : IMailer
    {
        private SmtpClient _server;
        private IMailMessage _message;

        internal SmtpMailer(SmtpConfigurationViewModel viewModel) 
        {
            _server = GetServer(viewModel);
        }

        private SmtpClient GetServer(SmtpConfigurationViewModel viewModel)
        {
            return new SmtpClient(viewModel.Host, viewModel.Port)
            {
                EnableSsl = viewModel.EnableSsl,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(viewModel.User, viewModel.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 20000
            };
        }

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
    }
}
