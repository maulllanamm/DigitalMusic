using AutoMapper;
using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Features.AuthFeatures.LoginFeatures;
using DigitalMusic.Application.Helper.Interface;
using DigitalMusic.Application.Repositories;
using DigitalMusic.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DigitalMusic.Application.Helper
{
    public class RefreshTokenHelper : IRefreshTokenHelper
    {
        private readonly TokenManagement _token;
        private readonly IHttpContextAccessor _httpCont;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public RefreshTokenHelper(IOptions<TokenManagement> token, IHttpContextAccessor httpCont, IUserRepository userRepository, IMapper mapper)
        {
            _token = token.Value;
            _httpCont = httpCont;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public string GenerateRefreshToken(string id, string email,string username, string roleName)
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
                    expires: DateTime.UtcNow.AddMinutes(_token.ExpiryRefreshMinutes),
                    signingCredentials: credential
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenDescriptor);
            return token;
        }

        public async void SetRefreshToken(string newRefreshToken, string username)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(_token.ExpiryRefreshMinutes)
            };
            _httpCont.HttpContext.Response.Cookies.Append("refreshToken", newRefreshToken, cookieOptions);

            var newUser = await _userRepository.GetByUsername(username);

            newUser.refresh_token = newRefreshToken;
            newUser.refresh_token_created = DateTime.UtcNow;
            newUser.refresh_token_expires = DateTime.UtcNow.AddMinutes(_token.ExpiryRefreshMinutes);

            await _userRepository.Update(newUser);
        }
        public async Task ValidateRefreshToken(string username, string refreshToken)
        {
            var user = await _userRepository.GetByUsername(username);
            var errors = new string[] { };
            if (!user.refresh_token.Equals(refreshToken))
            {
                errors = new string[] { "Invalid Refresh token." };
                throw new BadRequestException(errors);
            }
            else if (user.refresh_token_expires < DateTime.Now)
            {
                errors = new string[] { "Token expired." };
                throw new BadRequestException(errors);
            }
        }
    }
}
