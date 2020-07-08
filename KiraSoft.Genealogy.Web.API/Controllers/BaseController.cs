using KiraSoft.Application.Base.Service;
using KiraSoft.Application.Base.ViewModel;
using KiraSoft.CrossCutting.Operation.Exceptions;
using KiraSoft.CrossCutting.Operation.Executor;
using KiraSoft.CrossCutting.Operation.Transaction.Contracts;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KiraSoft.Genealogy.Web.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseController<TViewModel> : ControllerBase, IBaseController<TViewModel>
        where TViewModel : class, IBaseViewModel<int>
    {
        internal readonly IApplicationServiceBase<int, TViewModel> _service;
        internal string AcceptedFileTypes = string.Empty;

        public BaseController(IApplicationServiceBase<int, TViewModel> service) =>
            _service = service;

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            async Task<IEnumerable<TViewModel>> predicate() { return await _service.GetAllAsync(); }
            var response = await SafeExecutor<IEnumerable<TViewModel>>.ExecAsync(predicate);
            return ProcessResponse(response);
        }

        [HttpGet("{id}", Name = "Get[controller]")]
        public virtual async Task<IActionResult> Get(int id)
        {
            var response = await GetModelAsync(id);
            return ProcessResponse(response);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TViewModel viewModel)
        {
            SetServiceUser();

            async Task<TViewModel> predicate() { return await _service.AddWithResponseAsync(viewModel); }
            var response = await SafeExecutor<TViewModel>.ExecAsync(predicate);
            return ProcessResponse(response, true);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(int id, [FromBody] TViewModel viewModel)
        {
            SetServiceUser();

            if (viewModel.Id != id) return BadRequest();

            async Task predicate() => await _service.ModifyAsync(viewModel);
            var response = await SafeExecutor.ExecAsync(predicate);
            return ProcessResponse(response);
        }

        [HttpPatch("{Id}")]
        public virtual async Task<IActionResult> Patch(int Id, [FromBody] JsonPatchDocument<TViewModel> pathDocument)
        {
            SetServiceUser();
            async Task predicate() => await PathData(Id, pathDocument);
            var response = await SafeExecutor.ExecAsync(predicate);
            return ProcessResponse(response);
        }

        private async Task<TViewModel> PathData(int id, JsonPatchDocument<TViewModel> pathDocument)
        {
            if (pathDocument is null) throw new Exception("No existe");

            var response = await _service.GetAsync(id);
            if (response == null) throw new NotFoundException();

            pathDocument.ApplyTo(response, ModelState);
            if (!TryValidateModel(response)) throw new ModelStateException(ModelState);

            return await _service.ModifyWithResponseAsync(response);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            SetServiceUser();
            var response = await GetModelAsync(id);

            if (response.PayLoad == null) return NotFound(response);
            var response1 = SafeExecutor.Exec(() => _service.Remove(response.PayLoad));
            return ProcessResponse(response1); ;
        }

        private string GetControllerName() =>
            ControllerContext.RouteData.Values["controller"].ToString();

        private void SetServiceUser()
            => _service.SetUser(GetUser());

        protected string GetUser()
        {
            if (HttpContext.User is null || !HttpContext.User.Claims.Any()) return string.Empty;
            return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }

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

        private async Task<IAnswerBase<TViewModel>> GetModelAsync(int id)
        {
            async Task<TViewModel> predicate() { return await _service.GetAsync(id); }
            return await SafeExecutor<TViewModel>.ExecAsync(predicate);
        }
    }
}