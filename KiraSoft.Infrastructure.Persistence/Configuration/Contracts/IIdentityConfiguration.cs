using KiraSoft.Domain.Model.Identity;
using Microsoft.AspNetCore.Identity;

namespace KiraSoft.Infrastructure.Persistence.Configuration.Contracts
{
    public interface IIdentityConfiguration
    {
        public UserManager<User> UserManager { get; }
        public SignInManager<User> SignManager { get; }
        public RoleManager<Role> RoleManager { get; }
    }
}
