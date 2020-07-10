using KiraSoft.Application.Base.Service;
using KiraSoft.Application.IdentityViewModel;
using KiraSoft.Application.MapperBase;
using KiraSoft.Application.Services.Indentity.Contracts;
using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;
using System;

namespace KiraSoft.Application.Service.Identity
{
    public class UserRoleAppService :
        ApplicationServiceBase<UserRole, UserRoleViewModel, Guid>, IUserRoleAppService
    {
        public UserRoleAppService(
            IUserRoleRepository repository,
            IGenericMapper<UserRole, UserRoleViewModel, Guid> mapper) : base(mapper)
        { _repository = repository; }
    }
}
