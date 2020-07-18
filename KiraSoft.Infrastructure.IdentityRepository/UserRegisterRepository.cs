using KiraSoft.CrossCutting.Operation.Exceptions;
using KiraSoft.CrossCutting.Operation.Exceptions.Enum;
using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;
using KiraSoft.Infrastructure.Persistence.Configuration;
using KiraSoft.Infrastructure.Persistence.Configuration.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiraSoft.Infrastructure.IdentityRepository
{
    public class UserRegisterRepository : IUserRegisterRepository
    {
        private readonly IUserRoleRepository _repository;
        private readonly IIdentityConfiguration _identityConfiguration;

        public UserRegisterRepository(
            IUserRoleRepository repository,
            IIdentityConfiguration identityConfiguration) 
        {
            _repository = repository;
            _identityConfiguration = identityConfiguration;
        }
            

        public async Task CreateUserAsync(User user, string password)
        {
            var result = await _identityConfiguration.UserManager.CreateAsync(user, password);
            CheckForErrorResult(result);
        }

        public async Task<User> FindUserByNameAsync(string userName) =>
           await _identityConfiguration.UserManager.FindByNameAsync(userName);

        public async Task<Role> FindRoleByNameAsync(string roleName) =>
            await _identityConfiguration.RoleManager.FindByNameAsync(roleName);

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            Role role = await GetRoleByNameAsync(roleName);
            UserRole userRole = GetUserRole(user, role);

            await _repository.AddAsync(userRole);
            await _repository.SaveAsync();
        }

        public async Task<string> GenerarteEmailConfirmationLinkgAsync(User user)
        {
            var token = await _identityConfiguration.UserManager.GenerateEmailConfirmationTokenAsync(user);
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        }

        public async Task ValidateEmailConfigurationLinkAsync(Guid userId, string token)
        {
            var user = await FindUserByIdAsync(userId);
            ValidateAlreadyConfirmatedEmail(user);
            await ConfirmUserEmailAsync(user, token);
        }

        private void ValidateAlreadyConfirmatedEmail(User user)
        {
            if (user.EmailConfirmed)
                throw new BusinessException("Email already confirmed.", FailureCode.InvalidTransaction);
        }

        private async Task ConfirmUserEmailAsync(User user, string token)
        {
            var tokenDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var tokenDecoded = Encoding.UTF8.GetString(tokenDecodedBytes);

            var result = await _identityConfiguration.UserManager.ConfirmEmailAsync(user, tokenDecoded);
            ValidateResult(result);
        }

        private void ValidateResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                string errores = string.Empty;
                foreach (var error in result.Errors) 
                    errores += $"Code:{error.Code} - Description:{error.Description} {Environment.NewLine}";

                throw new BusinessException(errores, FailureCode.UnknownError);
            }    
        }

        public async Task<User> FindUserByIdAsync(Guid userId)
        {
            User user = await _identityConfiguration.UserManager.FindByIdAsync(userId.ToString());
            if (user is null) 
                throw new BusinessException("User Id is invalid", FailureCode.NotFound);

            return user;
        }

        private void CheckForErrorResult(IdentityResult result)
        {
            if (!result.Succeeded) throw new BusinessException(
                 result.Errors.ToList().FirstOrDefault().Description, FailureCode.ErrorAtCreateUser);
        }

        private async Task<Role> GetRoleByNameAsync(string roleName)
        {
            var role = await _identityConfiguration.RoleManager.FindByNameAsync(roleName);
            if (role is null) throw new BusinessException("The role isn't exists", FailureCode.ErrorAtCreateUser);
            return role;
        }

        private UserRole GetUserRole(User user, Role role)
        {
            return new UserRole()
            {
                Id = Guid.NewGuid(),
                RoleId = role.Id,
                UserId = user.Id,
                CreatedBy = "SYSTEM",
                CreationDate = DateTime.UtcNow,
                LastModifiedBy = "SYSTEM",
                LastModificationDate = DateTime.UtcNow,
                Enabled = true
            };
        }
    }
}
