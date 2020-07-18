using FluentAssertions;
using KiraSoft.Application.IdentityViewModel;
using KiraSoft.CrossCutting.Operation.Transaction.Implementation;
using KiraSoft.Genealogy.Web.API.Areas.Authentication.Models;
using KiraSoft.Genealogy.WebAPI.Test.Areas.UserRegisterController.Data;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace KiraSoft.Genealogy.WebAPI.Test.Areas.UserRegisterController
{
    public class T_02_UserLoginController_Test : Base_Controller_Test
    {
        //------------------------------------------------------------------
        //              CONSTANTS
        //------------------------------------------------------------------
        private readonly string URL_USER_LOGIN = "/api/v1/Authentication/Login";
        private readonly string USER_OR_PASS_ARE_WRONG = "User or password are wrong";
        private readonly string URL_USER_REGISTER = "/api/v1/UserRegister/UserRegister";
        private readonly string URL_EMAIL_CONFIRMATION = "/api/v1/UserRegister/EmailConfirmation";
        //------------------------------------------------------------------

        private UserRegisterViewModel _viewModel = Data_Mock.UserRegister_ViewModel_Ok_1();
        private object _confirmationLink;
        private object _userId;

        public T_02_UserLoginController_Test() : base() { }

        [Fact]
        public async Task User_Login_Use_Case() 
        {
            await User_Doesnt_Exists();
            await UserRegister_Ok();
            await Email_Confirmation_Ok();
            await User_Password_Are_Wrong();
            await User_Login_Successfully();
        }

        private async Task User_Login_Successfully()
        {
            var loginViewModel =
                new LoginViewModel { UserName = _viewModel.Email, Password = _viewModel.Password };

            var response = await _client.PostAsJsonAsync(URL_USER_LOGIN, loginViewModel);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private async Task User_Password_Are_Wrong()
        {
            var loginViewModel =
                new LoginViewModel { UserName = _viewModel.Email, Password = "BadPassword" };

            var response = await _client.PostAsJsonAsync(URL_USER_LOGIN, loginViewModel);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var result = await response.Content.ReadAsAsync<ErrorAnswer<string>>();
            result.ErrorList[0].Message.Should().Be(USER_OR_PASS_ARE_WRONG);
        }

        private async Task User_Doesnt_Exists()
        {
            var loginViewModel =
                new LoginViewModel { UserName = "saulrincon@asdasd.com", Password = "passwordsito" };

            var response = await _client.PostAsJsonAsync(URL_USER_LOGIN, loginViewModel);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var result = await response.Content.ReadAsAsync<ErrorAnswer<string>>();
            result.ErrorList[0].Message.Should().Be(USER_OR_PASS_ARE_WRONG);
        }

        private async Task UserRegister_Ok()
        {
            var response = await _client.PostAsJsonAsync(URL_USER_REGISTER, _viewModel);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadAsAsync<SuccessfullyAnswer<UserViewModel>>();
            _confirmationLink = result.PayLoad.ConfirmationLink;
            _userId = result.PayLoad.Id;
        }

        private async Task Email_Confirmation_Ok()
        {
            var response = await _client.GetAsync($"{URL_EMAIL_CONFIRMATION}?userId={_userId}&token={_confirmationLink}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
