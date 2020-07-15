using Microsoft.Extensions.Configuration;

namespace KiraSoft.CrossCutting.Mailer.Register
{
    public static class MailerRegister
    {
        internal static  IConfiguration Configuration {  get; private set; }
        public static void Register(IConfiguration configuration) 
        {
            Configuration = configuration;
        }
    }
}
