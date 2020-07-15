using System.Collections.Generic;

namespace KiraSoft.CrossCutting.Mailer.Message.Contract
{
    public interface IMailMessage
    {
        string From { get; }
        IList<string> To { get; }
        string Subject { get; }
        string Message { get; }
        string GetMessage();
    }
}
