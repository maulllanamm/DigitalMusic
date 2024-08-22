using MediatR;

namespace DigitalMusic.Application.Features.UserFeatures.Command.CreateAlbum
{
    public sealed record DeleteAlbumRequest
    (
        Guid Id
    ) : IRequest<bool>;
}
