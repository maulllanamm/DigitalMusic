using MediatR;

namespace DigitalMusic.Application.Features.SongFeatures.Command.CreateAlbum
{
    public sealed record CreateSongRequest
    (
        string Title,
        int Year,
        string Performer,
        string Genre,
        int Duration,
        Guid? AlbumId
    ) : IRequest<Guid>;
}
