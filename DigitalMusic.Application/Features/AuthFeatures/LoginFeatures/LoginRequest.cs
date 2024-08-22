using MediatR;

namespace DigitalMusic.Application.Features.AuthFeatures.LoginFeatures
{
    public sealed record LoginRequest
    (
        string Username,
        string Password
    ) : IRequest<LoginResponse>;
}
