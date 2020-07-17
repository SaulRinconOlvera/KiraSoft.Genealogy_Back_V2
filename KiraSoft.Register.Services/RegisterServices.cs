using KiraSoft.Application.Adapters;
using KiraSoft.Application.Service.Register;
using KiraSoft.Infrastructure.Persistence.Configuration;
using KiraSoft.Infrastructure.Repositories;
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
        }
    }
}
