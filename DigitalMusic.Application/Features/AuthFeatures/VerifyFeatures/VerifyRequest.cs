using MediatR;

namespace DigitalMusic.Application.Features.AuthFeatures.VerifyFeatures
{
    public sealed record VerifyRequest
    (
        string VerifyToken
    ) : IRequest<string>;
}
