using Bogus;
using FluentAssertions;
using KiraSoft.Application.IdentityViewModel;
using KiraSoft.Infrastructure.IdentityRepository;
using KiraSoft.Infrastructure.Persistence.Contexts;
using KiraSoft.Infrastructure.RepositoryBase.Implementation;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace KiraSoft.Genealogy.WebAPI.Test.Areas.MasiveTests
{
    public class Masive_User_Register : Base_Controller_Test
    {
        private readonly string URL_USER_REGISTER = "/api/v1/UserRegister/UserRegister";
        private UserRepository _repository;
        
        public Masive_User_Register() : base() {
            _repository = new UserRepository(new UnitOfWork(new GenealogyContext(null)));
        }

        [Fact]
        private async Task Test_Test()
        {
            var list1 = Create_Fake_Data();
            var list2 = Create_Fake_Data();
            var list3 = Create_Fake_Data();

            Parallel.Invoke(
                () => Process_List(list1),
                () => Process_List(list2),
                () => Process_List(list3));

            var cuantos = await _repository.GetAllAsync();
            cuantos.Count().Should().Be(list1.Count() + list2.Count() + list3.Count() + 2);

            Assert.True(true);
        }

        private void Process_List(List<UserRegisterViewModel> list1)
        {
            foreach (var data in list1)
                Register_User(data);
        }

        private void Register_User(UserRegisterViewModel data)
        {
            var response = _client.PostAsJsonAsync(URL_USER_REGISTER, data).GetAwaiter().GetResult();

            if (response.StatusCode == HttpStatusCode.BadRequest ||
                response.StatusCode == HttpStatusCode.InternalServerError)
            {
                var result = response.Content.ReadAsStringAsync();
                Debugger.Break();
            }

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private List<UserRegisterViewModel> Create_Fake_Data()
        {
            var list = new List<UserRegisterViewModel>();
            int contador = 0;

            while (true) 
            {
                var userRegister =
                    new Faker<UserRegisterViewModel>("es_MX")
                    .RuleFor(x => x.Email, x => x.Person.Email)
                    .RuleFor(x => x.Name, x => x.Person.FirstName)
                    .RuleFor(x => x.FirstFamilyName, x => x.Person.LastName)
                    .RuleFor(x => x.SecondFamilyName, x => x.Person.LastName)
                    .RuleFor(x => x.Password, x => x.Internet.Password(10,false))
                    .Generate();

                userRegister.PassConfirmation =
                    userRegister.Password =
                    $"{userRegister.Password}Za0.";

                list.Add(userRegister);

                if (contador == 50) break;
                contador++;
            }
            return list;
        }
    }
}
