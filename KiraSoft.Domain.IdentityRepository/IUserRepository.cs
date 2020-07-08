using KiraSoft.Domain.Model.Identity;
using KiraSoft.Domain.RepositoryBase.Contracts;

namespace KiraSoft.Domain.IdentityRepository
{
    public interface IUserRepository : IRepositoryBase<int, User> { }
}
