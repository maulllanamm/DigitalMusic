using MediatR;

namespace DigitalMusic.Application.Features.AlbumFeatures.Command.CreateAlbum
{
    public sealed record CreateAlbumRequest
    (
        string Name,
        int Year
    ) : IRequest<Guid>;
}
