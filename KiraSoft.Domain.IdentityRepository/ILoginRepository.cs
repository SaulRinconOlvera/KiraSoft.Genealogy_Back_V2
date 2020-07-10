using KiraSoft.Domain.Model.Identity;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KiraSoft.Domain.IdentityRepository
{
    public interface ILoginRepository
    {
        Task<User> FindUserByName(string userName);
        Task<bool> ValidatePasswordAsync(User user, string password);
        Task TryLoginAsync(string userName, string password);
        Task<IList<string>> GetUserRolesAsync(User user);
        Task<ClaimsPrincipal> GetUserPrincipalClaimsAsync(User user);
        Task<IList<Claim>> GetClaimsAsync(User user);

    }
}
