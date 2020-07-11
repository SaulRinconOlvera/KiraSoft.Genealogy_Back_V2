using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.MsSqlServer.Destructurers;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using System;
using System.IO;

namespace KiraSoft.Genealogy.Web.API
{
    public class Program
    {
        public static ILogger Logger { get; private set; }

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddUserSecrets("f833047b-e209-421a-84c8-357472062289")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .Build();

        public static int Main(string[] args)
        {
            //Logger = Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            //    .WriteTo
            //    .MSSqlServer(
            //        connectionString: Configuration["ConnectionStrings:Genealogy"],
            //        sinkOptions: new SinkOptions { TableName = "Genealogy_DataLog", AutoCreateSqlTable = true })
            //    .CreateLogger();

            Logger = Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .Enrich.WithExceptionDetails()
               .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                                            .WithDefaultDestructurers()
                                            .WithDestructurers(new[] { new SqlExceptionDestructurer() }))
               .ReadFrom.Configuration(Configuration)
               .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
               .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception} {Properties:j}")
               .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog() // <-- Add this line
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
