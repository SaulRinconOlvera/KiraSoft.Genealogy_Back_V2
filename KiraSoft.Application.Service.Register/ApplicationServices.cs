using KiraSoft.Application.IdentityViewModel;
using KiraSoft.Application.MapperBase;
using KiraSoft.Application.Service.Identity;
using KiraSoft.Application.Services.Indentity.Contracts;
using KiraSoft.Domain.Model.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

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

            services.AddTransient<IGenericMapper<User, UserViewModel, Guid>, GenericMapper<User, UserViewModel, Guid>>();
            services.AddTransient<IGenericMapper<UserRole, UserRoleViewModel, Guid>, GenericMapper<UserRole, UserRoleViewModel, Guid>>();
            services.AddTransient<IGenericMapper<UserClaim, UserClaimViewModel, int>, GenericMapper<UserClaim, UserClaimViewModel, int>>();
            services.AddTransient<IGenericMapper<Role, RoleViewModel, Guid>, GenericMapper<Role, RoleViewModel, Guid>>();

        }
    }
}
