using MediatR;

namespace DigitalMusic.Application.Features.AlbumFeatures.Command.DeleteAlbum
{
    public sealed record DeleteAlbumRequest
    (
        Guid Id
    ) : IRequest<bool>;
}
