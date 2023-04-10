using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DTO.Response;
using Microsoft.IdentityModel.Tokens;
using Domain;
using Services.Account;
using SecurityAlgorithms = Microsoft.IdentityModel.Tokens.SecurityAlgorithms;

namespace Services.Helper
{
    public static class TokenHelper
    {
        public static ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "user")
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        public static SecurityTokenViewModel GetSecurityToken(ClaimsIdentity identity, bool remember)
        {
            var now = DateTime.UtcNow;

            var expires = remember
                ? DateTime.MaxValue : now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME));

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: expires,
                signingCredentials: new SigningCredentials(
                    AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new SecurityTokenViewModel(encodedJwt, expires);
        }
    }
}