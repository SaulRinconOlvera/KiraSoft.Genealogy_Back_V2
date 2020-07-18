using KiraSoft.Domain.Model.Identity;
using KiraSoft.Infrastructure.Persistence.Configuration.Contracts;
using Microsoft.AspNetCore.Identity;

namespace KiraSoft.Infrastructure.Persistence.Configuration
{
    public class IdentityConfiguration : IIdentityConfiguration
    {
        public IdentityConfiguration(
            UserManager<User> userManager,
            SignInManager<User> signManager,
            RoleManager<Role> roleManager)
        {
            UserManager = userManager;
            SignManager = signManager;
            RoleManager = roleManager;
        }

        public UserManager<User> UserManager { get; private set; }

        public SignInManager<User> SignManager { get; private set; }

        public RoleManager<Role> RoleManager { get; private set; }
    }
}
