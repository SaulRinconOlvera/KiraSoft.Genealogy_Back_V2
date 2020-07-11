using KiraSoft.Domain.IdentityRepository;
using KiraSoft.Domain.RepositoryBase.Contracts;
using KiraSoft.Infrastructure.IdentityRepository;
using KiraSoft.Infrastructure.Persistence.Contexts;
using KiraSoft.Infrastructure.RepositoryBase.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KiraSoft.Infrastructure.Repositories
{
    public class RegisterRepositories
    {
        public static void Register(
            IServiceCollection services, 
            IConfiguration configuration
            )
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>(
                (x) => new UnitOfWork(new GenealogyContext(configuration)));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IUserLoginRepository, UserLoginRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserRoleRepository, UserRoleRepository>();
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<ITokenHistoryRepository, TokenHistoryRepository>();
            services.AddTransient<IUserRegisterRepository, UserRegisterRepository>();
        }
    }
}
