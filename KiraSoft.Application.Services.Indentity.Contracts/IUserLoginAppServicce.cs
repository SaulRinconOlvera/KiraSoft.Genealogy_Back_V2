using KiraSoft.Application.IdentityViewModel;
using System.Threading.Tasks;

namespace KiraSoft.Application.Services.Indentity.Contracts
{
    public interface IUserLoginAppServicce
    {
        Task<UserViewModel> LoginAsync(string userName, string password);
        Task<UserViewModel> SocialNetwiorkLoginAsync(string userId, string platform);
        Task Logout();
    }
}
