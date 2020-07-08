using KiraSoft.Domain.Model.Identity;
using KiraSoft.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KiraSoft.Infrastructure.Persistence.Configuration
{
    public static class DataBaseConfiguration
    {
        public static void Register(IServiceCollection services)
        {
            services.AddDbContext<GenealogyContext>();
            services.AddIdentity<User, Role>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 8;
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                })
                .AddEntityFrameworkStores<GenealogyContext>()
                .AddDefaultTokenProviders();

            UserManager = services.BuildServiceProvider().GetService<UserManager<User>>();
            SignManager = services.BuildServiceProvider().GetService<SignInManager<User>>();
            RoleManager = services.BuildServiceProvider().GetService<RoleManager<Role>>();
        }

        public static UserManager<User> UserManager { get; private set; }
        public static SignInManager<User> SignManager { get; private set; }
        public static RoleManager<Role> RoleManager { get; private set; }
    }
}
