using MediatR;

namespace DigitalMusic.Application.Features.AuthFeatures.ForgotPasswordFeatures
{
    public sealed record ForgotPasswordRequest
    (
        string Email
    ) : IRequest<string>;
}
