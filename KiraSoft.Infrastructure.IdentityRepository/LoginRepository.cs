using KiraSoft.CrossCutting.Operation.Exceptions;
using KiraSoft.CrossCutting.Operation.Exceptions.Enum;
using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;
using KiraSoft.Infrastructure.Persistence.Configuration;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KiraSoft.Infrastructure.IdentityRepository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly string _userWrongMessage = "User or password are wrong";
        private readonly string _confirmEmailMessage = "Must be confirm your email account.";
        private readonly string _userAreBlokedMessage = "The user are blocked.";
        private readonly string _accessNotAllowedMessage = "The acces is not allowed.";

        public async Task<User> LoginAsync(string userName, string password) =>
            await ValidateUserAsync(userName, password);

        private async Task<User> ValidateUserAsync(string userName, string password)
        {
            var user = await DataBaseConfiguration.UserManager.FindByNameAsync(userName);
            ValidaUserData(user);
            ValidateTwoFactor(user);
            await ValidatePasswordAsync(user, password);
            await TryLoginAsync(userName, password);
            await ProcessToken(user);
            return user;
        }

        private async Task ProcessToken(User user)
        {
            await ProcessClaimsAsync(user);
        }

        private async Task ProcessClaimsAsync(User user)
        {
            await CreateUserClaims(user);
            await GetUserRolesAsync(user);
        }

        private async Task GetUserRolesAsync(User user)
        {
            var roles = await DataBaseConfiguration.UserManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                user.RolesNames = user.RolesNames +
                    (string.IsNullOrWhiteSpace(user.RolesNames) ? "" : "|") + role;
            }
        }

        private async Task CreateUserClaims(User user)
        {
            var userPrincipal = await DataBaseConfiguration.SignManager.CreateUserPrincipalAsync(user);
            await AddUserExistsClaimsAsync(user);
            AddUserClaims(user, userPrincipal.Claims);
        }

        private async Task AddUserExistsClaimsAsync(User user)
        {
            var claims = await DataBaseConfiguration.UserManager.GetClaimsAsync(user);
            if (claims != null) AddUserClaims(user, claims);
        }

        private void AddUserClaims(User user, IEnumerable<Claim> claims)
        {
            if (user.Claims is null) user.Claims = new List<UserClaim>();
            foreach (var claim in claims)
            {
                user.Claims.Add(
                    new UserClaim()
                    {
                        Id = 0,
                        UserId = user.Id,
                        ClaimType = claim.Type,
                        ClaimValue = claim.Value,
                        CreatedBy = "SYSTEM",
                        CreationDate = DateTime.UtcNow,
                        LastModifiedBy = "SYSTEM",
                        Enabled = true,
                        LastModificationDate = DateTime.UtcNow
                    });
            }
        }

        private async Task TryLoginAsync(string userName, string password)
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

        private async Task ValidatePasswordAsync(User user, string password)
        {
            var passwordIsValid = await DataBaseConfiguration.UserManager.CheckPasswordAsync(user, password);
            if (!passwordIsValid)
                throw new BusinessException(_userWrongMessage, FailureCode.UnauthorizedAccess);
        }

        private void ValidateTwoFactor(User user)
        {
            if (user.TwoFactorEnabled && !user.EmailConfirmed)
                throw new BusinessException(_confirmEmailMessage, FailureCode.UnauthorizedAccess);
        }

        private void ValidaUserData(User user)
        {
            if (user is null) throw new BusinessException(
                _userWrongMessage, FailureCode.UnauthorizedAccess);
        }

        public Task LogoutAsync() =>
            throw new NotImplementedException();

        public Task<User> SocialNetwiorkLoginAsync(string userId, string platform) =>
            throw new NotImplementedException();
    }
}
