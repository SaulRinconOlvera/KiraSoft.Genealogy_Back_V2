using KiraSoft.CrossCutting.Mailer.Sender;
using KiraSoft.CrossCutting.Mailer.Sender.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KiraSoft.CrossCutting.Mailer.Register
{
    public static class MailerRegister
    {
        internal static  IConfiguration Configuration {  get; private set; }
        public static void Register(IServiceCollection services, IConfiguration configuration) 
        {
            Configuration = configuration;
            services.AddTransient<IEmailSender, EmailSender>();
        }
    }
}
