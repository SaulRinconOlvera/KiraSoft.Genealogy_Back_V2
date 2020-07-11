using KiraSoft.Domain.Model.Identity;
using System.Threading.Tasks;

namespace KiraSoft.Domain.IdentityRepository
{
    public interface IUserRegisterRepository
    {
        Task<User> FindUserByNameAsync(string userName);
        Task CreateUserAsync(User user, string password);
        Task<Role> FindRoleByNameAsync(string roleName);
        Task AddUserToRolAsync(User user, string rolName);
    }
}
