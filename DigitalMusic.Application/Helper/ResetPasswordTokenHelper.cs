using AutoMapper;
using DigitalMusic.Application.Helper.Interface;
using DigitalMusic.Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DigitalMusic.Application.Helper
{
    public class ResetPasswordTokenHelper : IResetPasswordTokenHelper
    {
        private readonly TokenManagement _token;
        public ResetPasswordTokenHelper(IOptions<TokenManagement> token)
        {
            _token = token.Value;
        }

        public string GeneratePasswordResetToken(string Username, string Email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token.Secret));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Username),
                new Claim(ClaimTypes.Email, Email),
            };

            var tokenDescriptor = new JwtSecurityToken
                (
                    _token.Issuer,
                    _token.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: credential
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenDescriptor);
            return token;
        }
    }
}
