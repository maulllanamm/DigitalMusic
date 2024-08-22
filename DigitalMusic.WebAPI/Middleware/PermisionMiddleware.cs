using DigitalMusic.Application.Features.AuthFeatures.PermittionFeatures;
using DigitalMusic.Application.Repositories;
using MediatR;
using System.Net;
using System.Security.Claims;

namespace DigitalMusic.WebAPI.Middleware
{
    public class PermisionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMediator _mediator;
        public PermisionMiddleware(RequestDelegate next, IMediator mediator)
        {
            _next = next;
            _mediator = mediator;
        }

        public async Task InvokeAsync(HttpContext context, IUserRepository userRepository)
        {
            try
            {
                var user = await userRepository.GetByUsername(context.User.FindFirst(ClaimTypes.Name)?.Value);
                if(user is null)
                {
                    user = new Domain.Entities.User();
                }
                var isPermitted = await _mediator.Send(new IsPermittedRequest(context, user.role));

                if (isPermitted)
                {
                    await _next(context);
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or log them as needed
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
