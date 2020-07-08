using KiraSoft.Application.Service.Identity;
using KiraSoft.Application.Services.Indentity.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace KiraSoft.Application.Service.Register
{
    public static class ApplicationServices
    {
        public static void Register(IServiceCollection services)
        {
            //  Authorization
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<IUserClaimAppService, UserClaimAppService>();
            services.AddTransient<IRoleAppService, RoleAppService>();
            services.AddTransient<IUserRoleAppService, UserRoleAppService>();
            services.AddTransient<IUserLoginAppServicce, UserLoginAppService>();
        }
    }
}
