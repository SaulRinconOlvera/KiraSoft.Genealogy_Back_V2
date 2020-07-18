using KiraSoft.Genealogy.Web.API;
using KiraSoft.Infrastructure.Persistence.Configuration;
using KiraSoft.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;

namespace KiraSoft.Genealogy.WebAPI.Test.Areas
{
    public class Base_Controller_Test
    {
        protected readonly HttpClient _client;

        private DbContextOptionsBuilder<GenealogyContext> GetDbContextForTests() =>
                new DbContextOptionsBuilder<GenealogyContext>()
                    .UseInMemoryDatabase("GenealogyDB_Tests");

        public Base_Controller_Test()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(b =>
                {
                    b.ConfigureServices(s =>
                    {
                        s.RemoveAll(typeof(GenealogyContext));
                        s.AddDbContext<GenealogyContext>(o => GetDbContextForTests());
                    });
                    b.UseUrls("https://localhost:44391/");
                });

            DataBaseConfiguration.SetOptionsBuilder(GetDbContextForTests());
            _client = appFactory.CreateClient();
        }
    }
}
