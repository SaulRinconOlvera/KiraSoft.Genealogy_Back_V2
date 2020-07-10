using KiraSoft.Application.Base.ViewModel;
using KiraSoft.Application.IdentityViewModel;
using KiraSoft.Application.Services.Indentity.Contracts;
using KiraSoft.CrossCutting.Operation.Executor;
using KiraSoft.CrossCutting.Operation.Transaction.Contracts;
using KiraSoft.Genealogy.Web.API.Areas.Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KiraSoft.Genealogy.Web.API.Areas.Authentication.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IUserLoginAppServicce _service;

        public AuthenticationController(IUserLoginAppServicce service) =>
            _service = service;

		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginViewModel viewModel)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

            async Task<UserViewModel> predicate() =>
                await _service.LoginAsync(viewModel.UserName, viewModel.Password);

            var result = await SafeExecutor<UserViewModel>.ExecAsync(predicate);
			return ProcessResponse(result);
		}

        private IActionResult ProcessResponse<T>(IAnswerBase<T> response, bool isPost = false) where T : class
        {
            if (response.Success)
            {
                if (isPost)
                    return new CreatedAtRouteResult($"Get{GetControllerName()}",
                        new { id = ((IBaseViewModel<int>)response.PayLoad).Id }, response);
                else return Ok(response);
            }
            else return BadRequest(response);
        }
        private string GetControllerName() =>
            ControllerContext.RouteData.Values["controller"].ToString();
    }
}
