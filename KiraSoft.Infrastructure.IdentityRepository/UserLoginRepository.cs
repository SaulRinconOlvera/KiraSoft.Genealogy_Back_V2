using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;
using KiraSoft.Domain.RepositoryBase.Contracts;
using KiraSoft.Infrastructure.RepositoryBase.Implementation;

namespace KiraSoft.Infrastructure.IdentityRepository
{
    public class UserLoginRepository : RepositoryBase<int, UserLogin>, IUserLoginRepository
    {
        public UserLoginRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
    }
}
