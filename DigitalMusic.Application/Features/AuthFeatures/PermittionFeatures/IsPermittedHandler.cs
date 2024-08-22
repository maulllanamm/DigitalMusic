using DigitalMusic.Application.Helper;
using MediatR;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using DigitalMusic.Application.Helper.EnumCollection;

namespace DigitalMusic.Application.Features.AuthFeatures.PermittionFeatures
{
    public class IsPermittedHandler : IRequestHandler<IsPermittedRequest, bool>
    {
        private readonly TokenManagement _token;

        public IsPermittedHandler(IOptions<TokenManagement> token)
        {
            _token = token.Value;
        }

        public async Task<bool> Handle(IsPermittedRequest request, CancellationToken cancellationToken)
        {
            var role = request.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            var path = request.HttpContext.Request.Path.Value;
            var method = request.HttpContext.Request.Method.ToString();
            var isPermitted = false;


            if (role == UserRole.Administrator || role is null)
            {
                return true;
            }

            foreach (var rolePermission in request.role.role_permissions)
            {
                if (path == rolePermission.permission.path && method == rolePermission.permission.http_method)
                {
                    isPermitted = true;
                    return isPermitted;
                }
                else
                {
                    isPermitted = false;
                }
            }

            return isPermitted;
        }
    }
}
