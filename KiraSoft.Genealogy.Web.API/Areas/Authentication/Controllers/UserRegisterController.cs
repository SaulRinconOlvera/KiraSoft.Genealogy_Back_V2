﻿using KiraSoft.Application.IdentityViewModel;
using KiraSoft.Application.Services.Indentity.Contracts;
using KiraSoft.CrossCutting.Operation.Executor;
using KiraSoft.Genealogy.Web.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace KiraSoft.Genealogy.Web.API.Areas.Authentication.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class UserRegisterController : BaseController
    {
        IUserRegisterAppService _service;

        public UserRegisterController(
            ILogger<UserRegisterController> logger,
            IUserRegisterAppService service) :  base(logger)  =>
            _service = service;

        [HttpPost]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            async Task<UserViewModel> predicate() =>
                await _service.ReguisterAsync(viewModel);

            var result = await SafeExecutor<UserViewModel>.ExecAsync(predicate, _logger);
            return ProcessResponse(result);

        }
    }
}
