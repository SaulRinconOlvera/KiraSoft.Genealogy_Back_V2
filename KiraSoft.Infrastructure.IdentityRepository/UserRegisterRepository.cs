using KiraSoft.CrossCutting.Operation.Exceptions;
using KiraSoft.CrossCutting.Operation.Exceptions.Enum;
using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;
using KiraSoft.Infrastructure.Persistence.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace KiraSoft.Infrastructure.IdentityRepository
{
    public class UserRegisterRepository : IUserRegisterRepository
    {
        public async Task CreateUserAsync(User user, string password)
        {
            var result = await DataBaseConfiguration.UserManager.CreateAsync(user, password);
            CheckForErrorResult(result);
        }

        private void CheckForErrorResult(IdentityResult result)
        {
            if (!result.Succeeded) throw new BusinessException(
                 result.Errors.ToList().FirstOrDefault().Description, FailureCode.ErrorAtCreateUser);
        }

        public async Task<User> FindUserByNameAsync(string userName) =>
            await DataBaseConfiguration.UserManager.FindByNameAsync(userName);

        public async Task<Role> FindRoleByNameAsync(string roleName) =>
            await DataBaseConfiguration.RoleManager.FindByNameAsync(roleName);

        public async Task AddUserToRolAsync(User user, string roleName) 
        {
            var result = await DataBaseConfiguration.UserManager.AddToRoleAsync(user, roleName);

            if (!result.Succeeded) throw new BusinessException(
                 result.Errors.ToList().FirstOrDefault().Description, FailureCode.ErrorAtCreateUser);
        }
            
    }
}
