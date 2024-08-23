using MediatR;

namespace DigitalMusic.Application.Features.AlbumFeatures.Command.UpdateAlbum
{
    public sealed record UpdateAlbumRequest
    (
        Guid Id,
        string Name,
        int Year
    ) : IRequest<UpdateAlbumResponse>;
}
