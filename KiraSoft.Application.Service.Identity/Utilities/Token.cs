using KiraSoft.Domain.Model.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace KiraSoft.Application.Service.Identity.Utilities
{
    public static class Token
    {
        private static DateTime _currentDateTime;
        private static DateTime _expiration;
        private static TokenHistory _tokenHistory;

        public static TokenHistory BuildToken(User user, IConfiguration configuration)
        {
            SetDates(configuration);
            CreateHistoryToken(user, configuration);
            AddClaims(user);
            CreateToken(user, configuration);
            return _tokenHistory;
        }

        private static void CreateToken(User user, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(getTokenDescriptor(user, configuration));

            user.Token = tokenHandler.WriteToken(securityToken);
        }

        private static SecurityTokenDescriptor getTokenDescriptor(User user, IConfiguration configuration)
        {
            var key = Encoding.UTF8.GetBytes(configuration["JwtBearer:SecretKey"]);
            var algorithm = configuration.GetValue<string>("JwtBearer:Algorithm");

            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(getClaims(user)),
                Expires = _expiration,
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), algorithm)
            };
        }

        private static IEnumerable<Claim> getClaims(User user) =>
            user.Claims.Select(uc => new Claim(uc.ClaimType, uc.ClaimValue));

        private static void AddClaims(User user)
        {
            user.Claims.Add(createUserClaim(user.Id, JwtRegisteredClaimNames.Jti, _tokenHistory.TokenId.ToString()));
            user.Claims.Add(createUserClaim(user.Id, ClaimTypes.NameIdentifier, _tokenHistory.TokenId.ToString()));
            user.Claims.Add(createUserClaim(user.Id, ClaimTypes.Name, _tokenHistory.TokenId.ToString()));
            user.Claims.Add(createUserClaim(user.Id, JwtRegisteredClaimNames.Email, _tokenHistory.UserEmail));
            user.Claims.Add(createUserClaim(user.Id, JwtRegisteredClaimNames.NameId, _tokenHistory.UserId.ToString()));
            user.Claims.Add(createUserClaim(user.Id, JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(_tokenHistory.Nbf).ToString()));
            user.Claims.Add(createUserClaim(user.Id, JwtRegisteredClaimNames.Nbf, EpochTime.GetIntDate(_tokenHistory.Nbf).ToString()));
            user.Claims.Add(createUserClaim(user.Id, JwtRegisteredClaimNames.Exp, EpochTime.GetIntDate(_tokenHistory.Exp).ToString()));
            user.Claims.Add(createUserClaim(user.Id, JwtRegisteredClaimNames.Aud, _tokenHistory.Audience));
            user.Claims.Add(createUserClaim(user.Id, JwtRegisteredClaimNames.Iss, _tokenHistory.Issuer));
        }

        private static UserClaim createUserClaim(Guid userId, string claimKey, string claimValue)
        {
            return new UserClaim()
            {
                ClaimType = claimKey,
                ClaimValue = claimValue,
                UserId = userId,
                CreatedBy = "SYSTEM",
                CreationDate = DateTime.UtcNow,
                LastModifiedBy = "SYSTEM",
                LastModificationDate = DateTime.UtcNow,
                Enabled = true
            };
        }

        private static void CreateHistoryToken(User user, IConfiguration configuration)
        {
            _tokenHistory = new TokenHistory()
            {
                Audience = configuration.GetValue<string>("JwtBearer:Audience"),
                Exp = _expiration,
                Iat = _currentDateTime,
                Issuer = configuration.GetValue<string>("JwtBearer:Issuer"),
                Nbf = _currentDateTime,
                CreatedBy = "SYSTEM",
                CreationDate = DateTime.UtcNow,
                LastModifiedBy = "SYSTEM",
                LastModificationDate = DateTime.UtcNow,
                Enabled = true,
                TokenId = Guid.NewGuid(),
                UserEmail = user.Email,
                UserId = user.Id,
                UserName = user.UserName
            };
        }

        private static void SetDates(IConfiguration configuration)
        {
            _currentDateTime = DateTime.UtcNow;
            _expiration = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("JwtBearer:Expiration"));
        }
    }
}
