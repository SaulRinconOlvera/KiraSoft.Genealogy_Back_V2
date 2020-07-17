using KiraSoft.Domain.Model.Identity;
using KiraSoft.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace KiraSoft.Infrastructure.Persistence.Configuration
{
    public static class DataBaseConfiguration
    {

        private static DbContextOptionsBuilder<GenealogyContext> _contextOptions;
        public static DbContextOptionsBuilder<GenealogyContext> 
            GetOptionsBuilder(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            if (_contextOptions is null) 
            {
                _contextOptions = new DbContextOptionsBuilder<GenealogyContext>()
                            .UseSqlServer(configuration["ConnectionStrings:Genealogy"]);

                if (configuration.GetValue<bool>("Application:EnableDatabaseLogging"))
                    _contextOptions.UseLoggerFactory(loggerFactory).EnableSensitiveDataLogging(true);
            }
            return _contextOptions;
        }

        private static IdentityOptions GetIdentityConfiguration()
        {
            return new IdentityOptions()
            {
                Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = true,
                    RequireUppercase = true,
                    RequiredLength = 8
                },
                Lockout = new LockoutOptions
                {
                    MaxFailedAccessAttempts = 5,
                    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15)
                },
                SignIn = new SignInOptions
                {
                    RequireConfirmedEmail = true,
                    RequireConfirmedAccount = true
                },
                User = new UserOptions
                {
                    RequireUniqueEmail = true
                }
            };
        }

        public static void Register(IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            services.AddDbContext<GenealogyContext>(o => GetOptionsBuilder(configuration, loggerFactory))
                .AddIdentity<User, Role>(o => GetIdentityConfiguration())
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
