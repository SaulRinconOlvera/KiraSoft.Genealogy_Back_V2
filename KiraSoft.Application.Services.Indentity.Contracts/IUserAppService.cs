using KiraSoft.Application.Base.Service;
using KiraSoft.Application.IdentityViewModel;
using System.Threading.Tasks;

namespace KiraSoft.Application.Services.Indentity.Contracts
{
    public interface IUserAppService : IApplicationServiceBase<int, UserViewModel>
    {
        Task<UserViewModel> GetForModifyAsync(int viewModelId);
        UserViewModel GetForModify(int viewModelId);
    }
}
