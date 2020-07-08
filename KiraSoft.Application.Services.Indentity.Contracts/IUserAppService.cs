using KiraSoft.Application.Base.Service;
using KiraSoft.Application.IdentityViewModel;
using System;
using System.Threading.Tasks;

namespace KiraSoft.Application.Services.Indentity.Contracts
{
    public interface IUserAppService : IApplicationServiceBase<Guid, UserViewModel>
    {
        Task<UserViewModel> GetForModifyAsync(Guid viewModelId);
        UserViewModel GetForModify(Guid viewModelId);
    }
}
