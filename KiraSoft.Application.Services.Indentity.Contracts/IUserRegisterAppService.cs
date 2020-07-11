using KiraSoft.Application.Base.Service;
using KiraSoft.Application.IdentityViewModel;
using System.Threading.Tasks;

namespace KiraSoft.Application.Services.Indentity.Contracts
{
    public interface IUserRegisterAppService 
    {
        Task<UserViewModel> ReguisterAsync(UserRegisterViewModel userRegisterViewModel);
    }
}
