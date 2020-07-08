using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;
using KiraSoft.Domain.RepositoryBase.Contracts;
using KiraSoft.Infrastructure.RepositoryBase.Implementation;
using System;

namespace KiraSoft.Infrastructure.IdentityRepository
{
    public class RoleRepository : RepositoryBase<Guid, Role>, IRoleRepository
    {
        public RoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
