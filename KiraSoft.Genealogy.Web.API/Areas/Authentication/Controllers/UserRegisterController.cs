using KiraSoft.Application.IdentityViewModel;
using KiraSoft.Application.Services.Indentity.Contracts;
using KiraSoft.CrossCutting.Mailer.Enum;
using KiraSoft.CrossCutting.Mailer.Message;
using KiraSoft.CrossCutting.Mailer.Sender;
using KiraSoft.CrossCutting.Operation.Executor;
using KiraSoft.Genealogy.Web.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace KiraSoft.Genealogy.Web.API.Areas.Authentication.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class UserRegisterController : BaseController
    {
        private readonly IUserRegisterAppService _service;
        private readonly IConfiguration _configuration;

        public UserRegisterController(
            IConfiguration configuration,
            ILogger<UserRegisterController> logger,
            IUserRegisterAppService service) : base(logger) 
        {
            _service = service;
            _configuration = configuration;
        }
            

        [HttpPost]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            async Task<UserViewModel> predicate() =>
                await _service.ReguisterAsync(viewModel);

            var result = await SafeExecutor<UserViewModel>.ExecAsync(predicate, _logger);
            return ProcessResponse(result);

        }

        [HttpPost]
        [ActionName("SendEmail")]
        public  IActionResult SendEmail()
        {
            var message = new UserRegisterMessage("portafolio.saulrincon@gmail.com", "saulrincon@hotmail.com", "Test")
            {
                UserName = "Saúl Rincón Olvera",
                ConfirmationLink = "https://www.eluniversal.com.mx/"
            };

            FactorySender.SendEmail(message, MailSenderEnum.SMTP, "Gmail_Test1");

            return Ok();
        }
    }
}
