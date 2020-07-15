using KiraSoft.CrossCutting.Mailer.Message.Contract;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KiraSoft.CrossCutting.Mailer.Message.Base
{
    public abstract class MailMessage : IMailMessage
    {
        private const string _pattern = 
            @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|" + 
            @"[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|" + 
            @"[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*" + 
            @"(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])" + 
            @"([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|" +
            @"(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";

        private Regex _regex = new Regex(_pattern);


        public string From { get; internal protected set; }
        public IList<string> To { get; internal protected set; }
        public string Subject { get; internal protected set; }
        public string Message { get; internal protected set; }

        public string GetMessage()
        {
            ProcessMessage();
            return Message;
        }

        public MailMessage(string from , string to, string subject) 
        {
            ValidateParameters(from, to, subject);

            To = new List<string>();
            From = from; To.Add(to); Subject = subject;
        }

        public MailMessage(string from, List<string> to, string subject)
        {
            ValidateParameters(from, "to", subject);
            ValidateTo(to);
            From = from; To = to; Subject = subject;

            ValidaEmails();
        }

        private void ValidaEmails()
        {
            ValidateEmailForm(From);

            foreach (var email in To)
                ValidateEmailForm(email);
        }

        private void ValidateEmailForm(string email)
        {

            if (!_regex.Match(email).Success)
                throw new FormatException(email);
        }

        private void ValidateTo(List<string> to)
        {
            if (to is null || to.Count == 0)
                throw new ArgumentNullException("To");
        }

        private void ValidateParameters(string from, string to, string subject)
        {
            if (string.IsNullOrWhiteSpace(from))
                throw new ArgumentNullException("From");

            if (string.IsNullOrWhiteSpace(to))
                throw new ArgumentNullException("To");

            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentNullException("Subject");
        }

        internal protected virtual void ProcessMessage() { ValidateInformation(); }
        internal protected virtual void ValidateInformation() { }
    }
}
