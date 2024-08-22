using DigitalMusic.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DigitalMusic.Application.Features.AuthFeatures.PermittionFeatures
{
    public sealed record IsPermittedRequest
    (
        HttpContext HttpContext,
        Role? role
    ) : IRequest<bool>;

}
