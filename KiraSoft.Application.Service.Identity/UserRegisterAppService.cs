using KiraSoft.Application.IdentityViewModel;
using KiraSoft.Application.MapperBase;
using KiraSoft.Application.Services.Indentity.Contracts;
using KiraSoft.CrossCutting.Operation.Exceptions;
using KiraSoft.CrossCutting.Operation.Exceptions.Enum;
using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;
using System;
using System.Threading.Tasks;

namespace KiraSoft.Application.Service.Identity
{
    public class UserRegisterAppService : IUserRegisterAppService
    {

        private readonly string _userAlreadyExistsMessage = "The user already exists.";
        private readonly string _roleName = "USER";

        private IUserRegisterRepository _repository;
        private readonly IGenericMapper<User, UserViewModel, Guid> _mapper;

        public UserRegisterAppService(
            IGenericMapper<User, UserViewModel, Guid> mapper,
            IUserRegisterRepository repository) 
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<UserViewModel> ReguisterAsync(UserRegisterViewModel userRegisterViewModel)
        {
            await CheckIfUserAlreadyExistsAsync(userRegisterViewModel);
            User user = await CreateNewUserAsync(userRegisterViewModel);
            await SetToRolAsync(user);

            return _mapper.GetViewModel(user);;
        }

        private async Task SetToRolAsync(User user)
        {
            var role = await _repository.FindRoleByNameAsync(_roleName);
            if(role != null) await AddUserToRoleAsync(user);
        }

        private Task AddUserToRoleAsync(User user) =>
            _repository.AddUserToRoleAsync(user, _roleName);

        private async Task<User> CreateNewUserAsync(UserRegisterViewModel userRegisterViewModel)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = userRegisterViewModel.Email,
                NormalizedUserName = userRegisterViewModel.Email.ToUpper(),
                Email = userRegisterViewModel.Email,
                NormalizedEmail = userRegisterViewModel.Email.ToUpper(),
                EmailConfirmed = false,
                TwoFactorEnabled = true,
                LockoutEnabled = true,
                PersonName = userRegisterViewModel.Name,
                FirstFamilyName = userRegisterViewModel.FirstFamilyName,
                SecondFamilyName = userRegisterViewModel.SecondFamilyName,
                Enabled = true,
                CreatedBy = "SYSTEM",
                CreationDate = DateTime.UtcNow,
                LastModifiedBy = "SYSTEM",
                LastModificationDate = DateTime.UtcNow
            };

            await ExecuteCreateAsync(user, userRegisterViewModel.Password);
            return user;
        }

        private async Task ExecuteCreateAsync(User user, string password) =>
            await _repository.CreateUserAsync(user, password);

        private async Task CheckIfUserAlreadyExistsAsync(UserRegisterViewModel userRegisterViewModel)
        {
            User user = await _repository.FindUserByNameAsync(userRegisterViewModel.Email);
            if (user != null)
                throw new BusinessException(_userAlreadyExistsMessage, FailureCode.AlreadyExist);
        }
    }
}
