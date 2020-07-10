using KiraSoft.Application.Base.Service;
using KiraSoft.Application.IdentityViewModel;
using KiraSoft.Application.MapperBase;
using KiraSoft.Application.Services.Indentity.Contracts;
using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;

namespace KiraSoft.Application.Service.Identity
{
    public class UserClaimAppService :
        ApplicationServiceBase<UserClaim, UserClaimViewModel, int>, IUserClaimAppService
    {
        public UserClaimAppService(
            IUserClaimRepository repository,
            IGenericMapper<UserClaim, UserClaimViewModel, int> mapper) : base(mapper)
        { _repository = repository; }
    }
}
