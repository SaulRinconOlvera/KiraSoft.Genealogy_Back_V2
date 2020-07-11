using KiraSoft.CrossCutting.Operation.Exceptions;
using KiraSoft.CrossCutting.Operation.Exceptions.Enum;
using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;
using KiraSoft.Infrastructure.Persistence.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KiraSoft.Infrastructure.IdentityRepository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly string _userAreBlokedMessage = "The user are blocked.";
        private readonly string _accessNotAllowedMessage = "The acces is not allowed.";

        public async Task<User> FindUserByName(string userName) =>
            await DataBaseConfiguration.UserManager.FindByNameAsync(userName);

        public async Task<bool> ValidatePasswordAsync(User user, string password) =>
            await DataBaseConfiguration.UserManager.CheckPasswordAsync(user, password);

        public async Task TryLoginAsync(string userName, string password)
        {
            SignInResult result = await DataBaseConfiguration.SignManager.PasswordSignInAsync(
                userName, password, false, false);

            CheckForErrorOnSingingResult(result);
        }

        private void CheckForErrorOnSingingResult(SignInResult result)
        {
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    throw new BusinessException(_userAreBlokedMessage, FailureCode.UnauthorizedAccess);

                if (result.IsNotAllowed)
                    throw new BusinessException(_accessNotAllowedMessage, FailureCode.UnauthorizedAccess);
            }
        }

        public async Task<IList<string>> GetUserRolesAsync(User user) =>
             await DataBaseConfiguration.UserManager.GetRolesAsync(user);


        public async Task<ClaimsPrincipal> GetUserPrincipalClaimsAsync(User user) =>
            await DataBaseConfiguration.SignManager.CreateUserPrincipalAsync(user);


        public async Task<IList<Claim>> GetClaimsAsync(User user) =>
            await DataBaseConfiguration.UserManager.GetClaimsAsync(user);
        
    }
}
