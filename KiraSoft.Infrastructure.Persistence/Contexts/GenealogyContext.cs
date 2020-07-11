using KiraSoft.Domain.Model.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;

namespace KiraSoft.Infrastructure.Persistence.Contexts
{
    public class GenealogyContext :
        IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly IConfiguration _config;
        private readonly ILoggerFactory _loggerFactory;

        public DbSet<TokenHistory> TokensHistory { get; set; }

        public GenealogyContext(IConfiguration config, ILoggerFactory loggerFactory) 
        {
            _config = config;
            _loggerFactory = loggerFactory;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:Genealogy"]);

            if (_config.GetValue<bool>("Application:EnableDatabaseLogging"))
                optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging(true);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            CreateIdentity(modelBuilder);
            SetFilter(modelBuilder);
        }

        private void SetFilter(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var isActiveProperty = entityType.FindProperty("Enabled");
                if (isActiveProperty != null && isActiveProperty.ClrType == typeof(bool))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "p");
                    var filter = Expression.Lambda(Expression.Property(parameter, isActiveProperty.PropertyInfo), parameter);
                    entityType.SetQueryFilter(filter);
                }
            }
        }

        private void CreateIdentity(ModelBuilder modelBuilder)
        {
            //  Indentity
            modelBuilder.Entity<User>(b =>
            {
                b.HasIndex(e => e.NormalizedUserName).HasName("UserNameIndex").IsUnique();
                b.HasIndex(e => e.NormalizedEmail).HasName("EmailIndex").IsUnique();
                b.HasIndex(e => e.NormalizedEmail).HasName("EmailIndex");
                b.Property(e => e.ConcurrencyStamp).IsConcurrencyToken();
                b.Property(e => e.UserName).HasMaxLength(256);
                b.Property(e => e.NormalizedUserName).HasMaxLength(256);
                b.Property(e => e.Email).HasMaxLength(256);
                b.Property(e => e.NormalizedEmail).HasMaxLength(256);
                b.ToTable("User");
            });

            //.ToTable("User");
            modelBuilder.Entity<Role>(b =>
            {
                b.HasIndex(e => e.NormalizedName).HasName("RoleNameIndex").IsUnique();
                b.Property(e => e.ConcurrencyStamp).IsConcurrencyToken();
                b.Property(e => e.Name).HasMaxLength(256);
                b.Property(e => e.NormalizedName).HasMaxLength(256);
                b.ToTable("Role");
            });

            modelBuilder.Entity<RoleClaim>(b =>
            {
                b.HasOne(roleClaim => roleClaim.Role).WithMany(role => role.Claims).HasForeignKey(roleClaim => roleClaim.RoleId);
                b.ToTable("RoleClaim");
            });

            modelBuilder.Entity<UserClaim>(b =>
            {
                b.HasOne(userClaim => userClaim.User).WithMany(user => user.Claims).HasForeignKey(userClaim => userClaim.UserId);
                b.ToTable("UserClaim");
            });

            modelBuilder.Entity<UserLogin>(b =>
            {
                b.HasKey(e => new { e.LoginProvider, e.ProviderKey });
                b.Property(e => e.LoginProvider).HasMaxLength(128);
                b.Property(e => e.ProviderKey).HasMaxLength(128);
                b.HasOne(userLogin => userLogin.User).WithMany(user => user.Logins).HasForeignKey(userLogin => userLogin.UserId);
                b.ToTable("UserLogin");
            });

            modelBuilder.Entity<UserRole>(b =>
            {
                b.HasKey(e => new { e.UserId, e.RoleId });
                b.HasOne(userRole => userRole.Role).WithMany(role => role.Users).HasForeignKey(userRole => userRole.RoleId);
                b.HasOne(userRole => userRole.User).WithMany(user => user.Roles).HasForeignKey(userRole => userRole.UserId);
                b.ToTable("UserRole");
            });

            modelBuilder.Entity<UserToken>(b =>
            {
                b.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
                b.Property(e => e.LoginProvider).HasMaxLength(256);
                b.Property(e => e.Name).HasMaxLength(256);
                b.HasOne(userToken => userToken.User).WithMany(user => user.Tokens).HasForeignKey(userToken => userToken.UserId);
                b.ToTable("UserToken");
            });
        }
    }
}
