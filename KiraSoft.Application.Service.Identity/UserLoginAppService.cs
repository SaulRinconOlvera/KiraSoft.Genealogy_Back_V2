using AutoMapper;
using KiraSoft.Application.IdentityViewModel;
using KiraSoft.Application.MapperBase;
using KiraSoft.Application.Services.Indentity.Contracts;
using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.Model.Identity;
using System.Threading.Tasks;

namespace KiraSoft.Application.Service.Identity
{
    public class UserLoginAppService : IUserLoginAppServicce
    {
        private ILoginRepository _repository;
        private GenericMapper<User, UserViewModel, int> _mapper;

        public UserLoginAppService(ILoginRepository repository, IMapper mapper) 
        {
            _mapper = new GenericMapper<User, UserViewModel, int>(mapper);
            _repository = repository;
        }

        public async Task<UserViewModel> LoginAsync(string userName, string password)
        {
            var entity = await _repository
                .LoginAsync(userName, password);

            if (entity is null) return null;
            return _mapper.GetViewModel(entity);
        }

        public Task Logout()
        {
            throw new System.NotImplementedException();
        }

        public Task<UserViewModel> SocialNetwiorkLoginAsync(string userId, string platform)
        {
            throw new System.NotImplementedException();
        }
    }
}
