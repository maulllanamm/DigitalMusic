using MediatR;

namespace DigitalMusic.Application.Features.SongFeatures.Command.UpdateSong
{
    public sealed record UpdateSongRequest
    (
        Guid Id,
        string Title,
        int Year,
        string Performer,
        string Genre,
        int Duration,
        Guid? AlbumId
    ) : IRequest<UpdateSongResponse>;
}
