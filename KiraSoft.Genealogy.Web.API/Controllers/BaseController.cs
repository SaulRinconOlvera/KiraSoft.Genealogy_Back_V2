using KiraSoft.Application.Base.ViewModel;
using KiraSoft.CrossCutting.Operation.Transaction.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KiraSoft.Genealogy.Web.API.Controllers
{
    public class BaseController : Controller
    {
        protected ILogger _logger;

        public BaseController(ILogger logger) => _logger = logger;

        protected IActionResult ProcessResponse<T>(IAnswerBase<T> response, bool isPost = false) where T : class
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

        protected string GetControllerName() =>
            ControllerContext.RouteData.Values["controller"].ToString();
    }
}
