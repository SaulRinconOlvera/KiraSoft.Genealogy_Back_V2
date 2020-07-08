using KiraSoft.Application.Base.Service;
using KiraSoft.Application.IdentityViewModel;
using System;

namespace KiraSoft.Application.Services.Indentity.Contracts
{
    public interface IRoleAppService : IApplicationServiceBase<Guid, RoleViewModel> { }
}
