using KiraSoft.CrossCutting.Mailer.Register;
using KiraSoft.Register.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace KiraSoft.Genealogy.Web.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(AddSwagger());
            services.AddLogging(o => o.AddSerilog(Program.Logger));

            Utilities.Token.Configure.ConfigureJWT(services, Configuration);
            RegisterServices.Register(services, Configuration);
            MailerRegister.Register(Configuration);
        }

        private Action<SwaggerGenOptions> AddSwagger()
        {
            return (SwaggerGenOptions o) => 
            {
                o.SwaggerDoc("v1", new OpenApiInfo { Title = "Genealogy API", Version = "v1" });
            };
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobFinder API");
            });


            loggerFactory.AddSerilog();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
