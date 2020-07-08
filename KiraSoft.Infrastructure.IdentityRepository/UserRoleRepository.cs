using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;
using KiraSoft.Domain.RepositoryBase.Contracts;
using KiraSoft.Infrastructure.RepositoryBase.Implementation;
using System;

namespace KiraSoft.Infrastructure.IdentityRepository
{
    public class UserRoleRepository : RepositoryBase<Guid, UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
