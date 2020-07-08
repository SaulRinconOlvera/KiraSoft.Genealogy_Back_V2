using AutoMapper;
using KiraSoft.Application.Base.Service;
using KiraSoft.Application.IdentityViewModel;
using KiraSoft.Application.Services.Indentity.Contracts;
using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;
using System;

namespace KiraSoft.Application.Service.Identity
{
    public class RoleAppService :
        ApplicationServiceBase<Role, RoleViewModel, Guid>, IRoleAppService
    {
        public RoleAppService(IRoleRepository repository, IMapper mapper) : base(mapper)
        { _repository = repository; }
    }
}
