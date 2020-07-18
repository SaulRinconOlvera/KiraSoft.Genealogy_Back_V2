using KiraSoft.CrossCutting.Operation.Exceptions;
using KiraSoft.CrossCutting.Operation.Exceptions.Enum;
using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;
using KiraSoft.Infrastructure.Persistence.Configuration.Contracts;
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
        private readonly IIdentityConfiguration _identityConfiguration;

        public LoginRepository(IIdentityConfiguration identityConfiguration) =>
            _identityConfiguration = identityConfiguration;

        public async Task<User> FindUserByName(string userName) =>
            await _identityConfiguration.UserManager.FindByNameAsync(userName);

        public async Task<bool> ValidatePasswordAsync(User user, string password) =>
            await _identityConfiguration.UserManager.CheckPasswordAsync(user, password);

        public async Task TryLoginAsync(string userName, string password)
        {
            SignInResult result = await _identityConfiguration.SignManager.PasswordSignInAsync(
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
             await _identityConfiguration.UserManager.GetRolesAsync(user);


        public async Task<ClaimsPrincipal> GetUserPrincipalClaimsAsync(User user) =>
            await _identityConfiguration.SignManager.CreateUserPrincipalAsync(user);


        public async Task<IList<Claim>> GetClaimsAsync(User user) =>
            await _identityConfiguration.UserManager.GetClaimsAsync(user);
        
    }
}
