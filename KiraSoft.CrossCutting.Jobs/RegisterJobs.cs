using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KiraSoft.CrossCutting.Jobs
{
    public class RegisterJobs
    {
        public static void Register(IServiceCollection services, IConfiguration configuration) 
        {
            services.AddHangfire(options => { options
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(
                    configuration["ConnectionStrings:HangFire"],
                    new SqlServerStorageOptions 
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    });
            });

            services.AddHangfireServer();
        }

        public static void AddDashboardUI(IApplicationBuilder app)  =>
            app.UseHangfireDashboard();
    }
}
