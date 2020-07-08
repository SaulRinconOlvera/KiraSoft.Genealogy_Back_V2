using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;
using KiraSoft.Domain.RepositoryBase.Contracts;
using KiraSoft.Infrastructure.RepositoryBase.Implementation;
using System;

namespace KiraSoft.Infrastructure.IdentityRepository
{
    public class UserRepository : RepositoryBase<Guid, User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    }
}
