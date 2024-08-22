using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Helper.Interface;
using DigitalMusic.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DigitalMusic.Application.Helper
{
    public class AccessTokenHelper : IAccessTokenHelper
    {
        private readonly TokenManagement _token;

        public AccessTokenHelper(IOptions<TokenManagement> token)
        {
            _token = token.Value;
        }

        public string GenerateAccessToken(string id, string email, string username, string roleName)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token.Secret));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, id),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, roleName),
            };

            var tokenDescriptor = new JwtSecurityToken
                (
                    _token.Issuer,
                    _token.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_token.ExpiryAccessMinutes),
                    signingCredentials: credential
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenDescriptor);
            return token;
        }

        public ClaimsPrincipal ValidateAccessToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token.Secret));

            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = key,
                ValidIssuer = _token.Issuer,
                ValidAudience = _token.Audience,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);


            var jsonToken = validatedToken as JwtSecurityToken;

            var isValidToken = jsonToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature, StringComparison.InvariantCultureIgnoreCase);
            if (jsonToken == null || !isValidToken)
            {
                var errors = new string[] { "Invalid token." };
                throw new BadRequestException(errors);
            }
            return principal;
        }
    }
}
