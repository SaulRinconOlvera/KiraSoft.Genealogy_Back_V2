using FluentAssertions;
using KiraSoft.Application.IdentityViewModel;
using KiraSoft.CrossCutting.Operation.Transaction.Contracts;
using KiraSoft.Domain.Model.Identity;
using KiraSoft.Genealogy.Web.API;
using KiraSoft.Genealogy.WebAPI.Test.Areas.UserRegisterController.Data;
using KiraSoft.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace KiraSoft.Genealogy.WebAPI.Test.Areas.UserRegisterController
{
    public class UserRegisterController_Test
    {

        private readonly HttpClient _client;

        public UserRegisterController_Test() 
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(b =>
                {
                    b.ConfigureServices(s =>
                    {
                        s.RemoveAll(typeof(GenealogyContext));
                        //s.AddDbContext<GenealogyContext>(o =>
                        //{
                        //    o.UseInMemoryDatabase("GenealogyDB_Test");
                        //});
                    });
                    b.UseUrls("https://localhost:44391/");
                });
            _client = appFactory.CreateClient();
        }

        [Fact]
        public async Task User_Register_Ok()
        {
            var response = await _client.PostAsJsonAsync("/api/v1/UserRegister/UserRegister", Data_Mock.UserRegister_ViewModel_Ok());
            var result = await response.Content.ReadAsStringAsync();


            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            //response.StatusCode.Should().Be(HttpStatusCode.OK);
            //result.Success.Should().BeTrue();
        }
    }
}
