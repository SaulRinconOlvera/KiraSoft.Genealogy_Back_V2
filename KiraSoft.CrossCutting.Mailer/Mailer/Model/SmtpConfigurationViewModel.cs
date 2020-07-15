namespace KiraSoft.CrossCutting.Mailer.Mailer.Model
{
    public class SmtpConfigurationViewModel
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}
