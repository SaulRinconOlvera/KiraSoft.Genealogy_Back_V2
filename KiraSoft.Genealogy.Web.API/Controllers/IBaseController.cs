using KiraSoft.Application.Base.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KiraSoft.Genealogy.Web.API.Controllers
{
    public interface IBaseController<IViewModel>
    where IViewModel : class, IBaseViewModel<int>
    {
        Task<IActionResult> Get();
        Task<IActionResult> Get(int id);

        Task<IActionResult> Post([FromBody] IViewModel viewModel);
        Task<IActionResult> Put(int id, [FromBody] IViewModel viewModel);
        Task<IActionResult> Delete(int id);
    }
}
