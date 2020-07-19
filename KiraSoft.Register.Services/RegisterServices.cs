using KiraSoft.Application.Adapters;
using KiraSoft.Application.Service.Register;
using KiraSoft.CrossCutting.Jobs;
using KiraSoft.CrossCutting.Mailer.Register;
using KiraSoft.Infrastructure.Persistence.Configuration;
using KiraSoft.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KiraSoft.Register.Services
{
    public static class RegisterServices
    {

        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            MapperServices.Register(services);
            ApplicationServices.Register(services);
            DataBaseConfiguration.Register(services, configuration, null);
            RegisterRepositories.Register(services, configuration);
            RegisterJobs.Register(services, configuration);
            MailerRegister.Register(services, configuration);
        }

        public static void AddJobsDashboardUI(IApplicationBuilder app) =>
            RegisterJobs.AddDashboardUI(app);
    }
}
