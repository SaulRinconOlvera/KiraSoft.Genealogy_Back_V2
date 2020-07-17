using KiraSoft.CrossCutting.Operation.Exceptions;
using KiraSoft.CrossCutting.Operation.Exceptions.Enum;
using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;
using KiraSoft.Infrastructure.Persistence.Configuration;
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

        public UserRegisterRepository(IUserRoleRepository repository) => 
            _repository = repository;

        public async Task CreateUserAsync(User user, string password)
        {
            var result = await DataBaseConfiguration.UserManager.CreateAsync(user, password);
            CheckForErrorResult(result);
        }

        public async Task<User> FindUserByNameAsync(string userName) =>
           await DataBaseConfiguration.UserManager.FindByNameAsync(userName);

        public async Task<Role> FindRoleByNameAsync(string roleName) =>
            await DataBaseConfiguration.RoleManager.FindByNameAsync(roleName);

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            Role role = await GetRoleByNameAsync(roleName);
            UserRole userRole = GetUserRole(user, role);

            await _repository.AddAsync(userRole);
            await _repository.SaveAsync();
        }

        public async Task<string> GenerarteEmailConfirmationLinkgAsync(User user)
        {
            var token = await DataBaseConfiguration.UserManager.GenerateEmailConfirmationTokenAsync(user);
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        }

        public async Task ValidateEmailConfigurationLinkAsync(Guid userId, string token)
        {
            var user = await FindUserByIdAsync(userId);
            await ConfirmUserEmailAsync(user, token);
        }

        private async Task ConfirmUserEmailAsync(User user, string token)
        {
            var tokenDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var tokenDecoded = Encoding.UTF8.GetString(tokenDecodedBytes);

            var result = await DataBaseConfiguration.UserManager.ConfirmEmailAsync(user, tokenDecoded);
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
            User user = await DataBaseConfiguration.UserManager.FindByIdAsync(userId.ToString());
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
            var role = await DataBaseConfiguration.RoleManager.FindByNameAsync(roleName);
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
