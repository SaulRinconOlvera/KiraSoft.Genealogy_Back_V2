using FluentAssertions;
using KiraSoft.Application.IdentityViewModel;
using KiraSoft.CrossCutting.Operation.Transaction.Implementation;
using KiraSoft.Genealogy.WebAPI.Test.Areas.UserRegisterController.Data;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace KiraSoft.Genealogy.WebAPI.Test.Areas.UserRegisterController
{
    public class UserRegisterController_Test : Base_Controller_Test
    {
        private UserRegisterViewModel _viewModel = Data_Mock.UserRegister_ViewModel_Ok();
        private string _confirmationLink;
        private Guid _userId;

        //------------------------------------------------------------------
        //              CONSTANTS
        //------------------------------------------------------------------
        private readonly string URL_USER_REGISTER = "/api/v1/UserRegister/UserRegister";
        private readonly string URL_EMAIL_CONFIRMATION = "/api/v1/UserRegister/EmailConfirmation";
        private readonly string USER_ALREADY_EXIST = "The user already exists.";
        private readonly string EMAIL_ALREADY_CONFIRMED = "Email already confirmed.";
        private readonly string EMAIL_INVALID_TOKEN = "Code:InvalidToken";
        //------------------------------------------------------------------

        public UserRegisterController_Test() : base()
        { }

        [Fact]
        public async Task User_Register_Use_Case()
        {
            await UserRegister_Ok();
            await User_Register_AlreadyExists();
            await User_Register_BadModel();
            await User_Register_Incorrect_Password_Structure();
            await Email_Confirmation_ConfirmationLink_Unknow();
            await Email_Confirmation_Ok();
            await Email_Already_Confirmed();
        }

        private async Task Email_Confirmation_ConfirmationLink_Unknow()
        {
            var response = await _client.GetAsync($"{URL_EMAIL_CONFIRMATION}?userId={_userId}&token=ABCDEFGHYJK131232123123");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var result = await response.Content.ReadAsAsync<ErrorAnswer<string>>();
            result.Success.Should().BeFalse();
            result.ErrorList[0].Message.Should().Contain(EMAIL_INVALID_TOKEN);
        }

        private async Task Email_Already_Confirmed()
        {
            var response = await _client.GetAsync($"{URL_EMAIL_CONFIRMATION}?userId={_userId}&token={_confirmationLink}");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var result = await response.Content.ReadAsAsync<ErrorAnswer<string>>();
            result.Success.Should().BeFalse();
            result.ErrorList[0].Message.Should().Be(EMAIL_ALREADY_CONFIRMED);
        }

        private async Task Email_Confirmation_Ok()
        {
            var response = await _client.GetAsync($"{URL_EMAIL_CONFIRMATION}?userId={_userId}&token={_confirmationLink}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private async Task User_Register_Incorrect_Password_Structure()
        {
            _viewModel.Email = "saulrincon@hotmail123.com";
            _viewModel.Password = "passwordsito";
            var response = await _client.PostAsJsonAsync(URL_USER_REGISTER, _viewModel);
            
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        private async Task User_Register_BadModel()
        {
            _viewModel.Email = "";
            var response = await _client.PostAsJsonAsync(URL_USER_REGISTER, _viewModel);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        private async Task UserRegister_Ok()
        {
            var response = await _client.PostAsJsonAsync(URL_USER_REGISTER, _viewModel);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadAsAsync<SuccessfullyAnswer<UserViewModel>>();
            _confirmationLink = result.PayLoad.ConfirmationLink;
            _userId = result.PayLoad.Id;
        }

        private async Task User_Register_AlreadyExists()
        {
            var response = await _client.PostAsJsonAsync(URL_USER_REGISTER, _viewModel);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var result = await response.Content.ReadAsAsync<ErrorAnswer<UserViewModel>>();
            result.Success.Should().BeFalse();
            result.ErrorList[0].Message.Should().Be(USER_ALREADY_EXIST);
        }
    }
}
