using KiraSoft.Domain.Model.Identity;
using System;
using System.Threading.Tasks;

namespace KiraSoft.Domain.IdentityRepository
{
    public interface IUserRegisterRepository
    {
        Task<User> FindUserByNameAsync(string userName);
        Task CreateUserAsync(User user, string password);
        Task<Role> FindRoleByNameAsync(string roleName);
        Task AddUserToRoleAsync(User user, string rolName);
        Task<string> GenerarteEmailConfirmationLinkgAsync(User user);
        Task ValidateEmailConfigurationLinkAsync(Guid userId, string token);
    }
}
