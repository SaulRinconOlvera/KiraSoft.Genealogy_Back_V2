using KiraSoft.Application.Base.Service;
using KiraSoft.Application.IdentityViewModel;
using KiraSoft.Application.MapperBase;
using KiraSoft.Application.Services.Indentity.Contracts;
using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;
using System;
using System.Threading.Tasks;

namespace KiraSoft.Application.Service.Identity
{
    public class UserAppService : ApplicationServiceBase<User, UserViewModel, Guid>, IUserAppService
    {
        public UserAppService(IUserRepository repository,
            IGenericMapper<User, UserViewModel, Guid>  mapper) : base(mapper) =>
            _repository = repository;

        public Task<UserViewModel> LoginAsync(string userName, string password)
        {
            throw new NotImplementedException();
            //var entity = await((ILoginRepository)_repository)
            //    .LoginAsync(userName, password);

            //if (entity is null) return null;
            //return _mapper.GetViewModel(entity);
        }

        //public async Task Logout() =>
        //    await ((IUserRepository)_repository).LogoutAsync();

        //public async Task<UserViewModel> SocialNetwiorkLoginAsync(string userId, string platform)
        //{
        //    var entity = await ((IUserRepository)_repository)
        //        .SocialNetwiorkLoginAsync(userId, platform);

        //    if (entity is null) return null;
        //    return _mapper.GetViewModel(entity);
        //}

        public UserViewModel GetForModify(Guid viewModelId)
        {
            var entity = _repository.Get(viewModelId);
            return GetModelForModify(entity);
        }

        public async Task<UserViewModel> GetForModifyAsync(Guid viewModelId)
        {
            var entity = await _repository.GetAsync(viewModelId);
            return GetModelForModify(entity);
        }

        private UserViewModel GetModelForModify(User entity)
        {
            var model = _mapper.GetViewModel(entity);
            //model.PasswordHash = entity.PasswordHash;
            //model.SecurityStamp = entity.SecurityStamp;
            return model;
        }

        public override UserViewModel AddWithResponse(UserViewModel viewModel)
        {
            viewModel = base.AddWithResponse(viewModel);
            return viewModel;
        }

        public override async Task<UserViewModel> AddWithResponseAsync(UserViewModel viewModel)
        {
            viewModel = await base.AddWithResponseAsync(viewModel);
            return viewModel;
        }

        public override void Add(UserViewModel viewModel) =>
            base.Add(viewModel);

        public override async Task AddAsync(UserViewModel viewModel) =>
            await base.AddAsync(viewModel);
    }
}
