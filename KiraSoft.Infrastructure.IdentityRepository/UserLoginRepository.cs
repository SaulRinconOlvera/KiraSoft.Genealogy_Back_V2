using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;
using KiraSoft.Domain.RepositoryBase.Contracts;
using KiraSoft.Infrastructure.RepositoryBase.Implementation;
using System;

namespace KiraSoft.Infrastructure.IdentityRepository
{
    public class UserLoginRepository : RepositoryBase<Guid, UserLogin>, IUserLoginRepository
    {
        public UserLoginRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
    }
}
