using Microsoft.AspNetCore.Mvc;

namespace KiraSoft.Genealogy.Web.API.Areas.Authentication.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class UserRegisterController : Controller
    {

        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }
    }
}
