using KiraSoft.Domain.Model.Identity;
using System.Threading.Tasks;

namespace KiraSoft.Domain.IdentityRepository
{
    public interface ILoginRepository
    {
        Task<User> LoginAsync(string userName, string password);
        Task<User> SocialNetwiorkLoginAsync(string userId, string platform);
        Task LogoutAsync();
    }
}
