using DigitalMusic.Application.Helper.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DigitalMusic.Application.Helper
{
    public class VerifyTokenHelper : IVerifyTokenHelper
    {
        private readonly TokenManagement _token;

        public VerifyTokenHelper(IOptions<TokenManagement> token)
        {
            _token = token.Value;
        }

        public async Task<string> GenerateVerifyToken(string email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token.Secret));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
            };

            var tokenDescriptor = new JwtSecurityToken
                (
                    _token.Issuer,
                    _token.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(7),
                    signingCredentials: credential
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenDescriptor);
            return token;
        }
    }
}
