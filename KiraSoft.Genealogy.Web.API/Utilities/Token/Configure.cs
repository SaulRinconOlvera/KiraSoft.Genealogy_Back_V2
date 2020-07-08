using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace KiraSoft.Genealogy.Web.API.Utilities.Token
{
    public class Configure
    {
        public static void ConfigureJWT(IServiceCollection services, IConfiguration configuration) 
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(o =>
               o.TokenValidationParameters = GetJWTParameters(configuration));
        }

        private static TokenValidationParameters GetJWTParameters(IConfiguration configuration) =>
            new TokenValidationParameters()
            {
                ValidateIssuer = configuration.GetValue<bool>("JwtBearer:TokenValidationParameters:ValidateIssuer"),
                ValidateAudience = configuration.GetValue<bool>("JwtBearer:TokenValidationParameters:ValidateAudience"),
                ValidateLifetime = configuration.GetValue<bool>("JwtBearer:TokenValidationParameters:ValidateLifetime"),
                ValidateIssuerSigningKey = configuration.GetValue<bool>("JwtBearer:TokenValidationParameters:ValidateIssuerSigningKey"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    configuration.GetValue<string>("JwtBearer:SecretKey"))),
                ClockSkew = TimeSpan.Zero
            };
    }
}
