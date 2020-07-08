using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;
using KiraSoft.Domain.RepositoryBase.Contracts;
using KiraSoft.Infrastructure.RepositoryBase.Implementation;

namespace KiraSoft.Infrastructure.IdentityRepository
{
    public class UserClaimRepository : RepositoryBase<int, UserClaim>, IUserClaimRepository
    {
        public UserClaimRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
