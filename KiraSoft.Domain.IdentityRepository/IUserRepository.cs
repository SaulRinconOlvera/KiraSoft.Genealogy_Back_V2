using KiraSoft.Domain.Model.Identity;
using KiraSoft.Domain.RepositoryBase.Contracts;
using System;

namespace KiraSoft.Domain.IdentityRepository
{
    public interface IUserRepository : IRepositoryBase<Guid, User> { }
}
