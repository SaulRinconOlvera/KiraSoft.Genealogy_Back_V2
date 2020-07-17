using KiraSoft.Application.IdentityViewModel;
using System;
using System.Threading.Tasks;

namespace KiraSoft.Application.Services.Indentity.Contracts
{
    public interface IUserRegisterAppService 
    {
        Task<UserViewModel> ReguisterAsync(UserRegisterViewModel userRegisterViewModel);
        Task ValidateEmailConfimationLinkAsync(Guid userId, string token);
    }
}
