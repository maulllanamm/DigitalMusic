using DigitalMusic.Application.Features.AuthFeatures.LoginFeatures;
using DigitalMusic.Domain.Entities;
using System.Security.Claims;

namespace DigitalMusic.Application.Helper.Interface
{
    public interface IRefreshTokenHelper
    {
        public string GenerateRefreshToken(string id, string email, string username, string roleName);
        public void SetRefreshToken(string newRefreshToken, string username);
        public Task ValidateRefreshToken(string username, string refreshToken);
    }
}
