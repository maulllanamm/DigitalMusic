using DigitalMusic.Domain.Entities;
using System.Security.Claims;

namespace DigitalMusic.Application.Helper.Interface
{
    public interface IAccessTokenHelper
    {
        public string GenerateAccessToken(string id, string email, string username , string roleName);
        public ClaimsPrincipal ValidateAccessToken(string token);
    }
}
