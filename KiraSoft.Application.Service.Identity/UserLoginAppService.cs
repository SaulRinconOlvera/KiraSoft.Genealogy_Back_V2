using KiraSoft.Application.IdentityViewModel;
using KiraSoft.Application.MapperBase;
using KiraSoft.Application.Service.Identity.Utilities;
using KiraSoft.Application.Services.Indentity.Contracts;
using KiraSoft.CrossCutting.Operation.Exceptions;
using KiraSoft.CrossCutting.Operation.Exceptions.Enum;
using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KiraSoft.Application.Service.Identity
{
    public class UserLoginAppService : IUserLoginAppServicce
    {
        private readonly string _userWrongMessage = "User or password are wrong";
        private readonly string _confirmEmailMessage = "Must be confirm your email account.";
        private readonly IConfiguration _configuration;

        private readonly ILoginRepository _repository;
        private readonly IGenericMapper<User, UserViewModel, Guid> _mapper;

        public UserLoginAppService(
            ILoginRepository repository, 
            IConfiguration configuration,
            IGenericMapper<User, UserViewModel, Guid> mapper) 
        {
            _mapper = mapper;
            _configuration = configuration;
            _repository = repository;
        }

        public async  Task<UserViewModel> LoginAsync(string userName, string password) =>
            await ValidateUserAsync(userName, password);

        private async Task<UserViewModel> ValidateUserAsync(string userName, string password)
        {
            var user = await _repository.FindUserByName(userName);
            ValidaUserData(user);
            ValidateTwoFactor(user);
            await ValidatePasswordAsync(user, password);
            await _repository.TryLoginAsync(userName, password);
            await ProcessToken(user);
            return _mapper.GetViewModel(user);
        }

        private async Task ProcessToken(User user)
        {
            await ProcessClaimsAsync(user);
            Token.BuildToken(user, _configuration);
        }

        private async Task ProcessClaimsAsync(User user)
        {
            await GetPrinsipalUserClaims(user);
            await GetUserClaims(user);
            await GetUserRolesAsync(user);
        }

        private async Task GetPrinsipalUserClaims(User user)
        {
            var claims = await _repository.GetUserPrincipalClaimsAsync(user);
            if (claims != null) AddUserClaims(user, claims.Claims);

        }

        private async Task GetUserClaims(User user)
        {
            var claims = await _repository.GetClaimsAsync(user);
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

        private async Task GetUserRolesAsync(User user)
        {
            var roles = await _repository.GetUserRolesAsync(user);

            foreach (var role in roles)
            {
                user.RolesNames = user.RolesNames +
                    (string.IsNullOrWhiteSpace(user.RolesNames) ? "" : "|") + role;
            }
        }

        private async Task ValidatePasswordAsync(User user, string password)
        {
            if(!await _repository.ValidatePasswordAsync(user, password))
                throw new BusinessException(_userWrongMessage, FailureCode.UnauthorizedAccess);
        }

        private void ValidateTwoFactor(User user)
        {
            if (user.TwoFactorEnabled && !user.EmailConfirmed)
                throw new BusinessException(_confirmEmailMessage, FailureCode.UnauthorizedAccess);
        }

        private void ValidaUserData(User user)
        {
            if (user is null)
                throw new BusinessException(_userWrongMessage, FailureCode.UnauthorizedAccess);
        }

        public Task Logout()
        {
            throw new System.NotImplementedException();
        }

        public Task<UserViewModel> SocialNetwiorkLoginAsync(string userId, string platform)
        {
            throw new NotImplementedException();
        }
    }
}
